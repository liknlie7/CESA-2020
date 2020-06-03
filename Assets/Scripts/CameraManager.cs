using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;

    public Camera sonarCamera;
    [HideInInspector]
    public SonarFx sonarFx;

    private void Awake()
    {
        instance = this;

        sonarFx = sonarCamera.GetComponent<SonarFx>();
    }

    public static CameraManager Get()
    {
        return instance;
    }
}
