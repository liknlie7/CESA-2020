using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDestroyed : EnemyState
{
    // ステートの名前を取得
    public override string GetStateName() { return "PlayerDestroyedState"; }

    // ステートが遷移してきたとき
    public override void EnterEvent() { }

    // ステートを実行
    public override void Execute() { }

    // ステートから出ていくとき
    public override void ExitEvent() { }
}
