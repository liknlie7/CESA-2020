using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraIntensity : MonoBehaviour
{
    private static float _lastIntensityValue;

    [Range(0, 1)]
    public float intensity;
    [Range(0, 1)]
    public float intensityTo;


    public float AmbientIntensity => Mathf.Lerp(ambientIntensityMin, ambientIntensityMax, intensity);
    public float ReflectionIntensity => Mathf.Lerp(reflectionIntensityMin, reflectionIntensityMax, intensity);

    [Range(0, 8)]
    public float ambientIntensityMin = 0;
    [Range(0, 8)]
    public float ambientIntensityMax = 1;

    [Range(0, 1)]
    public float reflectionIntensityMin = 0;
    [Range(0, 1)]
    public float reflectionIntensityMax = 1;

    private float UnLerp(float min, float max, float value)
    {
        return (value - min) / (max - min);
    }

    void Awake()
    {
        intensity = UnLerp(reflectionIntensityMin, reflectionIntensityMax, _lastIntensityValue);
    }

    void OnPreRender()
    {
        RenderSettings.ambientIntensity = AmbientIntensity;
        RenderSettings.reflectionIntensity = ReflectionIntensity;

        _lastIntensityValue = ReflectionIntensity;
    }

    void LateUpdate()
    {
        intensity = Mathf.MoveTowards(intensity, intensityTo, Time.deltaTime);
    }
}
