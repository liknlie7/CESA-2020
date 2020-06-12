using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private FMOD.Studio.EventInstance instance;

    [SerializeField] [Range(0f, 2f)] private float scene;

    void Start()
    {
        instance = GetComponent<StudioEventEmitter>().EventInstance;
    }

    void Update()
    {

        instance.setParameterByName("Scene", scene);
    }
}
