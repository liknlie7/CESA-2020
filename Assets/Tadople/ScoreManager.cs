using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // 現在のスコア
    public int _score;
    private string KEY = "_tadpoleNum";

    void Start()
    {
        _score = 0;
    }

    // スコアを保存(最高スコアが更新されたら上書きする)
    public void SaveScore()
    {
        string key = SceneManager.GetActiveScene().name + KEY;
        int highScore = PlayerPrefs.GetInt(key);
        // スコアを更新
        if(highScore<_score)
            PlayerPrefs.SetInt(key ,_score);
    }

    // スコアを追加
    public void AddScore()
    {
        _score++;
    }
}
