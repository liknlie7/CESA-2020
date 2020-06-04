using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Appear : MonoBehaviour
{
    public Text Qtext;
    bool Flag;
    float a_color;
    public AudioClip sound1;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Flag = false;
        a_color = 0;
        //SE
        audioSource = GetComponent<AudioSource>(); 
        
    }

    // Update is called once per frame
    void Update()

    {          
        //テキストの透明度を変更する
        Qtext.color = new Color(255, 246, 0, a_color);
        if (Flag)
            a_color -= Time.deltaTime;
        else
            a_color += Time.deltaTime;
        if (a_color < 0.1)
        {
            a_color = 0;
            Flag = false;
        }
        else if (a_color > 1)
        {
            a_color = 1;
            Flag = true;
            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound1);
        }
    }

}
