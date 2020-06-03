using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;

    public Camera mainCamera;
    public Camera sonarCamera;
    [HideInInspector]
    public SonarFx sonarFx;
    [HideInInspector]
    public SonarFxSwitcher sonarFxSwitcher;

    private void Awake()
    {
        instance = this;

        sonarFx = sonarCamera.GetComponent<SonarFx>();
        sonarFxSwitcher = sonarCamera.GetComponent<SonarFxSwitcher>();
    }

    public static CameraManager Get()
    {
        return instance;
    }
}
