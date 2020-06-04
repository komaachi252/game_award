Shader "UI/water"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Amount("Distort", Float) = 0.0
		_Color("Color"       , Color) = (1, 1, 1, 1)
		_Cutoff("Cutoff"      , Range(0, 1)) = 0.5 //アルファチャネルでくりぬかれる範囲の指定
	}
		SubShader
		{
			Tags { "Queue" = "AlphaTest"
				   "RenderType" = "Transparent" 
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
				float4 _MainTex_ST;
				float  _Amount;
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
					// 歪みの計算
					float x = 2 * i.uv.y + sin(_Time.y * 5);
					float distort = _Amount * sin(_Time.y * 10) * 0.1 *
										 sin(5 * x) * (-(x - 1) * (x - 1) + 1);
					// 座標を歪ませる 
					i.uv.x += distort;


					// RGB ごとに少しずつ座標をずらす
					fixed4 col = tex2D(_MainTex, i.uv) * _Color;

					clip(col.a - _Cutoff);

					return col;
				}

				ENDCG
			}
		}
		FallBack "Transparent/Cutout/Diffuse"
}