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
    [SerializeField]
    string titleSceneName = "TitleScene";
    // ステージセレクトシーンの名前
    [SerializeField]
    string stageSelectSceneName = "StageSelectScene";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Scene scene = SceneManager.GetSceneByName("PauseScene");
            if (!scene.IsValid())
            {

                Pause();
                
            }
            else
            {
                Resume();
            }
        }
    }
    // ゲーム停止
    void Pause()
    {
        // オブジェクトにアタッチされているレンダラー以外を停止させる
        foreach(var g in targets)
        {
            Behaviour[] behavs = g.GetComponentsInChildren<Behaviour>();
            foreach(var b in behavs)
            {
                b.enabled = false;
            }
        }
        // ポーズシーンの呼び出し
        Application.LoadLevelAdditiveAsync("PauseScene");
    }
    // 再開
    void  Resume()
    {
        //Debug.Log("りじゅむ");
        // オブジェクトにアタッチされているレンダラー以外を再開させる
        foreach (var g in targets)
        {
            Behaviour[] behavs = g.GetComponentsInChildren<Behaviour>();
            foreach (var b in behavs)
            {
                b.enabled = true;
            }
        }
        SceneManager.UnloadSceneAsync("PauseScene");
    }
    // オブジェクトをリストに追加
    public void AddPauseTarget(GameObject go)
    {
        targets.Add(go);
    }
    // ゲームにもどるボタンを押された時
    public void PushBacktoGameButton()
    {
        Resume();
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
