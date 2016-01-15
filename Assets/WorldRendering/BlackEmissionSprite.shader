Shader "Sprites/EmissionReactive"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _EmissionTex ("Emission Texture", 2D) = "black" {}
		
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
				_Cutoff ("Alpha Cutoff", Range (0,1)) = 0.5

	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="TransparentCutOut" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
			
		}
			LOD 300


		Cull Off
		Lighting On
		ZWrite Off
		Fog { Mode Off }
		

		CGPROGRAM
		#pragma surface surf LightSprite vertex:vert alpha alphatest:_Cutoff 
		#pragma multi_compile DUMMY PIXELSNAP_ON 

		sampler2D _MainTex;
		sampler2D _EmissionTex;
		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
			fixed4 color;
		};
		
		struct CustomSurfOutput{
		half3 Albedo;
    	half3 Normal;
    	half3 Emission;
   		half Specular;
    	half Gloss;
    	half Alpha;
    	half LightInfluence;
		};
		
		void vert (inout appdata_full v, out Input o)
		{
			#if defined(PIXELSNAP_ON) && !defined(SHADER_API_FLASH)
			v.vertex = UnityPixelSnap (v.vertex);
			#endif
			v.normal = float3(0,0,-1);
			v.tangent =  float4(1, 0, 0, 1);
			
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = _Color;
		}

		half4 LightingLightSprite (CustomSurfOutput s, half3 lightDir, half atten){

              half4 c = half4(0.00001,0.00001,0.00001,0.00001);
			  c.rgb = ((s.Albedo.rgb+c.rgb)*_LightColor0.rgb*atten);
              c.a = s.Alpha;
              return c;
		}
		void surf (Input IN, inout CustomSurfOutput o)
		{
			o.LightInfluence = 0.0;
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.LightInfluence = tex2D(_EmissionTex, IN.uv_MainTex).r;
		}
		ENDCG
	}

Fallback "Transparent/Cutout/Diffuse"
}
