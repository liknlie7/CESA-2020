using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   
 　 // Rigidbodyを変数に入れる
    Rigidbody rb;
    // 移動スピード
    public float speed = 3.0f;
    // プレイヤーの位置を入れる
    Vector3 playerPos;

    //================================↓追加分===========================================

    // ゴールしたかどうか
    bool goalFlag;
    // オタマジャクシを拾ったかどうか
    bool tadpoleFlag;

   //=================================↑追加分===========================================

    void Start()
    {
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        //在より少し前の位置を保存
        playerPos = transform.position;

        //================================↓追加分===========================================


        // オタマジャクシ
        tadpoleFlag = false;
        // ゴール
        goalFlag = false;

        //=================================↑追加分===========================================

    }

    void Update()
    {

        //A・Dキー、←→キーで横移動
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

        //W・Sキー、↑↓キーで前後移動
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        //現在の位置＋入力した数値の場所に移動する
        rb.MovePosition(transform.position + new Vector3(x, 0, z));

        //ユニティちゃんの最新の位置から少し前の位置を引いて方向を割り出す
        Vector3 direction = transform.position - playerPos;

        //移動距離が少しでもあった場合に方向転換
        if (direction.magnitude > 0.05f)
        {
            //directionのX軸とZ軸の方向を向かせる
            transform.rotation = Quaternion.LookRotation(new Vector3
                (direction.x, 0, direction.z));

        }


        //位置を更新する
        playerPos = transform.position;
    }

    //=================================↓追加分===========================================

    // ゴールやオタマとの当たり判定
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            goalFlag = true;
        }

        if(other.gameObject.tag == "Tadpole")
        {
            tadpoleFlag = true;
           
        }
    }

    // ゴールしたフラグ
    public bool GetGoalFlag()
    {
        return goalFlag;
    }

    // オタマジャクシを拾ったフラグ
    public bool GetTadpoleFlag()
    {
        return tadpoleFlag;
    }

    // オタマを拾ったフラグの設定
    public void SetTadpoleFlag(bool flag)
    {
        tadpoleFlag = flag;
    }

    //=================================↑追加分===========================================

}

