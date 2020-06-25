// 2020/6/10
// 西村優希
// プレイヤーの回転方法の変更/プレイヤーの移動方向を変更

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    // Rigidbodyを変数に入れる
    Rigidbody rb;

    // 移動スピード
    //[SerializeField] private float _moveForce = 3.0f;

    [SerializeField] private GameObject fxPrefab = null;

    [SerializeField] private ParticleSystem locusFx = null;

    // 親子関係にしたくないのでインスタンスします
    [SerializeField] private GameObject _destinationPrefab = null;
    private GameObject _destination = null;

    // プレイヤーの位置を入れる
    Vector3 playerPos;

    // Animator
    Animator _animator;

    // 回転速度
    [SerializeField, Range(0.0f, 10.0f)] private float _rotateSpeed = 0.0f;

    private NavMeshAgent _agent;

    // ゴールとの接触
    bool goalFlag = false;
    //泳ぐときの音をながす
    //bool swim = false;
    //public bool Swimming { set { swim = value; } get { return swim; } }

    // 生きているか
    private bool _isAlive = true;
    //private float _destroyScaleSpeed = 5.0f;
    //private float _destroyRotateSpeed = 200.0f;


    private PlayerSEController _playerSE;

    private Vector3 target;

    void Start()
    {
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        //在より少し前の位置を保存
        var position = transform.position;
        playerPos = position;
        // Animator取得
        _animator = transform.GetChild(0).GetComponent<Animator>();

        _agent = this.GetComponent<NavMeshAgent>();
        _agent.SetDestination(position);

        _playerSE = this.GetComponent<PlayerSEController>();

        // 親子関係にしたくないのでインスタンスします
        _destination = Instantiate(_destinationPrefab);
        _destination.SetActive(false);
    }

    void Update()
    {
        var plane = new Plane(Vector3.up, Vector3.zero);
        var ray = CameraManager.Get().sonarCamera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            target = ray.GetPoint(enter);


            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                _agent.SetDestination(target);
                _playerSE.audioSource.PlayOneShot(_playerSE.MovePointSE);

                // 移動先を記すオブジェクトの位置を変更
                _destination.transform.position = target;
                _destination.SetActive(true);
            }
        }
    }

    void FixedUpdate()
    {
        if (_isAlive)
            Move();
        // もししんでいるのならプレイヤーを消滅させる
        else
            PlayerDisappearance();
    }

    // プレイヤーの回転
    void Move()
    {

        // ほぼ停止していたら移動先印のオブジェクトを非表示にする

        // 停止処理
        if (_agent.remainingDistance <= 0.5f)
        {
            Vector3 targetDir = target - transform.position;
            targetDir.y = transform.position.y;

            float speed = _rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, speed, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);


            if(_animator.GetBool("Swimming"))
                _destination.SetActive(false);

            _animator.SetBool("Swimming", false);
            //Swimming = false;
            // 泳ぐSE停止
            //_playerSE.SwimSE.Stop();
            _playerSE.SwimSE.SetBool("Enabled", false);

            locusFx.Stop();
        }
        // 泳いでいる処理
        else
        {
            _animator.SetBool("Swimming", true);
            // 泳ぐSE再生
            //if (!Swimming)
            //    _playerSE.SwimSE.Play();
            //Swimming = true;
            _playerSE.SwimSE.SetBool("Enabled", true);

            if (!locusFx.isPlaying)
            {
                locusFx.Play();
                // 移動先印オブジェクトを表示させる
                _destination.SetActive(true);
            }
        }
    }

    // ゴールとの当たり判定
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            goalFlag = true;

            // 敵を全て停止させる
            FixedManager.Get().enemyManager.StopAllEnemy();

            // プレイヤー関連を全て停止
            this.transform.GetChild(0).GetComponent<Collider>().enabled = false;
            this.GetComponent<NavMeshAgent>().speed = 0.0f;
            this.GetComponent<ThrowingScript>().enabled = false;

            // カーソルの表示
            Cursor.visible = true;
            // メニューの表示
            FixedManager.Get().menuCanvas.SetActive(true);
            FixedManager.Get().scoreManager.Goal();
            FixedManager.Get().intensityManager.intensityTo = 1;
        }

        // TODO:: 長い　できればrippleSphereを編集して"RunawayEnemy"のタグも光らせるようにしたい
        if (other.CompareTag("Enemy"))
        {
            FixedManager.Get().gameOverStaging.GameOver();
            this.transform.GetChild(0).GetComponent<Collider>().enabled = false;

            _isAlive = false;

            // プレイヤーを停止
            this.GetComponent<NavMeshAgent>().speed = 0.0f;
            Instantiate(fxPrefab, this.transform.position, Quaternion.identity);

            locusFx.transform.parent = new GameObject().transform;
            //locusFx.GetComponent<ParticleSystem>().Stop();
            locusFx.Stop();
        }
    }

    // ゴール判定の取得
    public bool GetGoalFlag()
    {
        return goalFlag;
    }

    // プレイヤーを消滅させる
    private void PlayerDisappearance()
    {
        Vector3 scale = this.transform.localScale;
        //float sub = _destroyScaleSpeed * Time.deltaTime;
        //if (scale.x - sub < 0)
        Destroy(this.gameObject);
        //else
        {
            //    this.transform.localScale = new Vector3(scale.x - sub, scale.y - sub, scale.z - sub);
            //    this.transform.Rotate(new Vector3(0.0f, Time.deltaTime * _destroyRotateSpeed, 0.0f));
        }
    }
}