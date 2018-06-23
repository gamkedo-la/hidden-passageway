Shader "Custom/SeptWaterShader1" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpTex ("Water Bump", 2D) = "white" {}
		_BumpScale ("Bump Amount", Range(0,1)) = 1
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_ScrollSpeedX("Scroll Speed X", Range(-5,5)) = 1
		_ScrollSpeedY("Scroll Speed Y", Range(-5,5)) = 1
		_Alpha("Alpha Adjustment", Range(0, 2)) = 0
		_Emission("Emission Value", float) = 0
		
	}
	SubShader {
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:fade
		

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpTex;

		struct Input {
			float2 uv_MainTex;
			
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed _ScrollSpeedX;
		fixed _ScrollSpeedY;
		float _Alpha;
		float _BumpScale;
		float _Emission;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed2 scrollingUV = IN.uv_MainTex;

			fixed xScroll = _ScrollSpeedX * _Time;
			fixed yScroll = _ScrollSpeedY * _Time;

			scrollingUV += fixed2(xScroll, yScroll);
			
			float4 bumpscale = (_BumpScale, _BumpScale, _BumpScale, 1);

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 scrollc = tex2D(_MainTex, scrollingUV)* _Color;
			fixed4 b = tex2D(_BumpTex, scrollingUV);
		
			// Metallic and smoothness come from slider variables
			o.Albedo = scrollc.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness ;
			o.Alpha = scrollc.a * _Alpha;
			o.Normal = b * bumpscale;
			o.Emission = scrollc.a * _Alpha * _Emission;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
