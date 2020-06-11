using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public int activeId = 1;
    public GameObject ui;
    public Animator fadeOut;
    public Animator blurFadeOut;

    public void OnStageButtonClicked()
    {
        StartCoroutine(ExecuteAfterTime(3));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        fadeOut.SetBool("Enabled", false);
        blurFadeOut.SetBool("Enabled", false);
        DontDestroyOnLoad(ui);
        SceneManager.LoadScene($"Stage{activeId}");
        yield return new WaitForSeconds(time);
        Destroy(ui);
    }
}
