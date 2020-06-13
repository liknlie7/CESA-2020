using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraIntensity : MonoBehaviour
{
    [Range(0, 1)]
    public float ambientIntensityPercent;
    [Range(0, 1)]
    public float reflectionIntensityPercent;

    public float AmbientIntensity => Mathf.Lerp(ambientIntensityMin, ambientIntensityMax, ambientIntensityPercent);
    public float ReflectionIntensity => Mathf.Lerp(reflectionIntensityMin, reflectionIntensityMax, reflectionIntensityPercent);

    [Range(0, 8)]
    public float ambientIntensityMin = 0;
    [Range(0, 1)]
    public float reflectionIntensityMin = 0;

    [Range(0, 8)]
    public float ambientIntensityMax = 1;
    [Range(0, 1)]
    public float reflectionIntensityMax = 1;

    void OnPreRender()
    {
        RenderSettings.ambientIntensity = AmbientIntensity;
        RenderSettings.reflectionIntensity = ReflectionIntensity;
    }
}
