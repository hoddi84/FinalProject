// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Propagating" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Color2 ("Color 2", Color) = (0,0,0,0)
        _Point ("Origin", Vector) = (0,0,0,1)
        _MaxDistance("MaxDistance", Range(0,5)) = 0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
       
        Pass {
            Tags { "LightMode" = "Always" }
           
            Fog { Mode Off }
            ZWrite On
            ZTest LEqual
            Cull Back
            Lighting Off
   
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
               
                float4 _Color;
                float4 _Color2;
                float4 _Point;
                uint _MaxDistance;
               
                struct appdata {
                    float4 vertex : POSITION;
                    float4 color : TEXCOORD0;
                };
               
                struct v2f {
                    float4 vertex : POSITION;
                    float4 color : TEXCOORD0;
                };
               
                v2f vert (appdata v) {

                    // testing here
                    float4 obj2world = mul(unity_ObjectToWorld, _Point);
                    float world2obj = mul(unity_WorldToObject, _Point);

                    float dist = distance(_Point, v.vertex);

                    float4 color = v.color;

                    if (dist < _MaxDistance) 
                    {
                        color = _Color;
                    }
                    else
                    {
                        color = _Color2;
                    }


                    v2f o;
                    o.color = color;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    return o;
                }
               
                fixed4 frag (v2f i) : COLOR {
                    fixed4 output;
                    output = i.color;
                    return output;
                }
            ENDCG
   
        }
    }
}