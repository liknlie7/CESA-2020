using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStopState : EnemyState
{

    public override string GetStateName() { return "EnemyStopState"; }

    private bool _isPlayerDiscovery;

    private Vector3 _initPos;

    [SerializeField]
    private bool _isSemiAlertState = true;

    private const string SONAR_TAG_NAME = "PlayerSonar";

    [SerializeField]
    private float _killLengrh = 0; // 確殺距離の半径


    private float _interval = 3.0f;
    private float _intervalMax = 3.0f;


    void Start()
    {
        // 初期位置を設定
        _initPos = this.transform.position;
        _prop.Agent.SetDestination(_initPos);
    }
    // ステートが遷移してきたとき
    public override void EnterEvent()
    {
        _prop.Agent.SetDestination(_initPos);
    }

    public override void Execute()
    {
        Vector3 playerPos = _prop.PlayerTrs.position;
        float distance = Vector3.Distance(playerPos, StateController.transform.position);
        // プレイヤーとの距離が一定範囲内なら索敵開始
        if (distance < _killLengrh)
            _isPlayerDiscovery = true;
        else
            _isPlayerDiscovery = false;

        // プレイヤーを見つけていたら警戒状態に移行
        if (_interval <= _intervalMax)
        {
            _interval += Time.deltaTime * 0.5f;
            Debug.Log(_interval);
        }
        // プレイヤーを見つけたら警戒ステートに変更
        if (_isPlayerDiscovery && _interval >= _intervalMax)
        {
            StateController.SetState(_nextStateName);
            _interval = 0.0f;

            Debug.Log("Stop 2 Alert");

            _prop.Alert();
        }

    }

    // ステートから出ていくとき
    public override void ExitEvent() { }

    void OnTriggerEnter(Collider other)
    {
        // 巡回状態で波紋が自身に触れたら準警戒状態に移行
        if (other.gameObject.tag == SONAR_TAG_NAME && StateController.GetStateName() == GetStateName())
        {
            if (_isSemiAlertState)
            {
                _prop.TargetTrs = other.transform;
                StateController.SetState("EnemySemiAlertState");
            }
            else
                StateController.SetState(_nextStateName);

        }
    }
}
