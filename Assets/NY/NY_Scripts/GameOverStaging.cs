﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverStaging : MonoBehaviour
{
    // チュートリアルで読み取れるようにするため(2020/06/13 佐竹)
    public bool _isGameOver = false; // ゲームオーバーになったか

    [SerializeField]
    private float _speed; // スローモーションの速度

    [SerializeField]
    GameObject GameOverUi = null;

    [SerializeField]
    Animator Blur = null;

    // Update is called once per frame
    void Update()
    {
        //// スローモーション
        //if (_isGameOver && Time.timeScale >= _speed)
        //{
        //    if (Time.timeScale - _speed <= _speed)
        //        Time.timeScale = 0.0f;
        //    else
        //        Time.timeScale -= _speed;

        //}
        
        //if (Time.timeScale == 0.0f)
        //{
        //    GameOverProduction();
        //}

        

    }

    public void GameOver()
    {
        if(!_isGameOver)
            _isGameOver = true;

        // 敵を全て停止させる
        FixedManager.Get().enemyManager.StopAllEnemy();
        // マウスカーソルを表示する
        Cursor.visible = true;
        GameOverProduction();
    }

    //ゲームオーバーの処理を書く
    void GameOverProduction()
    {
        GameOverUi.SetActive(true);
        Blur.SetBool("Enabled", true);
    }
}
