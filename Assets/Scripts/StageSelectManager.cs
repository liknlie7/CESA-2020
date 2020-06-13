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

    public AudioSource clickSound;

    public void OnBack()
    {
        clickSound.Play();
        StartCoroutine(ExecuteAfterTime($"TitleScene", 3));
    }

    public void OnStageButtonClicked()
    {
        clickSound.Play();
        StartCoroutine(ExecuteAfterTime($"Stage{activeId}", 3));
    }

    IEnumerator ExecuteAfterTime(string scene, float time)
    {
        fadeOut.SetBool("Enabled", false);
        blurFadeOut.SetBool("Enabled", false);
        DontDestroyOnLoad(ui);
        SceneManager.LoadScene(scene);
        yield return new WaitForSeconds(time);
        Destroy(ui);
    }
}
