using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 準警戒状態
public class EnemyRunawayState : EnemyState
{
    // TODO:: 沈むアニメーションが追加されるとこのコードも変更する

    [SerializeField]
    private float _sinkSpeed = 0.1f; // 沈む速度

    private float _timer = 0.0f; // Cosカーブに使うためのタイマー
    private const float TIMER_RATE = 4.0f; //　タイマーの倍率
    private Renderer _render;

    [SerializeField]
    private SonarPulse _sonar;

    // ステートの名前を取得
    public override string GetStateName() { return "EnemyRunawayState"; }

    // ステートが遷移してきたとき
    public override void EnterEvent()
    {
        this.GetComponent<NavMeshAgent>().enabled = false;
        _render = this.GetComponent<Renderer>();

        // 波紋を発生
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CameraManager.Get().sonarFx.Pulse(this.transform.position, _sonar, player);
    }

    // ステートを実行
    public override void Execute()
    {
        _timer += Time.deltaTime;
        Vector3 pos = new Vector3(0.0f, Mathf.Cos(_timer * TIMER_RATE) * _sinkSpeed, 0.0f);
        this.transform.Translate(pos);

        // カメラが外に沈むと消える
        if (_timer * TIMER_RATE > 3.14 * 1.5f)
            this.gameObject.SetActive(false);
    }

    // ステートから出ていくとき
    public override void ExitEvent() { }

    void OnTriggerEnter(Collider other)
    {
        // プレイヤーのソナーかプレイヤーに衝突したらEnemyRunawayStateに移行
        if (StateController.GetStateName() != "EnemyRunawayState" && (other.tag == "Player" || other.tag == "PlayerSonar"))
            StateController.SetState("EnemyRunawayState");
    }
}
