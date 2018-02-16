Shader "GPUBuilder/GPUSurfaceInstanced" {

    Properties {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalMap ("Normal", 2D) = "bump" {}
		_NormalMapIntensity ("Normal Intensity", Range(0,10)) = 1
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM

        #pragma surface surf Standard addshadow fullforwardshadows 
		#pragma vertex vert
        #pragma multi_compile_instancing
        #pragma instancing_options procedural:setup

        sampler2D _MainTex;
		sampler2D _NormalMap;

        struct Input {
            float2 uv_MainTex;
			float2 uv_NormalMap;
        };

		#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
			StructuredBuffer<float4> positions;
            StructuredBuffer<float4> quaternions;
		#endif

        void setup()
        {
        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED

            float3 position 	= positions[unity_InstanceID];
			float4 q 			= quaternions[unity_InstanceID];
			float qr			= q[0];
			float qi			= q[1];
			float qj			= q[2];
			float qk			= q[3];

			float4x4 rotation;
			float4x4 translation = {
				1,0,0,position.x,
				0,1,0,position.y,
				0,0,1,position.z,
				0,0,0,1
			};

			rotation[0][0]			= 1.0f - 2.0f*qj*qj - 2.0f*qk*qk;
			rotation[0][1]			= 2.0f*(qi*qj - qk*qr);
			rotation[0][2]			= 2.0f*(qi*qk + qj*qr);
			rotation[0][3]			= 0.0f;

			rotation[1][0]			= 2.0f*(qi*qj+qk*qr);
			rotation[1][1]			= 1.0f - 2.0f*qi*qi - 2.0f*qk*qk;
			rotation[1][2]			= 2.0f*(qj*qk - qi*qr);
			rotation[1][3]			= 0.0f;

			rotation[2][0]			= 2.0f*(qi*qk - qj*qr);
			rotation[2][1]			= 2.0f*(qj*qk + qi*qr);
			rotation[2][2]			= 1.0f - 2.0f*qi*qi - 2.0f*qj*qj;
			rotation[2][3]			= 0.0f;

			rotation[3][0]			= 0.0f;
			rotation[3][1]			= 0.0f;
			rotation[3][2]			= 0.0f;
			rotation[3][3]			= 1.0f;
			// quaternion to matrix
			// http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToMatrix/
			// https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation#Quaternion-derived_rotation_matrix



//			unity_ObjectToWorld._11_21_31_41 = float4(1, 0, 0, 0);
//			unity_ObjectToWorld._12_22_32_42 = float4(0, 1, 0, 0);
//			unity_ObjectToWorld._13_23_33_43 = float4(0, 0, 1, 0);
//			unity_ObjectToWorld._14_24_34_44 = float4(position.xyz, 1);
			//unity_ObjectToWorld = rotation;
			unity_ObjectToWorld = mul(translation, rotation);

			
			// inverse transform matrix
			// taken from richardkettlewell's post on
			// https://forum.unity3d.com/threads/drawmeshinstancedindirect-example-comments-and-questions.446080/

			float3x3 w2oRotation;
			w2oRotation[0] = unity_ObjectToWorld[1].yzx * unity_ObjectToWorld[2].zxy - unity_ObjectToWorld[1].zxy * unity_ObjectToWorld[2].yzx;
			w2oRotation[1] = unity_ObjectToWorld[0].zxy * unity_ObjectToWorld[2].yzx - unity_ObjectToWorld[0].yzx * unity_ObjectToWorld[2].zxy;
			w2oRotation[2] = unity_ObjectToWorld[0].yzx * unity_ObjectToWorld[1].zxy - unity_ObjectToWorld[0].zxy * unity_ObjectToWorld[1].yzx;

			float det = dot(unity_ObjectToWorld[0], w2oRotation[0]);

			w2oRotation = transpose(w2oRotation);

			w2oRotation *= rcp(det);

			float3 w2oPosition = mul(w2oRotation, -unity_ObjectToWorld._14_24_34);

			unity_WorldToObject._11_21_31_41 = float4(w2oRotation._11_21_31, 0.0f);
			unity_WorldToObject._12_22_32_42 = float4(w2oRotation._12_22_32, 0.0f);
			unity_WorldToObject._13_23_33_43 = float4(w2oRotation._13_23_33, 0.0f);
			unity_WorldToObject._14_24_34_44 = float4(w2oPosition, 1.0f);
        #endif
        }

        half _Glossiness;
        half _Metallic;
		half _NormalMapIntensity;
        float4 _Color;

		void vert(inout appdata_full v,  out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
        }

        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb * _Color;
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
			o.Normal *= float3(_NormalMapIntensity, _NormalMapIntensity, 1);
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}