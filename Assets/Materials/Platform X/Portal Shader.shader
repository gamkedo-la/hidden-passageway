Shader "Custom/Portal Shader" {
	Properties {
		_BaseTex ("Base layer", 2D) = "white" {}
		_FirstTex ("First layer", 2D) = "white" {}
		_SecondTex ("Second layer", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_ScrollXSpeed ("X Scroll Speed", Range(0, 10)) = 2
		_ScrollYSpeed ("Y Scroll Speed", Range(0, 10)) = 2
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

        Pass {
            ZWrite On
        }

		// Base layer, circular motion
        CGPROGRAM
        #pragma surface surf Lambert alpha:auto fullforwardshadows vertex:vert
		#pragma target 3.0

        sampler2D _BaseTex;
		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;

        struct Input {
            float2 uv_BaseTex;
        };

		void vert (inout appdata_full v) {
            float sinX = sin (_ScrollXSpeed * _Time);
            float cosX = cos (_ScrollXSpeed * _Time);
            float sinY = sin (_ScrollYSpeed * _Time);
            float2x2 rotationMatrix = float2x2( cosX, -sinX, sinY, cosX);
            v.texcoord.xy = mul ( v.texcoord.xy, rotationMatrix );
        }

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_BaseTex, IN.uv_BaseTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG

		// First layer, moving zig-zag diagonally
        CGPROGRAM
        #pragma surface surf Lambert alpha:auto fullforwardshadows vertex:vert
		#pragma target 3.0

        sampler2D _FirstTex;
		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;

        struct Input {
            float2 uv_FirstTex;
        };

		void vert (inout appdata_full v) {
            fixed2 direction = fixed2(-_ScrollXSpeed / 6, -_ScrollYSpeed / 6);
            fixed2 normDirection = normalize(direction);
            fixed2 crossDirection = fixed2(direction.y, -direction.x);
            v.texcoord.xy += (direction * _Time) + (crossDirection * _SinTime);

		}

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_FirstTex, IN.uv_FirstTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG

		// Second layer, moving in big circles
		CGPROGRAM
        #pragma surface surf Lambert alpha:auto fullforwardshadows vertex:vert
		#pragma target 3.0

        sampler2D _SecondTex;
		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;

        struct Input {
            float2 uv_SecondTex;
        };

		void vert (inout appdata_full v) {
			fixed xScrollValue = _ScrollXSpeed * _CosTime;
			fixed yScrollValue = _ScrollYSpeed * _SinTime;
			v.texcoord.xy += fixed2(xScrollValue, yScrollValue);
        }

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_SecondTex, IN.uv_SecondTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
	}
	FallBack "Diffuse"
}
