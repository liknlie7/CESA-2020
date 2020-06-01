using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
[RequireComponent(typeof(UnityEngine.Camera))]
public class DimEffect : MonoBehaviour
{
    [SerializeField]
    private bool state;
    [SerializeField]
    private Shader dimEffectShader;
    [SerializeField]
    private Color fogColor;
    [Range(0f, 1f)]
    [SerializeField]
    private float duration;

    private Material _material;
    private static readonly int _FogColor = Shader.PropertyToID("_FogColor");
    private static readonly int _Duration = Shader.PropertyToID("_Duration");

    private UnityEngine.Camera _camera;
    private CommandBuffer _buffer;
    private int _tempTextureIdentifier;

    private bool _currentState = false;

    public void SetState(bool value)
    {
        if (_currentState == value) return;
        _currentState = value;

        if (value)
        {
            Init();
            _camera.AddCommandBuffer(CameraEvent.AfterForwardAlpha, _buffer);
        }
        else
        {
            _camera.RemoveCommandBuffer(CameraEvent.AfterForwardAlpha, _buffer);
        }
    }

    public void OnValidate()
    {
        SetState(state);

        if (_material != null)
        {
            _material.SetColor(_FogColor, fogColor);
            _material.SetFloat(_Duration, duration);
        }
    }

    private void Init()
    {
        if (_camera == null)
        {
            _camera = this.GetComponent<UnityEngine.Camera>();
        }

        if (_material == null)
        {
            _material = new Material(dimEffectShader) { hideFlags = HideFlags.DontSave };
        }
        _material.SetColor(_FogColor, fogColor);
        _material.SetFloat(_Duration, duration);

        if (_tempTextureIdentifier == 0) _tempTextureIdentifier = Shader.PropertyToID("_PostEffect");

        if (_buffer == null)
        {
            _buffer = new CommandBuffer { name = "DimEffect" };

            _buffer.GetTemporaryRT(_tempTextureIdentifier, -1, -1, 0);
            _buffer.Blit(BuiltinRenderTextureType.CameraTarget, _tempTextureIdentifier);
            _buffer.Blit(_tempTextureIdentifier, BuiltinRenderTextureType.CameraTarget, _material);
            _buffer.ReleaseTemporaryRT(_tempTextureIdentifier);
        }
    }
}