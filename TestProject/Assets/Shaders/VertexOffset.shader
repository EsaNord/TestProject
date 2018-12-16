Shader "Custom/VertexOffset" 
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white"{}
		_BumpMap("BumpMap", 2D) = "bump"{}
		_Metal("Metal", Float) = 1
		_Smooth("Smooth", Float) = 1
		_USpeed("USpeed", Float) = 1
		_VSpeed("VSpeed", Float) = 1
		_Scaler("Scale", Float) = 1
		_Amount("Offset Amount", Float) = -1.1
		_Transparency("Transparency", Range(0,1)) = 0.75
	}

	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows vertex vert fragment frag

		#include "UnityCG.cginc"

		struct Input {
		float2 uv_MainTex;
		float2 uv_BumpMap;
		};

		struct v2f {
			float2 uv : TEXCOORD0;
			float4 vertex : SV_POSITION;
		};

		fixed4 _Color;
		sampler2D _MainTex;
		sampler2D _BumpMap;
		float _Metal;
		float _Smooth;
		float _USpeed;
		float _VSpeed;
		float _Scaler;
		float _Amount;
		float _Transparency;

		void vert(inout appdata_full v) 
		{
			v.vertex.xyz += v.normal * _Amount;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 col = tex2D(_MainTex, i.uv) + _Color;
			col.a = _Transparency;
			return col;
		}

		void surf(Input IN, inout SurfaceOutputStandard o) 
		{
			float2 vAlbedoOffset = (IN.uv_MainTex * _Scaler) + float2(_Time.y * _USpeed, _Time.y * _VSpeed);
			float2 vNormalOffset = (IN.uv_BumpMap * _Scaler) + float2(_Time.y * _USpeed, _Time.y * _VSpeed);

			fixed4 c = tex2D(_MainTex, vAlbedoOffset) * _Color;
			o.Albedo = c.rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, vNormalOffset));
			o.Metallic = _Metal;
			o.Smoothness = _Smooth;
			o.Alpha = _Transparency;
		}
	ENDCG
	}
		FallBack "Diffuse"
}