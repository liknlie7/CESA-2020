using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // プレイヤー操作のスクリプト
    [SerializeField]
    Player player;

    // ボタン
    private GameObject menuObject;

    // Start is called before the first frame update
    void Start()
    {
        menuObject = FixedManager.Get().menuCanvas;
    }

    // Update is called once per frame
    void Update()
    {
        //ゴールしたなら
        if (player.GetGoalFlag())
        {
            // メニューの表示
            menuObject.SetActive(true);
            FixedManager.Get().scoreManager.SaveScore();
        }
    }
}

