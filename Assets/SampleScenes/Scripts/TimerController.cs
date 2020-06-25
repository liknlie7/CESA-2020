using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    public float totalTime = 0.0f;
    int seconds = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;
        seconds = (int)totalTime;

        //if (seconds < 0)
        //    ; //UnityEditor.EditorApplication.isPlaying = false;

    }
}
