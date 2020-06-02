Shader "UI/ice"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Color"       , Color) = (1, 1, 1, 1)
		_Cutoff("Cutoff"      , Range(0, 1)) = 0.5 //アルファチャネルでくりぬかれる範囲の指定
		_Amount("Threshold", Range(0, 1)) = 0.0
		_DisolveTex("DisolveTex (RGB)", 2D) = "white" {}
	}
		SubShader
		{
			Tags { "Queue" = "AlphaTest"
				   "RenderType" = "Opaque"
				 }

			Pass
			{
				LOD 200
				Cull Off
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0
				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				fixed4 _Color;

				sampler2D _MainTex;
				sampler2D _DisolveTex;
				float4 _MainTex_ST;
				half _Amount;
				fixed _Cutoff;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv) * _Color;

					fixed4 m = tex2D(_DisolveTex, i.uv);

					half g =  m.r * 0.2 + m.g * 0.7 + m.b * 0.1;
					if (g < (_Amount * 5)) {
						discard;
					}

					clip(col.a - _Cutoff);

					return col;
				}

				ENDCG
			}
		}
			FallBack "Transparent/Cutout/Diffuse"
}