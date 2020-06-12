using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverStaging : MonoBehaviour
{
    private bool _isGameOver = false; // ゲームオーバーになったか

    [SerializeField]
    private float _speed; // スローモーションの速度

    [SerializeField]
    GameObject GameOverUi = null;

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
        if(!_isGameOver)
            _isGameOver = true;
    }

    //ゲームオーバーの処理を書く
    void GameOverProduction()
    {
        if (GameObject.Find("GameOverUi(Clone)") == null)
        {
            GameObject Obj = Instantiate(GameOverUi);
            Obj.GetComponent<RectTransform>().position = new Vector2(0,0);
            Obj.transform.parent = GameObject.Find("Canvas").transform;
            Obj.SetActive(true);
        }
    }
}
