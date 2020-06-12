using UnityEngine;

public class AnimatedProjector : MonoBehaviour
{
    public float fps = 30.0f;
    public Texture2D[] frames;

    private int frameIndex;
    private Material projector;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private static readonly int AlphaTex = Shader.PropertyToID("_AlphaTex");

    //Starts the actuall function
    void Start()
    {
        projector = GetComponent<Renderer>().material;
        NextFrame();
        InvokeRepeating(nameof(NextFrame), 1 / fps, 1 / fps);
    }

    void NextFrame()
    {
        projector.SetTexture(MainTex, frames[frameIndex]);
        projector.SetTexture(AlphaTex, frames[frameIndex]);
        frameIndex = (frameIndex + 1) % frames.Length;
    }
}