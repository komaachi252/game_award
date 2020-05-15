Shader "Example"
{
	Properties
	{
		[NoScaleOffset] _MainTex("Texture", 2D) = "white" {} //TilingとOffsetを非表示に
		[NoScaleOffset] _DistTex("Distortion Texture", 2D) = "white" {} //ディストーションするテクスチャを設定
		_RotateSpeed("Rotate Speed", float) = 1.0
		_Tiling("Tiling", float) = 1.0 //画像のサイズごとに等間隔で分割
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }

			Pass
			{
				CGPROGRAM
			   #pragma vertex vert
			   #pragma fragment frag
			   #include "UnityCG.cginc"

			   #define PI 3.141592

				struct appdata
				{
					float4 vertex : POSITION;//Vertexの座標
					float2 uv : TEXCOORD0;//uv座標
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;//uv座標
					float2 uv2 : TEXCOORD1;//ディストーション用のuv座標
					float4 vertex : SV_POSITION;//Vertex変換後の座標
				};

				sampler2D   _MainTex;//テクスチャ
				float4      _MainTex_ST;//繰り返し処理を行ったテクスチャ
				sampler2D   _DistTex;//ディストーション用のテクスチャ
				float4      _DistTex_ST;//ディストーション用の繰り返し処理を行ったテクスチャ
				float      _RotateSpeed;//回転速度
				float      _Tiling;//繰り返し

				//Vertexに頂点座標の登録
				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.uv2 = TRANSFORM_TEX(v.uv, _DistTex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
				// Distortionの値をangleに反映する 
				half dist = tex2D(_DistTex, i.uv2).r;
				// Timeを入力として現在の回転角度を作る
				half angle = (frac(_Time.x) + dist) * PI * 2;
				// 回転行列を作る
				half angleCos = cos(angle * _RotateSpeed);
				half angleSin = sin(angle * _RotateSpeed);
				half2x2 rotateMatrix = half2x2(angleCos, -angleSin, angleSin, angleCos);
				// タイリング処理
				i.uv = frac(i.uv * _Tiling);
				// 中心を起点にUVを回転させる
				i.uv = mul(i.uv - 0.5, rotateMatrix) + 0.5;

				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
		}
}