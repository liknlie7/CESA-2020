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
    // Animator
    Animator anim;
 
    void Start()
    {
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        //在より少し前の位置を保存
        playerPos = transform.position;
        // Animator取得
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        //A・Dキー、←→キーで横移動
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;


        //W・Sキー、↑↓キーで前後移動
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        //現在の位置＋入力した数値の場所に移動する
        rb.MovePosition(transform.position + new Vector3(x, 0, z));

        //最新の位置から少し前の位置を引いて方向を割り出す
        Vector3 direction = transform.position - playerPos;

        //移動距離が少しでもあった場合に方向転換
        if (direction.magnitude > 0.01f)
        {
            //directionのX軸とZ軸の方向を向かせる
            transform.rotation = Quaternion.LookRotation(new Vector3
                (direction.x, 0, direction.z));

        }


        //位置を更新する
        playerPos = transform.position;
    }
}

