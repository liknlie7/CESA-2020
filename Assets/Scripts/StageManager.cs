using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // プレイヤー
    [SerializeField]
    GameObject player;
    // ライト
    [SerializeField]
    new GameObject light;
    // オタマジャクシを拾った数
    int tadpoleCount;
    //Text用変数
    public Text countText;
    // ボタン
    public Button nextButton;
    public Button selectButton;
    // 画像
    public Image stageClear;
    public Image tadpole1;
    public Image tadpole2;
    public Image tadpole3;

    // Start is called before the first frame update
    void Start()
    {
        //// オタマジャクシを拾った数
        //tadpoleCount = 0;
        //// テキストの初期化
        //countText.text = "Count: 0";
        //// ボタンの非表示
        //nextButton.gameObject.SetActive(false);
        //selectButton.gameObject.SetActive(false);
        //// 画像の非表示
        //stageClear.gameObject.SetActive(false);
        //tadpole1.gameObject.SetActive(false);
        //tadpole2.gameObject.SetActive(false);
        //tadpole3.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // ゴールしたなら
        //if (player.GetGoalFlag())
        //{
        //    // ライトをONにする
        //    light.SetActive(true);

        //    // ボタンの表示
        //    nextButton.gameObject.SetActive(true);
        //    selectButton.gameObject.SetActive(true);
        //    // 画像の表示
        //    stageClear.gameObject.SetActive(true);
        //}
            //    if (tadpoleCount == 1)
            //    {
            //        tadpole1.gameObject.SetActive(true);
            //    }
            //    else if (tadpoleCount == 2)
            //    {
            //        tadpole1.gameObject.SetActive(true);
            //        tadpole2.gameObject.SetActive(true);
            //    }
            //    else if (tadpoleCount == 3)
            //    {
            //        tadpole1.gameObject.SetActive(true);
            //        tadpole2.gameObject.SetActive(true);
            //        tadpole3.gameObject.SetActive(true);
            //    }
            //}
            //// オタマジャクシを拾ったなら
            //if(player.GetTadpoleFlag())
            //{
            //    // カウントを増やす
            //    tadpoleCount += 1;
            //    countText.text = "Score: " + tadpoleCount.ToString();
            //    player.SetTadpoleFlag(false);
            //}
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("StageSelectScene");
        }
    }
}