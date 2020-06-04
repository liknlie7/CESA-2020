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

    // ステートの名前を取得
    public override string GetStateName() { return "EnemyRunawayState"; }

    // ステートが遷移してきたとき
    public override void EnterEvent()
    {
        Debug.Log("RunawayState : に移行");
        this.GetComponent<NavMeshAgent>().enabled = false;
        _render = this.GetComponent<Renderer>();
        // TODO:: ここで波紋を出す
    }

    // ステートを実行
    public override void Execute()
    {
        _timer += Time.deltaTime;
        Vector3 pos = new Vector3(0.0f, Mathf.Cos(_timer * TIMER_RATE) * _sinkSpeed, 0.0f);
        this.transform.Translate(pos);

        // カメラが外に沈むと消える
        // if (_render.isVisible == false)
        //   Destroy(this.gameObject);
        // カメラが外に沈むと消える
        if (_timer * TIMER_RATE > 3.14 * 1.5f)
            Destroy(this.gameObject);
    }

    // ステートから出ていくとき
    public override void ExitEvent() { }
}
