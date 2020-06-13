using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartSound : MonoBehaviour
{
    public AudioSource audioSource;

    // Update is called once per frame
    void PlayStartSound()
    {
        //音を鳴らす
        audioSource.Play();
    }
}
