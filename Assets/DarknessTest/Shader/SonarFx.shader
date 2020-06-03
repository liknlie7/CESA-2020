//
// Sonar FX
//
// Copyright (C) 2013, 2014 Keijiro Takahashi

// SonarFxâ¸ë¢
// 2020/05/17
// ç≤í|ê∞ìo
// éRå˚ä∞âÎ
Shader "Hidden/SonarFX"
{
	Properties
	{
		_SonarBaseColor("Base Color",  Color) = (0.1, 0.1, 0.1, 0)
		_SonarWaveColor("Wave Color",  Color) = (1.0, 0.1, 0.1, 0)
		_SonarWaveParams("Wave Params", Vector) = (1, 20, 20, 10)
		_SonarAddColor("Add Color",   Color) = (0, 0, 0, 0)
		_SonarRadius("Wave Radius", Float) = 5
		_SonarTimer("Sonar Timer", Float) = 0
        //_SonarWaves("Waves", Float[]) = (0, -1, -1, -1)
        //_SonarWaves("Wave Vectors", Vector[])
    }
    SubShader
    {
        Tags { "RenderType" = "Geometry+2"}

        CGPROGRAM

        #pragma surface surf Lambert
        #pragma multi_compile SONAR_DIRECTIONAL SONAR_SPHERICAL

        struct Input
        {
            float3 worldPos;
        };

        float3 _SonarBaseColor;
        float3 _SonarWaveColor;
        float4 _SonarWaveParams; // Amp, Exp, Interval, Speed
        float3 _SonarAddColor;
        float  _SonarRadius;
        float _SonarTimer;
        float _SonarWaves[16];
        float3 _SonarWaveVectors[16];

        void surf(Input IN, inout SurfaceOutput o)
        {
            float3 waveColor = _SonarAddColor;

            // Circle Mask
            for (int i = 0; i < 4; i++)
            {
#ifdef SONAR_DIRECTIONAL
                float w = dot(IN.worldPos, _SonarWaveVectors[i]);
#else
                float w = length(IN.worldPos - _SonarWaveVectors[i]);
#endif
            
                if (_SonarRadius > w)
                {
                    // Moving wave. _waveSpeed
                    w -= (_SonarTimer - _SonarWaves[i]) * _SonarWaveParams.w; // w < 0

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

                    waveColor += _SonarWaveColor * w;
                }
            }

            o.Albedo = _SonarBaseColor;
            o.Emission = waveColor;
            
        }

        ENDCG
    } 
    Fallback "Diffuse"
}
