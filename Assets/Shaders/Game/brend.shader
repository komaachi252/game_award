Shader "Custom/sample" {
	Properties{
		_MainTex("Main Texture", 2D) = "white" {}
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Lambert vertex:vert
			#pragma target 3.0

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
			};

			//頂点カラーを取り出す
			void vert(inout appdata_full v, out Input o)
			{
				UNITY_INITIALIZE_OUTPUT(Input, o);
				v.vertex.xyz = float3(v.vertex.x, v.vertex.y, v.vertex.z);
			}

			void surf(Input IN, inout SurfaceOutput o) {
				fixed4 c1 = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = c1.rgb;
				o.Alpha = c1.a;
			}
			ENDCG
	}
		FallBack "Diffuse"
}