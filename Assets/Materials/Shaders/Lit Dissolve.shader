Shader "Toon/Lit Dissolve" {
	Properties{
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}
	_NoiseTex("Dissolve Noise", 2D) = "white"{} // Texture the dissolve is based on
	_DisAmount("Dissolve Amount", Range(0, 1)) = 0 // amount of dissolving going on
		_DisLineWidth("Dissolve Width", Range(0, 2)) = 0 // width of the line around the dissolve
		_DisLineColor("Dissolve Color", Color) = (1,1,1,1) // Color of the dissolve Line
	}

		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha // transparency
		CGPROGRAM
#pragma surface surf ToonRamp keepalpha // transparency

		sampler2D _Ramp;

	// custom lighting function that uses a texture ramp based
	// on angle between light direction and normal
#pragma lighting ToonRamp exclude_path:prepass
	inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
	{
#ifndef USING_DIRECTIONAL_LIGHT
		lightDir = normalize(lightDir);
#endif

		half d = dot(s.Normal, lightDir)*0.5 + 0.5;
		half3 ramp = tex2D(_Ramp, float2(d,d)).rgb;

		half4 c;
		c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
		//c.a = 0; we don't want the alpha
		c.a = s.Alpha; // use the alpha of the surface output
		return c;
	}


	sampler2D _MainTex;
	float4 _Color;
	sampler2D _NoiseTex;//
	float _DisAmount;//
	float _DisLineWidth;//
	float4 _DisLineColor;//

	struct Input {
		float2 uv_MainTex : TEXCOORD0;
		float3 worldPos;// built in value to use the world space position

	};

	void surf(Input IN, inout SurfaceOutput o) {

		half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		half4 n = tex2D(_NoiseTex, IN.worldPos.xy); // turn the noise texture into a value we can compare to. worldPos.xy projects from one side, xz from other side, yz from top

		if (n.r - _DisLineWidth < _DisAmount) { //if the noise value minus the width of the line is lower than the dissolve amount
			c = _DisLineColor; // that part is the dissolve line color
		}
		if (n.r<_DisAmount) { // if the noise value is under the dissolve amount
			c.a = 0.0; // it's transparent, the alpha is set to 0
		}
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG

	}

		Fallback "Diffuse"
}