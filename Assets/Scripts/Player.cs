// 2020/6/10
// 西村優希
// プレイヤーの回転方法の変更/プレイヤーの移動方向を変更

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private NavMeshAgent _agent;

    void Start()
    {
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        //在より少し前の位置を保存
        playerPos = transform.position;
        // Animator取得
        anim = GetComponent<Animator>();

        _agent = this.GetComponent<NavMeshAgent>();
        _agent.SetDestination(this.transform.position);
    }

    void FixedUpdate()
    {
        Rotation();

        //if (Input.GetKey(KeyCode.W))
        //{
        //    float torque = _moveForce;
        //    Vector3 vel = transform.forward * Time.deltaTime * torque;
        //    rb.MovePosition(this.transform.position + vel);
        //}

        //if(Input.GetMouseButtonDown(0))
        //{
        //    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    _agent.SetDestination(pos);
        //    Debug.Log(pos + " / " + this.transform.position);
        //}

    }

    // プレイヤーの回転
    void Rotation()
    {
        var plane = new Plane(Vector3.up, Vector3.zero);
        var ray = CameraManager.Get().sonarCamera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            var target = ray.GetPoint(enter);
            if (_agent.remainingDistance <= 0.3f)
            {
                Vector3 direction = target - this.transform.position;
                //float rad = Mathf.Atan2(direction.x, direction.z);


                this.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            }

            if (Input.GetMouseButtonDown(0))
                _agent.SetDestination(target);
        }

    }
}

