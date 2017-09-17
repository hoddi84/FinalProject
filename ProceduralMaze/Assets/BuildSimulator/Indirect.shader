// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

 Shader "Instanced/InstancedShader" {
    Properties {
        //_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BallColor1 ("Ball Color 1", Color) = (0,1,0,0)
        _BallColor2 ("Ball Color 2", Color) = (0,0,0,0)
        _CubeColor ("Cube Color", Color) = (0,0,0,0)
        _DirChanger ("Dir Changer", Range(-1.0000, 1.0000)) = 0.00
        _BallSize ("Ball Size", Range(0.00, 150.00)) = 0.00
        _Point ("Ball Origin", Vector) = (0.0, 0.0, 0.0, 1.0)
    }
    SubShader {

        Pass {

            Tags {"LightMode"="ForwardBase"}

            CGPROGRAM

            #pragma vertex vertexProgram
            #pragma fragment fragmentProgram
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            #pragma target 4.5

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"
            #include "AutoLight.cginc"

            //sampler2D _MainTex;
            float4 _BallColor1;
            float4 _BallColor2;
            float4 _CubeColor;
            float _DirChanger;
            uint _BallSize;
            float4 _Point;

        #if SHADER_TARGET >= 45
            StructuredBuffer<float4> positionBuffer;
        #endif

            struct v2f
            {
                float4 pos : SV_POSITION;
                //float2 uv_MainTex : TEXCOORD0;
                float3 ambient : TEXCOORD1;
                float3 diffuse : TEXCOORD2;
                float3 color : TEXCOORD3;
                SHADOW_COORDS(4)
            };

            void ChangeColors(inout appdata_full v, uint instanceID)
            {
                if (instanceID < 100000) {
                    v.color = _BallColor1;
                }
                else if (instanceID >= 100000 && instanceID < 200000)
                {
                    v.color = _BallColor2;
                }
                else if (instanceID >= 200000)
                {
                    v.color = _BallColor2;
                }
            }

            void ResizeBall(float4 data, inout appdata_full v)
            {
                float dist = distance(_Point, data.xyz);

                if (dist > _BallSize)
                {
                    v.color = _CubeColor;
                }
            }

            void rotate2D(inout float2 v, float r)
            {
                float s, c;
                sincos(r, s, c);
                v = float2(v.x * c - v.y * s, v.x * s + v.y * c);
            }

            v2f vertexProgram (appdata_full v, uint instanceID : SV_InstanceID)
            {
            #if SHADER_TARGET >= 45
                float4 data = positionBuffer[instanceID];
            #else
                float4 data = 0;
            #endif

                ChangeColors(v, instanceID);

                ResizeBall(data, v);

                float rotation = data.w * data.w * _Time.x;
                rotate2D(data.xz, rotation);
                //data.xz = sinh(data.xz * _Time.x * 0.01);
                //data.y = sinh(data.y * _DirChanger) * _Time.x * 0.01;

                float3 localPosition = v.vertex.xyz * data.w;
                float3 worldPosition = data.xyz + localPosition;
                float3 worldNormal = v.normal;

                float3 ndotl = saturate(dot(worldNormal, _WorldSpaceLightPos0.xyz));
                float3 ambient = ShadeSH9(float4(worldNormal, 1.0f));
                float3 diffuse = (ndotl * _LightColor0.rgb);
                float3 color = v.color;

                v2f o;
                o.pos = mul(UNITY_MATRIX_VP, float4(worldPosition, 1.0f));
                //o.uv_MainTex = v.texcoord;
                o.ambient = ambient;
                o.diffuse = diffuse;
                o.color = color;
                TRANSFER_SHADOW(o)
                return o;
            }

            fixed4 fragmentProgram (v2f i) : SV_Target
            {
                // Tranforms position from world to homogenous space
                //float3 origin = UnityWorldToClipPos(_Point.xyz);
                // Tranforms position from view to homogenous space ----DONE, seems (0,0,0) is screen origin.
                //float3 origin = UnityViewToClipPos(_Point.xyz);
                // Tranforms position from object to camera space -----DONE, seems (0,0,0) is screen origin.
                //float3 origin = UnityObjectToViewPos(_Point.xyz);

                fixed shadow = SHADOW_ATTENUATION(i);
                //fixed4 albedo = tex2D(_MainTex, i.uv_MainTex);
                float3 lighting = i.diffuse * shadow + i.ambient;
                //fixed4 output = fixed4(albedo.rgb * i.color * lighting, albedo.w);
                fixed4 output = fixed4(i.color * lighting, 1);
                UNITY_APPLY_FOG(i.fogCoord, output);
                return output;
            }

            ENDCG
        }
    }
}