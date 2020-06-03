using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera sonarCamera;
    [HideInInspector]
    public SonarFx sonarFx;

    private void Awake()
    {
        sonarFx = sonarCamera.GetComponent<SonarFx>();
    }

    public static CameraManager Get()
    {
        return Camera.main.GetComponent<CameraManager>();
    }
}
