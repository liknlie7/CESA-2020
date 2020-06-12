using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunawayEnemyAnimationController : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _animator = this.transform.GetChild(0).GetComponent<Animator>();
        _agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = _agent.velocity.magnitude;
        _animator.SetFloat("speed", speed);
    }
}
