using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyState : MonoBehaviour
{
    //public int _stateNum;
    private string _nextStateName;
    private EnemyController _stateController;
    public EnemyController StateController { set { _stateController = value; } get { return _stateController; } }
    protected EnemyPropaty _prop; // 自身のプロパティ

    void Awake()
    {
        this.GetComponent<EnemyController>().RegisterStateList(this);
        _prop = this.GetComponent<EnemyPropaty>();
        //_nextStateName = EnemyStateEditor.NextStateName[_stateNum];
    }

    // ステートの名前を取得
    public virtual string GetStateName() { return "StateNone"; }

    // ステートが遷移してきたとき
    public virtual void EnterEvent(){ }

    // ステートを実行
    public virtual void Execute() { }

    // ステートから出ていくとき
    public virtual void ExitEvent(){ }
}
