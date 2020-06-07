using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource[1].Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
