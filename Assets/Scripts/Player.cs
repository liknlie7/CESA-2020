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
    Animator _animator;

    // 回転速度
    [SerializeField,Range(0.0f,10.0f)]
    private float _rotateSpeed; 

    private NavMeshAgent _agent;

    void Start()
    {
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        //在より少し前の位置を保存
        playerPos = transform.position;
        // Animator取得
        _animator = transform.GetChild(0).GetComponent<Animator>();

        _agent = this.GetComponent<NavMeshAgent>();
        _agent.SetDestination(this.transform.position);
    }

    void FixedUpdate()
    {
        Move();
    }

    // プレイヤーの回転
    void Move()
    {
        var plane = new Plane(Vector3.up, Vector3.zero);
        var ray = CameraManager.Get().sonarCamera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            var target = ray.GetPoint(enter);
            if (_agent.remainingDistance <= 0.3f)
            {
                Vector3 targetDir = target - transform.position;
                targetDir.y = transform.position.y;

                float speed = _rotateSpeed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, speed, 0.0F);
                transform.rotation = Quaternion.LookRotation(newDir);
                
                //_animator.SetBool("Swiming", false);
            }

            if (Input.GetMouseButtonDown(0))
                _agent.SetDestination(target);
        }

    }
}

