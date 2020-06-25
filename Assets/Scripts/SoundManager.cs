using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private FMOD.Studio.EventInstance instance;

    [SerializeField] [Range(0f, 3f)] private float scene = default;

    void Start()
    {
        instance = GetComponent<StudioEventEmitter>().EventInstance;
    }

    void Update()
    {

        instance.setParameterByName("Scene", scene);
    }
}
