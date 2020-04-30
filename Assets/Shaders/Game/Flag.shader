Shader "Custom/water" {


	//Unity側から値を設定できるようにする
	Properties{
		_MainTex("Albedo (RGB)", 2D) = "while"{}
	}
		SubShader{
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
			
		//   "Queue"における描画順番
		//	「Background」→「Geometry」→「AlphaTest」→「Transparent」→「Overlay」

		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows  Lambert vertex:vert alpha:fade

		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;//テクスチャのuv座標
			float3 worldPos;
		};

	//struct Output {
	//	Albedo//基本色
	//	Normal//法線情報
	//};

		float _WorldposX, _WorldposY, _WorldposZ;
		float _Ripple_bUse;//波紋
		float _Ripple_On;
		float _Ripple_Speed;//波紋の速さ
		float _Water_Alpha;

		//頂点カラーを取り出す
		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			float amp = 0.05*sin(_Time * 75 + v.vertex.x * 75);//波の上下幅 * sin(波の速さ管理)
			v.vertex.xyz = float3(v.vertex.x, v.vertex.y + amp, v.vertex.z);
		}

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed2 uv = IN.uv_MainTex;

			uv.x += 0.1 * _Time;
			uv.y += 0.2 * _Time;

			float dist = distance(fixed3(_WorldposX, _WorldposY, _WorldposZ), IN.worldPos);
			float val = abs(sin(dist * 2 - _Time * 70));
			//float radius = 2;

			uv.x += 1.5 * _Time;//水の流れの速さを管理
			uv.y += 1.6 * _Time;//水の流れの速さを管理
			fixed4 c = tex2D(_MainTex, uv);//画像反映

			o.Albedo = c.rgb;
			if (val > 0.98 && (_Ripple_bUse == 1 || _Ripple_On == 1))
			{
				o.Alpha = c.a * _Water_Alpha;//透明度管理
			}
			else
			{
				o.Alpha = c.a * 0.5;//透明度管理
			}
		}	
	ENDCG
	}
		FallBack "Diffuse"
}