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
			float qw			= q[3];
			float qx			= q[0];
			float qy			= q[1];
			float qz			= q[2];

			float4x4 rotation;
			float4x4 translation = {
				1,0,0,position.x,
				0,1,0,position.y,
				0,0,1,position.z,
				0,0,0,1
			};

			rotation[0][0] = 1.0f - 2.0f*qy*qy - 2.0f*qz*qz;
			rotation[0][1] = 2.0f*(qx*qy - qz*qw);
			rotation[0][2] = 2.0f*(qx*qz + qy*qw);
			rotation[0][3] = 0.0f;

			rotation[1][0] = 2.0f*(qx*qy+qz*qw);
			rotation[1][1] = 1.0f - 2.0f*qx*qx - 2.0f*qz*qz;
			rotation[1][2] = 2.0f*(qy*qz - qx*qw);
			rotation[1][3] = 0.0f;

			rotation[2][0] = 2.0f*(qx*qz - qy*qw);
			rotation[2][1] = 2.0f*(qy*qz + qx*qw);
			rotation[2][2] = 1.0f - 2.0f*qx*qx - 2.0f*qy*qy;
			rotation[2][3] = 0.0f;

			rotation[3][0] = 0.0f;
			rotation[3][1] = 0.0f;
			rotation[3][2] = 0.0f;
			rotation[3][3] = 1.0f;

			unity_ObjectToWorld = mul(translation, rotation);
        #endif
        }

        half _Glossiness;
        half _Metallic;
		half _NormalMapIntensity;
        float4 _Color;

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