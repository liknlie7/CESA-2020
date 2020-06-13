using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSoundSwitcher : MonoBehaviour
{
    public Animator anim;

    private static readonly int Scene = Animator.StringToHash("Scene");

    // Update is called once per frame
    void Update()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "TitleScene")
            anim.SetInteger(Scene, 0);
        else if (sceneName == "StageSelectScene")
            anim.SetInteger(Scene, 1);
        else if (FixedManager.Get().scoreManager.goaled)
            anim.SetInteger(Scene, 3);
        else
            anim.SetInteger(Scene, 2);
    }
}
