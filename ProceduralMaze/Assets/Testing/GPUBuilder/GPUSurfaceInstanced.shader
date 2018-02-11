Shader "GPUBuilder/GPUSurfaceInstanced" {

    Properties {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalMap ("Normal", 2D) = "bump" {}
		_NormalMapIntensity ("Normal Intensity", Range(0,10)) = 1
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
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
		#endif

        void setup()
        {
        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED

            float4 data = positions[unity_InstanceID];
            unity_ObjectToWorld._11_21_31_41 = float4(data.w, 0, 0, 0);
            unity_ObjectToWorld._12_22_32_42 = float4(0, data.w, 0, 0);
            unity_ObjectToWorld._13_23_33_43 = float4(0, 0, data.w, 0);
            unity_ObjectToWorld._14_24_34_44 = float4(data.xyz, 1);
            unity_WorldToObject = unity_ObjectToWorld;
            unity_WorldToObject._14_24_34 *= -1;
            unity_WorldToObject._11_22_33 = 1.0f / unity_WorldToObject._11_22_33;

        #endif
        }

        half _Glossiness;
        half _Metallic;
		half _NormalMapIntensity;

		void vert(inout appdata_full v,  out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
        }

        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
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