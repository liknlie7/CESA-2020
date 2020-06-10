// 2020/6/10
// 西村優希
// プレイヤーの回転方法の変更/プレイヤーの移動方向を変更

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
 　 // Rigidbodyを変数に入れる
    Rigidbody rb;
    // 移動スピード
    [SerializeField]
    private float _moveForce = 3.0f;
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

    void FixedUpdate()
    {

        //A・Dキー、←→キーで横移動
        //float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;


        //W・Sキー、↑↓キーで前後移動
        //float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        //現在の位置＋入力した数値の場所に移動する
        //rb.MovePosition(transform.position + new Vector3(x, 0, z));
        
        Rotation();

        if (Input.GetKey(KeyCode.W))
        {
            float torque = _moveForce;
            Vector3 vel = transform.forward * Time.deltaTime * torque;
            rb.MovePosition(this.transform.position + vel);
        }



    }

    // プレイヤーの回転
    void Rotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 direction = hit.point - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
    }

}

