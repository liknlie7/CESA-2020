using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTrackingState : EnemyState
{
    [SerializeField]
    private GameObject _discoveryObj;
    public override string GetStateName() { return "EnemyTrackingState"; }

    // ステートが遷移してきたとき
    public override void EnterEvent()
    {
        if (_prop.Agent == null)
            _prop.Agent = StateController.GetComponent<NavMeshAgent>();
        _prop.Agent.SetDestination(_prop.PlayerTrs.position);

        _discoveryObj.SetActive(true);

        Debug.Log("EnemyTrackingState : に移行");
    }

    public override void Execute()
    {
        Move();

        if (!IsPlayerSee())
            StateController.SetState(_nextStateName);

    }

    // ステートから出ていくとき
    public override void ExitEvent()
    {
        _discoveryObj.SetActive(false);
    }

    // プレイヤーを障害物なしに視認できたか
    bool IsPlayerSee()
    {
        // プレイヤーを障害物なしに視認できたか
        Vector3 playerPos  = _prop.PlayerTrs.position;
        Vector3 searchDire = playerPos - this.transform.position;
        Ray ray = new Ray(this.transform.position, searchDire.normalized);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _prop.FovLength))
        {
            if (hit.collider.gameObject.tag == "Player")
                return true;
        }
        return false;
    }


    void Move()
    {
        _prop.Agent.SetDestination(_prop.PlayerTrs.position);
    }

}
