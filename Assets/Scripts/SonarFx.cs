//
// Sonar FX
//
// Copyright (C) 2013, 2014 Keijiro Takahashi
//
// SonarFx改造
// 2020/05/17
// 佐竹晴登
// 山口寛雅

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SonarFx : MonoBehaviour
{
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

    // Wave color
    [SerializeField] Color _waveColor = new Color(1.0f, 0.2f, 0.2f, 0);
    public Color waveColor { get { return _waveColor; } set { _waveColor = value; } }

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

    // Sonar Timings
    private float _sonarTimer = 0.0f;
    private int _sonarCounter = 0;
    private float[] _sonarWaves = Enumerable.Repeat(-float.MaxValue, 16).ToArray();
    private Vector4[] _sonarWaveVectors = Enumerable.Repeat(Vector4.zero, 16).ToArray();

    // Reference to the shader.
    [SerializeField] Shader shader;

    // Private shader variables
    int baseColorID;
    int waveColorID;
    int waveParamsID;
    int addColorID;
    int waveRadiusID;
    int sonarTimerID;
    int wavesID;
    int waveVectorsID;
    
    void Awake()
    {
        baseColorID = Shader.PropertyToID("_SonarBaseColor");
        waveColorID = Shader.PropertyToID("_SonarWaveColor");
        waveParamsID = Shader.PropertyToID("_SonarWaveParams");
        addColorID = Shader.PropertyToID("_SonarAddColor");
        waveRadiusID = Shader.PropertyToID("_SonarRadius");
        sonarTimerID = Shader.PropertyToID("_SonarTimer");
        wavesID = Shader.PropertyToID("_SonarWaves");
        waveVectorsID = Shader.PropertyToID("_SonarWaveVectors");
    }

    void OnEnable()
    {
        GetComponent<Camera>().SetReplacementShader(shader, null);
        Update();
    }

    void OnDisable()
    {
        GetComponent<Camera>().ResetReplacementShader();
    }

    public void Pulse(Vector3 pos, float range = 10)
    {
        _sonarWaves[_sonarCounter] = _sonarTimer;
        _sonarWaveVectors[_sonarCounter] = new Vector4(pos.x, pos.y, pos.z, range);
        _sonarCounter = (_sonarCounter + 1) % _sonarWaves.Length;
    }

    public struct SonarBounds
    {
        public Vector3 center;
        public float radius;
    }

    public IEnumerator<SonarBounds> GetSonarBounds()
    {
        for (int i = 0; i < _sonarWaves.Length; i++)
        {
            var vec = _sonarWaveVectors[i];
            var radius = (_sonarTimer - _sonarWaves[i]) * _waveSpeed;
            if (0 < radius && radius < vec.w)
                yield return new SonarBounds()
                {
                    center = (Vector3)vec,
                    radius = radius,
                };
        }
    }

    void Update()
    {
        _sonarTimer += Time.deltaTime;

        //Debug.Log(_sonarTimer);
        Shader.SetGlobalColor(baseColorID, _baseColor);
        Shader.SetGlobalColor(waveColorID, _waveColor);
        Shader.SetGlobalColor(addColorID, _addColor);
        Shader.SetGlobalFloat(waveRadiusID, _waveRadius);
        Shader.SetGlobalFloat(sonarTimerID, _sonarTimer);
        var param = new Vector4(_waveAmplitude, _waveExponent, _waveInterval, _waveSpeed);
        Shader.SetGlobalVector(waveParamsID, param);
        Shader.SetGlobalVectorArray(waveVectorsID, _sonarWaveVectors);
        Shader.SetGlobalFloatArray(wavesID, _sonarWaves);

        if (_mode == SonarMode.Directional)
        {
            Shader.DisableKeyword("SONAR_SPHERICAL");
            //Shader.SetGlobalVector(waveVectorID, _direction.normalized);
        }
        else
        {
            Shader.EnableKeyword("SONAR_SPHERICAL");
            //Shader.SetGlobalVector(waveVectorID, _origin);
        }
    }
}
