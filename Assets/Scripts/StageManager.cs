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
    public Button nextButton;
    public Button selectButton;
    // 画像
    public Image stageClear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ゴールしたなら
        if (player.GetGoalFlag())
        {
            // ボタンの表示
            nextButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(true);
            // 画像の表示
            stageClear.gameObject.SetActive(true);
        }

    }
}

