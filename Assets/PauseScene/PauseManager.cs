// ポーズ画面を管理するスクリプト
// 2020/06/11
// 佐竹晴登

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // ポーズ対象のオブジェクトリスト
    static List<GameObject> targets = new List<GameObject>();

    // タイトルシーンの名前
    [SerializeField] string titleSceneName = "TitleScene";

    // ステージセレクトシーンの名前
    [SerializeField] string stageSelectSceneName = "StageSelectScene";
    // Start is called before the first frame update

    public GameObject pauseObject;
    public Animator pauseAnim;
    public Animator pauseBlur;
    private static readonly int Enabled = Animator.StringToHash("Enabled");

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }

    public void Toggle()
    {
        if (!pauseAnim.GetBool(Enabled))
            Pause();
        else
            Resume();
    }

    // ゲーム停止
    public void Pause()
    {
        if (SceneManager.GetActiveScene().buildIndex < 2)
            return;

        // オブジェクトにアタッチされているレンダラー以外を停止させる
        FixedManager.Get().enemyManager.StopAllEnemy();

        // ポーズシーンの呼び出し
        //Application.LoadLevelAdditiveAsync("PauseScene");
        pauseObject.SetActive(true);
        pauseAnim.SetBool(Enabled, true);
        pauseBlur.SetBool(Enabled, true);
    }

    // 再開
    void Resume()
    {
        //Debug.Log("りじゅむ");
        // オブジェクトにアタッチされているレンダラー以外を再開させる
        FixedManager.Get().enemyManager.Resume();

        //SceneManager.UnloadSceneAsync("PauseScene");
        //pauseObject.SetActive(false);
        pauseAnim.SetBool(Enabled, false);
        pauseBlur.SetBool(Enabled, false);
    }

    // ゲームにもどるボタンを押された時
    public void PushBacktoGameButton()
    {
        Resume();
    }

    public void PushRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ステージセレクトボタンを押された時
    public void PushStageSelectButton()
    {
        SceneManager.LoadScene(stageSelectSceneName);
    }

    // タイトルへ戻るを押された時
    public void PushBacktoTitle()
    {
        SceneManager.LoadScene(titleSceneName);
    }
}