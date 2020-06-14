using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPropaty : MonoBehaviour
{
    [SerializeField] private float        _fovAngle;  // 視野角
    [SerializeField] private float        _fovLength; // 視野の長さ
    [SerializeField] private NavMeshAgent _agent;     // 自身のナビメッシュエージェント
    private Transform _targetTrs; // 移動先のトランスフォーム
    private Transform _playerTrs; // プレイヤーのトランスフォーム
    public ParticleSystem detectParticle;
    public AudioSource detectSound;

    // 各プロパティ
    public float     FovAngle  { set { _fovAngle = value;}  get { return _fovAngle;  } }
    public float     FovLength { set { _fovLength = value;} get { return _fovLength; } }
    public NavMeshAgent Agent  { set { _agent = value; } get { return _agent; } }
    public Transform PlayerTrs {get { return _playerTrs; } }
    public Transform TargetTrs { set { _targetTrs = value; } get { return _targetTrs; } }

    void Start()
    {
        _playerTrs = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
