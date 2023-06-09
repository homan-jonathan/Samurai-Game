Shader "Custom/CelShader" {
	Properties{
	  _Color("Color", Color) = (1, 1, 1, 1)
	  _MainTex("Texture", 2D) = "red" {}
	}
		SubShader{
		  Tags { "RenderType" = "Opaque" }
		  CGPROGRAM
	#pragma surface surf Ramp

	sampler2D _Ramp;

	half4 LightingRamp(SurfaceOutput s, half3 lightDir, half atten) {
		half NdotL = dot(s.Normal, lightDir);
		if (NdotL < 0.0) NdotL = 0; 
		else NdotL = 1;
		half4 c;
		c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten * 2);
		c.a = s.Alpha;
		return c;
	}

	struct Input {
		float2 uv_MainTex;
	};
	sampler2D _MainTex;
	fixed4 _Color;

	void surf(Input IN, inout SurfaceOutput o) {
		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
	}
		Fallback "Diffuse"
}


