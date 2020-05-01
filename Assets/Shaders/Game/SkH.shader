Shader "Custom/SkH"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white"{}
	}

		CGINCLUDE
#include "UnityCG.cginc"

		float wave(float2 st)
	{
		float d = distance(0.5, st);
		
		return (1 + sin(d * 4 - _Time.y * 2)) * 0.5;
	}

	float4 frag(v2f_img i) : SV_Target
	{
		return float4 (1,
					 0.4f - wave(i.uv),
					 0,
					 0.5f - wave(i.uv));
	}


		//float4 frag(v2f_img i) : SV_Target
		//{
		//	return float4(1,
		//			    box_wave(i.uv, 4),
		//				 box_wave(i.uv, 4),
		//				 box_wave(i.uv, 4));
		//}


	//	float4 frag(v2f_img i) : SV_Target
	//{
	//	float2 st = 0.5 - i.uv;
	//	float a = atan2(st.y, st.x);

	//	float r = length(st);

	//	//花びら
	//	float d = min(abs(cos(a * 2.0)) + 0.4,//min(abs(cos(a * 2.0))): + 幅
	//						abs(sin(a * 2.0)) + 2.1) * 0.32;//abs(sin(a * 花びらの数)) + 形) * 大きさ

	//	//背景
	//	float4 color = lerp(0.8, float4(0, 0.4, 1, 0), i.uv.y);

	//	//グラデーションの幅
	//	float petal = step(r, d);
	//	color = lerp(color, lerp(float4(1, 0.3, 1, 1), 1, r * 2.5), petal);

	//	//中心
	//	float cap = step(distance(0, st), 0.11);// step(distance(0, 場所), サイズ)
	//	color = lerp(color, float4(0.99, 0.78, 0, 1), cap);

	//	return color;
	//}


	/*
	
	distance(a,b) = aとbの距離を返す
	step(a,x) = x>=aなら1、x<aなら0を返す
	
	
	*/


		ENDCG

		SubShader
	{
		Tags{ "Queue" = "Transparent" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			ENDCG
		}
	}
}
