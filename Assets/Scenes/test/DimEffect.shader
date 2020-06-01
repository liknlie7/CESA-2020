Shader "Hidden/DimEffect"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
	_FogColor("Fog Color", Color) = (0, 0, 0, 1)
		_Duration("Duration", Range(0, 1)) = 0
	}

		SubShader
	{
		Tags{ "Queue" = "Background" "RenderType" = "Opaque" }
		ZTest Always
		ZWrite On

		Cull Off /*ZWrite Off ZTest Always*/

		Pass
	{
		Stencil
	{
		Ref 1
		Comp NotEqual
	}

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		sampler2D _MainTex;

	float4 _FogColor;
	float _Duration;
	float _StencilMask;

	struct v2f {
		half4 pos : POSITION;
		half2 uv : TEXCOORD0;
	};

	float4 _MainTex_ST;

	v2f vert(appdata_base v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		return o;
	}

	half4 frag(v2f i) : COLOR
	{
		float4 color = tex2D(_MainTex, i.uv);
		float3 lerpColor = lerp(color, _FogColor, _Duration).rgb;
		return float4(lerpColor, color.w);
	}

		ENDCG
	}
	}
}
