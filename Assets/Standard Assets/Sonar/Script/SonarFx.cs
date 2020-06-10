//
// Sonar FX PostProcessing
//
// Copyright (C) 2013, 2014 Keijiro Takahashi

// SonarFx PostProcessing
// 2020/06/8
// 佐竹晴登
// 山口寛雅

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class SonarFx : MonoBehaviour
{
    #region Sonar

    // Sonar mode (directional or spherical)
    public enum SonarMode { Directional, Spherical }
    [SerializeField] SonarMode _mode = SonarMode.Directional;
    public SonarMode mode { get { return _mode; } set { _mode = value; } }

    //// Wave direction (used only in the directional mode)
    //[SerializeField] Vector3 _direction = Vector3.forward;
    //public Vector3 direction { get { return _direction; } set { _direction = value; } }

    //// Wave origin (used only in the spherical mode)
    //[SerializeField] Vector3 _origin = Vector3.zero;
    //public Vector3 origin { get { return _origin; } set { _origin = value; } }

    // Base color (albedo)
    [SerializeField] Color _baseColor = new Color(0.2f, 0.2f, 0.2f, 0);
    public Color baseColor { get { return _baseColor; } set { _baseColor = value; } }

    // Wave color amplitude
    [SerializeField] float _waveAmplitude = 2.0f;
    public float waveAmplitude { get { return _waveAmplitude; } set { _waveAmplitude = value; } }

    // Exponent for wave color
    [SerializeField] float _waveExponent = 22.0f;
    public float waveExponent { get { return _waveExponent; } set { _waveExponent = value; } }

    // Interval between waves
    [SerializeField] float _waveInterval = 20.0f;
    public float waveInterval { get { return _waveInterval; } set { _waveInterval = value; } }

    // Wave speed
    [SerializeField] float _waveSpeed = 10.0f;
    public float waveSpeed { get { return _waveSpeed; } set { _waveSpeed = value; } }

    // Wave Radius
    [SerializeField] float _waveRadius = 10.0f;

    public float WaveRadius { get { return _waveRadius; } set { _waveRadius = value; } }

    // Additional color (emission)
    [SerializeField] Color _addColor = Color.black;

    public Color addColor { get { return _addColor; } set { _addColor = value; } }

    [SerializeField] SonarPulse _defaultPulse;

    public SonarPulse defaultPulse { get { return _defaultPulse; } set { _defaultPulse = value; } }

    // Sonar Timings
    private float _sonarTimer = 0.0f;
    private int _sonarCounter = 0;
    private float[] _sonarStartTimes = Enumerable.Repeat(-float.MaxValue, 16).ToArray();
    private Vector4[] _sonarWaveVectors = Enumerable.Repeat(Vector4.zero, 16).ToArray();
    private Vector4[] _sonarWaveColors = Enumerable.Repeat(Vector4.zero, 16).ToArray();
    private SonarBounds[] _sonarBounds;

    SonarFx()
    {
        _sonarBounds = new SonarBounds[16];
        for (int i = 0; i < _sonarBounds.Length; i++)
            _sonarBounds[i] = new SonarBounds(this, i);
    }

    // Private shader variables
    int baseColorID;
    int waveParamsID;
    int addColorID;
    int waveRadiusID;
    int sonarTimerID;
    int sonarStartTimesID;
    int waveVectorsID;
    int waveColorsID;

    private void OnInitParamID()
    {
        baseColorID = Shader.PropertyToID("_SonarBaseColor");
        waveParamsID = Shader.PropertyToID("_SonarWaveParams");
        addColorID = Shader.PropertyToID("_SonarAddColor");
        waveRadiusID = Shader.PropertyToID("_SonarRadius");
        sonarTimerID = Shader.PropertyToID("_SonarTimer");
        sonarStartTimesID = Shader.PropertyToID("_SonarStartTimes");
        waveVectorsID = Shader.PropertyToID("_SonarWaveVectors");
        waveColorsID = Shader.PropertyToID("_SonarWaveColors");
    }

    public void Pulse(Vector3 pos, SonarPulse pulse = null, GameObject source = null)
    {
        if (pulse == null)
            pulse = defaultPulse;

        var bound = _sonarBounds[_sonarCounter];
        bound.source = source;
        bound.center = pos;
        bound.pulse = pulse;
        bound.startTime = _sonarTimer;
        bound.Apply();

        _sonarCounter = (_sonarCounter + 1) % _sonarBounds.Length;
    }

    public class SonarBounds
    {
        private readonly SonarFx _fx;
        private readonly int _id;

        public float startTime = -float.MaxValue;
        public Vector3 center;
        public SonarPulse pulse;
        public GameObject source;

        public SonarBounds(SonarFx fx, int id)
        {
            _fx = fx;
            _id = id;
        }

        public float Radius => (_fx._sonarTimer - startTime) * _fx._waveSpeed;

        public bool IsValid
        {
            get
            {
                if (pulse == null)
                    return false;
                var r = Radius;
                return (0 < r && r < pulse.range);
            }
        }

        public void Apply()
        {
            _fx._sonarStartTimes[_id] = startTime;
            _fx._sonarWaveVectors[_id] = new Vector4(center.x, center.y, center.z, pulse.range);
            _fx._sonarWaveColors[_id] = pulse.color;
        }
    }

    public SonarBounds[] GetSonarBounds()
    {
        return _sonarBounds;
    }

    void Update()
    {
        _sonarTimer += Time.deltaTime;
    }

    private void OnShaderParameter(Material mat)
    {
        //Debug.Log(_sonarTimer);
        mat.SetColor(baseColorID, _baseColor);
        mat.SetColor(addColorID, _addColor);
        mat.SetFloat(waveRadiusID, _waveRadius);
        mat.SetFloat(sonarTimerID, _sonarTimer);
        var param = new Vector4(_waveAmplitude, _waveExponent, _waveInterval, _waveSpeed);
        mat.SetVector(waveParamsID, param);
        mat.SetVectorArray(waveVectorsID, _sonarWaveVectors);
        mat.SetFloatArray(sonarStartTimesID, _sonarStartTimes);
        mat.SetVectorArray(waveColorsID, _sonarWaveColors);

        if (_mode == SonarMode.Directional)
        {
            mat.DisableKeyword("SONAR_SPHERICAL");
            //Shader.SetGlobalVector(waveVectorID, _direction.normalized);
        }
        else
        {
            mat.EnableKeyword("SONAR_SPHERICAL");
            //Shader.SetGlobalVector(waveVectorID, _origin);
        }
    }

    #endregion


    #region PostEffect

    //the main post effect shader
    [SerializeField] Shader shader;

    // Private shader variables
    int leftWorldFromViewID;
    int leftViewFromScreenID;

    //material field + accessor that creates it when needed (done this way to avoid editor confusion when OnEnable doesn't get called in time)
    Material m_material = null;
    Material Material
    {
        get
        {
            if (m_material == null)
            {
                m_material = new Material(shader);
                m_material.hideFlags = HideFlags.DontSave;
            }
            return m_material;
        }
    }

    //camera field + accessor
    Camera m_camera;
    Camera Camera
    {
        get
        {
            if (m_camera == null)
                m_camera = GetComponent<Camera>();
            return m_camera;
        }
    }

    void Awake()
    {
        leftWorldFromViewID = Shader.PropertyToID("_LeftWorldFromView");
        leftViewFromScreenID = Shader.PropertyToID("_LeftViewFromScreen");

        // Sonar Param ID initialization
        OnInitParamID();
    }

    //on disable destroys the material
    protected void OnDisable()
    {
        if (m_material)
            DestroyImmediate(m_material);
    }

    //pre render captures all the necessary matrix info, which is normally mangled by the time OnRenderImage is called
    private void OnPreRender()
    {
        //camera must at least be in depth mode
        Camera.depthTextureMode |= DepthTextureMode.DepthNormals;

        // Main eye inverse view matrix
        Matrix4x4 leftWorldFromView = Camera.cameraToWorldMatrix;

        // Inverse projection matrices, plumbed through GetGPUProjectionMatrix to compensate for render texture
        Matrix4x4 screenFromView = Camera.projectionMatrix;
        Matrix4x4 leftViewFromScreen = GL.GetGPUProjectionMatrix(screenFromView, true).inverse;

        // Negate [1,1] to reflect Unity's CBuffer state
        leftViewFromScreen[1, 1] *= -1;

        // Store matrices
        Material.SetMatrix(leftWorldFromViewID, leftWorldFromView);
        Material.SetMatrix(leftViewFromScreenID, leftViewFromScreen);

        // Sonar params
        OnShaderParameter(Material);
    }

    //simplest possible on render image just blits from source to dest using our shader
    [ImageEffectOpaque]
    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        Graphics.Blit(source, dest, Material);
    }

    #endregion
}
