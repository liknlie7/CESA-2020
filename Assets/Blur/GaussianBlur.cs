using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GaussianBlur : MonoBehaviour
{
    [SerializeField]
    private Shader _shader = null;

    [SerializeField, Range(1.0f, 10.0f)]
    private float _offset = 1.0f;

    [SerializeField, Range(1.0f, 1000.0f)]
    private float _blur = 100.0f;

    private Material _material;

    // Apply sevral blur effect so use as double buffers.
    private RenderTexture _rt2;

    private float[] _weights = new float[10];
    private static readonly int Offsets = Shader.PropertyToID("_Offsets");
    private static readonly int Weights = Shader.PropertyToID("_Weights");

    private Vector2Int _lastScreenSize;
    private bool WindowSizeChanged
    {
        get
        {
            var screenSize = new Vector2Int(Screen.width, Screen.height);
            if (_lastScreenSize == screenSize)
                return false;
            _lastScreenSize = screenSize;
            return true;
        }
    }

    #region ### MonoBehaviour ###
    /// 

    /// Do blur to the texture.
    /// 
    public void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        if (WindowSizeChanged)
        {
            //Debug.Log("Size Changed");
            Initialize(source);
        }

        if (_blur < 10f)
        {
            Graphics.Blit(source, dest);
            return;
        }

        UpdateWeights();

        _material.SetFloatArray(Weights, _weights);

        float x = _offset / source.width;
        float y = _offset / source.height;

        // for horizontal blur.
        _material.SetVector(Offsets, new Vector4(x, 0, 0, 0));

        Graphics.Blit(source, _rt2, _material);

        // for vertical blur.
        _material.SetVector(Offsets, new Vector4(0, y, 0, 0));

        Graphics.Blit(_rt2, dest, _material);
    }
    #endregion ### MonoBehaviour ###

    /// 

    /// Initialize (setup)
    /// 
    private void Initialize(RenderTexture source)
    {
        if (_material == null)
            _material = new Material(_shader) { hideFlags = HideFlags.HideAndDontSave };

        // Down scale.
        if (_rt2 != null)
            _rt2.Release();
        _rt2 = new RenderTexture(source.width / 2, source.height / 2, 0, RenderTextureFormat.ARGB32);
    }

    /// 

    /// Update waiths by gaussian function.
    /// 
    private void UpdateWeights()
    {
        float total = 0;
        float d = _blur * _blur * 0.001f;

        for (int i = 0; i < _weights.Length; i++)
        {
            // Offset position per x.
            float x = i * 2f;
            float w = Mathf.Exp(-0.5f * (x * x) / d);
            _weights[i] = w;

            if (i > 0)
            {
                w *= 2.0f;
            }

            total += w;
        }

        for (int i = 0; i < _weights.Length; i++)
        {
            _weights[i] /= total;
        }
    }
}
