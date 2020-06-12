using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartSound : MonoBehaviour
{
    public CanvasGroup image;
    bool flag;
    public AudioSource audioSource;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (!flag && image.alpha >= 1)
        {
            flag = true;

            //音を鳴らす
            audioSource.Play();

            animator.SetBool("Enabled", false);
        }
    }

}
