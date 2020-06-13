using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // 現在のスコア
    public int _score;
    // ゴールしたか
    [HideInInspector]
    public bool goaled;

    private string KEY_NUM = "_tadpoleNum";
    private string KEY_CLEARED = "_cleared";

    public AudioSource scoreSound;
    public AudioSource goalSound;

    void Start()
    {
        _score = 0;
    }

    // スコアを保存(最高スコアが更新されたら上書きする)
    public void SaveScore()
    {
        string sname = SceneManager.GetActiveScene().name;
        string keyNum = sname + KEY_NUM;
        string keyCleared = sname + KEY_CLEARED;

        // クリア判定
        PlayerPrefs.SetInt(keyCleared, 1);
        // スコアを更新
        int highScore = PlayerPrefs.GetInt(keyNum);
        if (highScore < _score)
            PlayerPrefs.SetInt(keyNum, _score);
    }

    public void Goal()
    {
        goalSound.Play();
        goaled = true;
        SaveScore();
    }

    // スコアを追加
    public void AddScore()
    {
        _score++;
        scoreSound.Play();
    }
}