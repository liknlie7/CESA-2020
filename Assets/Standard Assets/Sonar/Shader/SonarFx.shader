//
// Sonar FX PostProcessing
//
// Copyright (C) 2013, 2014 Keijiro Takahashi

// SonarFx PostProcessing
// 2020/06/8
// ç≤í|ê∞ìo
// éRå˚ä∞âÎ
Shader "Hidden/SonarFX"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		_SonarBaseColor("Base Color",  Color) = (0.1, 0.1, 0.1, 0)
		_SonarWaveParams("Wave Params", Vector) = (1, 20, 20, 10)
		_SonarAddColor("Add Color",   Color) = (0, 0, 0, 0)
		_SonarRadius("Wave Radius", Float) = 5
		_SonarTimer("Sonar Timer", Float) = 0
		//_SonarStartTimes("Waves", Vector[])
		//_SonarWaveVectors("Wave Vectors", Vector[])
		//_SonarWaveColors("Wave Colors", Vector[])
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			//depth buffer
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _CameraDepthTexture;
			float4 _CameraDepthTexture_ST;

			//view/projection matrices proved by VolumetricSphere.cs (in none vr, only left eye is used)
			float4x4 _LeftWorldFromView;
			float4x4 _LeftViewFromScreen;

			//sonar params
			float3 _SonarBaseColor;
			float4 _SonarWaveParams; // Amp, Exp, Interval, Speed
			float3 _SonarAddColor;
			float  _SonarRadius;
			float _SonarTimer;
			float4 _SonarStartTimes[16];
			float4 _SonarWaveVectors[16];
			float4 _SonarWaveColors[16];

			//simple vs output
			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			//simple vertex shader
			//transforms vertex from 0:1 into screen space -1:1
			//flips y of tex coord 
			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = v.vertex * float4(2,2,1,1) + float4(-1,-1,0,0);
				o.uv = v.texcoord;
				o.uv.y = 1.0f - o.uv.y; //blit flips the uv for some reason
				return o;
			}

			//world space fragment shader
			fixed4 frag (v2f i) : SV_Target
			{
				//read none linear depth texture, accounting for 
				float d = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, UnityStereoTransformScreenSpaceTex(i.uv)); // non-linear Z

				//pick one of the passed in projection/view matrices based on stereo eye selection (always left if not vr)
				float4x4 proj = _LeftViewFromScreen;
				float4x4 eyeToWorld = _LeftWorldFromView;

				//bit of matrix math to take the screen space coord (u,v,depth) and transform to world space
				float2 uvClip = i.uv * 2.0 - 1.0;
				float4 clipPos = float4(uvClip, d, 1.0);
				float4 viewPos = mul(proj, clipPos); // inverse projection by clip position
				viewPos /= viewPos.w; // perspective division
				float3 worldPos = mul(eyeToWorld, viewPos).xyz;

				//output result
				//fixed3 color = pow(abs(cos(worldPos * UNITY_PI * 4)), 20); // visualize grid
				//return fixed4(color, 1);

				float4 waveColor = tex2D(_MainTex, i.uv);

				// Circle Mask
				for (int i = 0; i < 16; i++)
				{
#ifdef SONAR_DIRECTIONAL
					float w = dot(worldPos, _SonarWaveVectors[i]);
#else
					float w = length(worldPos - _SonarWaveVectors[i]);
#endif

					float radius = _SonarWaveVectors[i].w;
					float radiusMultiplier = 1 - smoothstep(radius - _SonarRadius, radius + _SonarRadius, w);

					// Moving wave. _waveSpeed
					w -= (_SonarTimer - _SonarStartTimes[i].x) * _SonarStartTimes[i].y; // w < 0

					// Get modulo (w % params.z / params.z)
					w /= _SonarWaveParams.z; // _waveInterval

					if (w > 0)
						w = 1;

					w = clamp(w, -.5, .5);
					w = w - floor(w);

					// 0 < w < 1  0Ç∆1Ç≈ã…å¿=ê‘

					// Make the gradient steeper.
					float p = _SonarWaveParams.y; // _waveExponent
					w = (pow(w, p) + pow(1 - w, p * 4)) * 0.5;

					// Amplify.
					w *= _SonarWaveParams.x; // _waveAmplitude

					w *= radiusMultiplier;

					waveColor += _SonarWaveColors[i] * w;
				}

				return waveColor;
			}

			ENDCG
		}
	}
}
