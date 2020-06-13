using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    private EnemyState _state = null; // 現在実行しているステート
    [SerializeField]
    private string _startStateName = "StateName";

    private List<EnemyState> _stateList = new List<EnemyState>(); // ステート一覧

    // Start is called before the first frame update
    void Start()
    {
        // 一番のステートを実行
        SetState(_startStateName);
        foreach (EnemyState state in _stateList)
            state.StateController = this;
    }

    // Update is called once per frame
    void Update()
    {
        // ステートを実行
        _state.Execute();
    }

    // ステートを変更
    public void SetState(string stateName)
    {
        foreach (EnemyState state in _stateList)
        {
            if (state.GetStateName() == stateName)
            {
                if (_state != null)
                    _state.ExitEvent();
                _state = state;
                _state.EnterEvent();
                return;
            }
        }
    }

    // ステートを追加
    public void RegisterStateList(EnemyState state)
    {
        _stateList.Add(state);
    }

    // 現在のステートの名前を取得
    public string GetStateName()
    {
        return _state.GetStateName();
    }


    public void StopNavMeshAgent()
    {
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
        agent.speed = 0;
    }
}
