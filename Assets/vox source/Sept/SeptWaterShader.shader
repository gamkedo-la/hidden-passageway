Shader "Custom/SeptWaterShader" {
	Properties {
		// Everything in the Triplanar Toon Shader
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Top Water Texture", 2D) = "white" {}
		_MainTexSide("Side Water Texture", 2D) = "white" {}
		_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}
		_Normal("Normal/Noise", 2D) = "bump" {}
		_Scale("Top Scale", Range(-2,2)) = 1
		_SideScale("Side Scale", Range(-2,2)) = 1
		_NoiseScale("Noise Scale", Range(-2,2)) = 1
		_TopSpread("TopSpread", Range(-2,2)) = 1
		_EdgeWidth("EdgeWidth", Range(0,0.5)) = 1
		_RimPower("Rim Power", Range(-2,20)) = 1
		_RimColor("Rim Color Top", Color) = (0.5,0.5,0.5,1)
		_RimColor2("Rim Color Side/Bottom", Color) = (0.5,0.5,0.5,1)
		_FlowSpeed("Flow Speed and Direction", Range(-1,1)) = 1

			//Extra parameters we MIGHT use
			_Glossiness("Smoothness", Range(0,1)) = 0.5
			_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM

		//Toon shader calls for this, we're going to try the standard shader
		/*	#pragma surface surf ToonRamp
			sampler2D _Ramp;
		*/

		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows
		
		sampler2D _Ramp;

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		//THIS MAY NOT FAIL / NOT WORK
		// custom lighting function that uses a texture ramp based
		// on angle between light direction and normal
#pragma lighting ToonRamp exclude_path:prepass
		inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
		{
#ifndef USING_DIRECTIONAL_LIGHT
			lightDir = normalize(lightDir);
#endif

			half d = dot(s.Normal, lightDir)*0.5 + 0.5;
			half3 ramp = tex2D(_Ramp, float2(d, d)).rgb;

			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
			c.a = 0;
			return c;
		}

		// DECLARE PROPERTY VARIABLES
		sampler2D _MainTex, _MainTexSide, _Normal;
		float4 _Color, _RimColor, _RimColor2;
		float _RimPower;
		float _TopSpread, _EdgeWidth;
		float _Scale, _SideScale, _NoiseScale, _FlowSpeed;
		// Possibly Extra
		half _Glossiness;
		half _Metallic;

		struct Input {
			INTERNAL_DATA
			float2 uv_MainTex : TEXCOORD0;
			float3 worldPos; // world position built-in value
			float3 worldNormal; // world normal built-in value
			float3 viewDir;// view direction built-in value we're using for rimlight
		};

		

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			//ANIMATED TRIPLANAR NOISE STARTS here

			// clamp (saturate) and increase (pow) the worldnormal value to use as a blend between the projected textures
			float3 blendNormal = saturate(pow(IN.worldNormal * 1.4, 4));

			// Take the world position and move Y over _Time. Stretch the X/Z sides. Project side textures on Moved world position
			float3 mWorldPos = (IN.worldPos.x * 10, IN.worldPos.y *_Time.y, IN.worldPos.z * 10);
			float3 xo = tex2D(_Normal, mWorldPos.zy);
			float3 zo = tex2D(_Normal, mWorldPos.xy);

			// For the top, animate the texture using _Time and the World Normal
			float3 movingTop = (IN.worldNormal * _Time);
			float3 yo = tex2D(_Normal, movingTop.zx);

			// lerp together x/y/z
			float3 movingnoisetexture = zo;
			movingnoisetexture = lerp(movingnoisetexture, xo, IN.worldNormal);
			movingnoisetexture = lerp(movingnoisetexture, yo, IN.worldNormal);

			// add noise to normal
			o.Normal += movingnoisetexture;
			
			//BASE TRIPLANAR BELOW


			// clamp (saturate) and increase (pow) the worldnormal value to use as a blend between the projected textures
			//float3 blendNormal = saturate(pow(IN.worldNormal * 1.4, 4));

			//WE ARE REPLACING THIS WITH MOVING NOISE
			// normal noise triplanar for x,y,z
			float3 xn = tex2D(_Normal, IN.worldPos.zy * _NoiseScale);
			float3 yn = tex2D(_Normal, IN.worldPos.zx * _NoiseScale);
			float3 zn = tex2D(_Normal, IN.worldPos.xy * _NoiseScale);

			// lerped together all sides for noise texture
			float3 noisetexture = zn;
			noisetexture = lerp(noisetexture, xn, blendNormal.x);
			noisetexture = lerp(noisetexture, yn, blendNormal.y);
			

			// triplanar for top texture for x, y, z sides
			float3 xm = tex2D(_MainTex, IN.worldPos.zy * _Scale);
			float3 zm = tex2D(_MainTex, IN.worldPos.xy * _Scale);
			float3 ym = tex2D(_MainTex, IN.worldPos.zy * _Scale);
			// NOTE: ^^^ That this differs from the normal.
			// I THINK since it's for the top, we're talking relative
			// z/y/x so the world positions are different orientations

			// lerped together all sides for top texture
			float3 toptexture = zm;
			toptexture = lerp(toptexture, xm, blendNormal.x);
			toptexture = lerp(toptexture, ym, blendNormal.y);

			// triplanar for side and bottom texture x,y,z sides
			float3 x = tex2D(_MainTexSide, IN.worldPos.zy * _SideScale);
			float3 y = tex2D(_MainTexSide, IN.worldPos.zx * _SideScale);
			float3 z = tex2D(_MainTexSide, IN.worldPos.xy * _SideScale);
			// NOTE: Here we are back to the same orientation as the normal

			// lerped together all sides for the side bottom texture
			float3 sidetexture = z;
			sidetexture = lerp(sidetexture, x, blendNormal.x);
			sidetexture = lerp(sidetexture, y, blendNormal.y);

			// rim light for fuzzy top texture
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal * noisetexture));
			// MOVING NOISE VERSION
			//half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal * movingnoisetexture));

			// rim light for side/bottom texture
			half rim2 = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));

			// dot product of world normal and surface normal + noise
			float worldNormalDotNoise = dot(o.Normal + (noisetexture.y + (noisetexture * 0.5)), IN.worldNormal.y);
			// MOVING NOISE VERSION
			//float worldNormalDotNoise = dot(o.Normal + (movingnoisetexture.y + (movingnoisetexture * 0.5)), IN.worldNormal.y);

			// if dot product is higher than the top spread slider, multiply by triplanar mapped top texture
			// step is replacing an if statement to avoid branching :
			// if (worldNormalDotNoise > _TopSpread{ o.Albedo = toptexture}
			float3 topTextureResult = step(_TopSpread, worldNormalDotNoise) * toptexture;

			// if dot product is lower than the top spread slider, multiply by triplanar mapped side/bottom texture
			float3 sideTextureResult = step(worldNormalDotNoise, _TopSpread) * sidetexture;

			// if dot product is in between the two, make the texture darker
			float3 topTextureEdgeResult = step(_TopSpread, worldNormalDotNoise) * step(worldNormalDotNoise, (_TopSpread + _EdgeWidth)) *  -0.15;
			
			// final Albedo
			o.Albedo = topTextureResult + sideTextureResult + topTextureEdgeResult;
			o.Albedo *= _Color;

			// adding the fuzzy rimlight(rim) on the top texture, and the harder rimlight (rim2) on the side/bottom texture
			o.Emission = step(_TopSpread, worldNormalDotNoise) * _RimColor.rgb * pow(rim, _RimPower) + step(worldNormalDotNoise, _TopSpread) * _RimColor2.rgb * pow(rim2, _RimPower);

			// Standard Texture Color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
