using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sound = null;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        audioSource.PlayOneShot(sound[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
