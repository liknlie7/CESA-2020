//
// Sonar FX
//
// Copyright (C) 2013, 2014 Keijiro Takahashi

// SonarFxâ¸ë¢
// 2020/05/17
// ç≤í|ê∞ìo
Shader "Hidden/SonarFX"
{
	Properties
	{
		_SonarBaseColor("Base Color",  Color) = (0.1, 0.1, 0.1, 0)
		_SonarWaveColor("Wave Color",  Color) = (1.0, 0.1, 0.1, 0)
		_SonarWaveParams("Wave Params", Vector) = (1, 20, 20, 10)
		_SonarWaveVector("Wave Vector", Vector) = (0, 0, 1, 0)
		_SonarAddColor("Add Color",   Color) = (0, 0, 0, 0)
		_SonarRadius("Wave Radius", Float) = 5
		_SonarTimer("Sonar Timer",Float) = 0
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
        float3 _SonarWaveVector;
        float3 _SonarAddColor;
        float  _SonarRadius;
		float _SonarTimer;

        void surf(Input IN, inout SurfaceOutput o)
        {
#ifdef SONAR_DIRECTIONAL
            float w = dot(IN.worldPos, _SonarWaveVector);
#else
            float w = length(IN.worldPos - _SonarWaveVector);
#endif
            
            // Circle Mask
            float dist = distance(_SonarWaveVector, IN.worldPos);
            float radius = _SonarRadius;
            if (radius < dist)
            {
                //o.Albedo = fixed4(0, 0, 0, 1);
            }
            else
            {
                // Moving wave.
                w -= _SonarTimer * _SonarWaveParams.w;

                if (w > 0)
                    w = 1;

                // Get modulo (w % params.z / params.z)
                w /= _SonarWaveParams.z;
                w = w - floor(w);

                // Make the gradient steeper.
                float p = _SonarWaveParams.y;
                w = (pow(w, p) + pow(1 - w, p * 4)) * 0.5;

                // Amplify.
                w *= _SonarWaveParams.x;

                o.Albedo = _SonarBaseColor;
                o.Emission = _SonarWaveColor * w + _SonarAddColor;
            }
            
        }

        ENDCG
    } 
    Fallback "Diffuse"
}
