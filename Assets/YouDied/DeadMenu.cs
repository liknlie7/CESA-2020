using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    [SerializeField] private string stageSelectSceneName;

    public AudioSource clickSound;
    // ステージセレクトボタンを押された時
    public void PushStageSelectButton()
    {
        clickSound.Play();
        SceneManager.LoadScene(stageSelectSceneName);
    }

    // リトライを押された時
    public void PushRetry()
    {
        clickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
