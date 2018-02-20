Shader "ShaderTesting/SimpleVertexShader"
{
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};
			
			v2f vert (appdata v)
			{
				v2f o = (v2f)0;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.vertex.x;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 col = i.color;
				return col;
			}
			ENDCG
		}
	}
}
