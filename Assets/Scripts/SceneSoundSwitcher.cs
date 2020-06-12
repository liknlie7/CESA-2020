using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSoundSwitcher : MonoBehaviour
{
    public Animator anim;

    private int titleSceneID;
    private int selectSceneID;
    private static readonly int Scene = Animator.StringToHash("Scene");

    private void Start()
    {
        titleSceneID = SceneManager.GetSceneByName("TitleScene").buildIndex;
        selectSceneID = SceneManager.GetSceneByName("StageSelectScene").buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        var sceneID = SceneManager.GetActiveScene().buildIndex;
        if (sceneID == titleSceneID)
            anim.SetInteger(Scene, 0);
        else if (sceneID == selectSceneID)
            anim.SetInteger(Scene, 1);
        else
            anim.SetInteger(Scene, 2);
    }
}
