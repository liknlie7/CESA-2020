using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    [SerializeField] private string stageSelectSceneName;

    // ステージセレクトボタンを押された時
    public void PushStageSelectButton()
    {
        SceneManager.LoadScene(stageSelectSceneName);
    }

    // リトライを押された時
    public void PushRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
