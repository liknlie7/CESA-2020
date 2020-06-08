using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverStaging : MonoBehaviour
{
    private bool _isGameOver = false; // ゲームオーバーになったか

    [SerializeField]
    private float _speed; // スローモーションの速度
    

    // Update is called once per frame
    void Update()
    {
        // スローモーション
        if (_isGameOver && Time.timeScale >= _speed)
        {
            if (Time.timeScale - _speed <= _speed)
                Time.timeScale = 0.0f;
            else
                Time.timeScale -= _speed;
        }
        
        if (Time.timeScale == 0.0f)
        {
            GameOverProduction();
        }

        

    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    //ゲームオーバーの処理を書く
    void GameOverProduction()
    {
        Debug.Log("ゲームオーバー");
    }
}
