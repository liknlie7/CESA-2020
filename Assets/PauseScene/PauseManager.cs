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

    public bool showCursor;

    public AudioSource clickSound;

    void Start()
    {
        // マウスカーソルを非表示にする
        Cursor.visible = showCursor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }

    public void Toggle()
    {
        bool b;
        if (!pauseAnim.GetBool(Enabled))
            b = Pause();
        else
            b = Resume();
        if (b)
            clickSound.Play();
    }

    // ゲーム停止
    public bool Pause()
    {
        if (SceneManager.GetActiveScene().buildIndex < 2)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadScene(0);
                return true;
            }

            return false;
        }

        if (FixedManager.Get().enemyManager.isPaused)
            return false;

        // カーソルを表示する
        Cursor.visible = true;

        // オブジェクトにアタッチされているレンダラー以外を停止させる
        FixedManager.Get().enemyManager.StopAllEnemy();

        // ポーズシーンの呼び出し
        //Application.LoadLevelAdditiveAsync("PauseScene");
        pauseObject.SetActive(true);
        pauseAnim.SetBool(Enabled, true);
        pauseBlur.SetBool(Enabled, true);

        return true;
    }

    // 再開
    public bool Resume()
    {
        // カーソルを非表示にする
        Cursor.visible = showCursor;
        //Debug.Log("りじゅむ");
        // オブジェクトにアタッチされているレンダラー以外を再開させる
        FixedManager.Get().enemyManager.Resume();

        //SceneManager.UnloadSceneAsync("PauseScene");
        //pauseObject.SetActive(false);
        pauseAnim.SetBool(Enabled, false);
        pauseBlur.SetBool(Enabled, false);

        return true;
    }

    // ゲームにもどるボタンを押された時
    public void PushBacktoGameButton()
    {
        clickSound.Play();
        Resume();
    }

    public void PushRetryButton()
    {
        clickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ステージセレクトボタンを押された時
    public void PushStageSelectButton()
    {
        clickSound.Play();
        SceneManager.LoadScene(stageSelectSceneName);
    }

    // タイトルへ戻るを押された時
    public void PushBacktoTitle()
    {
        clickSound.Play();
        SceneManager.LoadScene(titleSceneName);
    }
}