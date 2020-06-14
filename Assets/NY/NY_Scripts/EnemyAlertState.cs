using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAlertState : EnemyState
{
    private float _alertGauge         = 0.0f;
    private float _attentionTime      = 0.0f;
    private float _crawlAttentionTime = 8.0f;

    const float ALERT_GAUGE_MAX = 4.0f;


    public override string GetStateName() { return "EnemyAlertState"; }

    // ステートが遷移してきたとき
    public override void EnterEvent()
    {
        _alertGauge = 0.0f;
        
        _prop.detectParticle.Play();
        _prop.detectSound.Play();
    }


    public override void Execute()
    {
        // プレイヤーを見つけていたら
        if (IsSearch())
        {
            // 警戒ゲージを増加
            _alertGauge += Time.deltaTime;
            _prop.Agent.SetDestination(_prop.PlayerTrs.position);
            // 注意時間をリセット
            //if (StateController)
            //    _currentState = STATE.TRACKING;
            //else
                _attentionTime = _crawlAttentionTime;
        }
        else
            _alertGauge = 0.0f;

        // もし、プレイヤーが居た付近に着いたら、周りを見渡す
        if (_prop.Agent.remainingDistance <= 0.1f)
        {
            Vector3 rotate = new Vector3(0.0f, Mathf.Cos(Time.time) * 0.5f, 0.0f);
            this.transform.Rotate(rotate);
            _attentionTime -= Time.deltaTime;
        }

        // 警戒時間が経過すると巡回ステートへ
        if (_attentionTime <= 0.0f)
            StateController.SetState(_nextStateName);
        

        // 一艇時間プレイヤーを見続けると追跡ステートへ
        if (_alertGauge >= ALERT_GAUGE_MAX)
            StateController.SetState("EnemyTrackingState");
        
    }

    // ステートから出ていくとき
    public override void ExitEvent()
    {
    }


    bool IsSearch()
    {
        Vector3 playerPos = _prop.PlayerTrs.position;
        Vector3 dir = StateController.transform.forward;

        // プレイヤーが視野範囲内に入ったら
        if (Vector3.Angle((playerPos - StateController.transform.position).normalized, dir) <= _prop.FovAngle / 2)
            return IsPlayerSee();

        return false;
    }

    // プレイヤーを障害物なしに視認できたか
    bool IsPlayerSee()
    {
        // プレイヤーを障害物なしに視認できたか
        Vector3 playerPos = _prop.PlayerTrs.position;
        Vector3 searchDire = playerPos - StateController.transform.position;
        Ray ray = new Ray(StateController.transform.position, searchDire.normalized);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _prop.FovLength))
        {
            if (hit.collider.gameObject.tag == "Player")
                return true;
        }
        return false;
    }
}
