using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Plane = UnityEngine.Plane;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.AI;

public class ThrowingScript : MonoBehaviour
{
    /// 射出するオブジェクト
    [SerializeField, Tooltip("射出するオブジェクトをここに割り当てる")]
    private GameObject ThrowingObject;

    [SerializeField, Range(0F, 60F), Tooltip("次に水を吐くためのインターバル")]
    private float _throwingIntervalMax;
    private float _throwingInterval = 0.0f;
    private bool _isThrowing = true;

    //// 標的のオブジェクト
    [SerializeField, Tooltip("標的のオブジェクトをここに割り当てる")]
    private GameObject targetFXPrefab;
    private GameObject targetFX;

    // 射出角度
    [SerializeField, Range(0F, 90F), Tooltip("射出する角度")]
    private float ThrowingAngle;

    // 射出位置オフセット
    [SerializeField, Tooltip("射出位置")]
    private GameObject ThrowingOffset;

    // 投げる位置の最大値
    [SerializeField]
    private float _throwLengthMax;

    [SerializeField]
    private Material[] _targetMaterials;
    enum TargetMaterialID
    { possible = 0, inpossible }

    enum State
    {
        ThrowingPossible = 0,
        ThrowingInpossible
    }
    private State _currentState = State.ThrowingPossible;

    [SerializeField]
    GameObject DRAG_PARTICLE;  // PS_DragStarを割り当てること
    private GameObject _dragParticle;

    private NavMeshAgent _agent;


    // Start is called before the first frame update
    void Start()
    {
        // 最初から水を吐ける状態にしておく
        _throwingInterval = _throwingIntervalMax;

        Collider collider = GetComponent<Collider>();

        targetFX = Instantiate(targetFXPrefab);

        _dragParticle = Instantiate(DRAG_PARTICLE);
        _dragParticle.GetComponent<ParticleSystem>().Play();

        _agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case State.ThrowingPossible: ThrowingPossibleState(); break;
            case State.ThrowingInpossible: ThrowingInpossibleState(); break;
            default: break;
        }

        // 水を吐くまでのインターバルを設ける
        if (!_isThrowing)
        {
            _throwingInterval += Time.deltaTime;
            if (_throwingInterval > _throwingIntervalMax)
                _isThrowing = true;
        }
    }
    // ボールを射出する
    private void ThrowingBall(Vector3 Target)
    {
        if (ThrowingObject)
        {
            // Ballオブジェクトの生成
            GameObject ball = Instantiate(ThrowingObject, ThrowingOffset.transform.position, Quaternion.identity);

            var water = ball.GetComponent<Water>();

            // 標的の座標
            Vector3 targetPosition = Target;

            // 射出角度
            float angle = ThrowingAngle;

            // 射出速度を算出
            Vector3 velocity = CalculateVelocity(ThrowingOffset.transform.position, targetPosition, angle);

            // 射出
            Rigidbody rid = ball.GetComponent<Rigidbody>();
            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);
        }
        else
        {
            throw new System.Exception("射出するオブジェクトが未設定です。");
        }
    }

    // 標的に命中する射出速度の計算
    /// <param name="pointA">射出開始座標</param>
    /// <param name="pointB">標的の座標</param>
    /// <returns>射出速度</returns>
    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        // 射出角をラジアンに変換
        float rad = angle * Mathf.PI / 180;

        // 水平方向の距離x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));

        // 垂直方向の距離y
        float y = pointA.y - pointB.y;

        // 斜方投射の公式を初速度について解く
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // 条件を満たす初速を算出できなければVector3.zeroを返す
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
        }
    }

    private void ThrowingPossibleState()
    {
        var plane = new Plane(Vector3.up, new Vector3(0.0f, 0.8f, 0.0f));
        var ray = CameraManager.Get().sonarCamera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            var target = ray.GetPoint(enter);

            // 射程距離を制限
            Vector3 distance = target - transform.position;
            if (distance.magnitude > _throwLengthMax)
            {
                // 色を変更する
                targetFX.transform.GetChild(0).GetComponent<Renderer>().material = _targetMaterials[(int)TargetMaterialID.inpossible];
                _currentState = State.ThrowingInpossible;
                _dragParticle.GetComponent<ParticleSystem>().Stop();
            }
            // 右クリックで水を吐き出す
            if (Input.GetMouseButtonDown(1) && _isThrowing && _agent.remainingDistance <= 0.1f)
            {
                // Fireボタンでボールを射出する
                ThrowingBall(target);

                // インターバルをリセットする
                _isThrowing = false;
                _throwingInterval = 0.0f;
            }
            targetFX.transform.position = target;
            Vector3 particlePos = target;
            _dragParticle.transform.position = particlePos;
        }
    }


    private void ThrowingInpossibleState()
    {
        var plane = new Plane(Vector3.up, new Vector3(0.0f, 0.8f, 0.0f));
        var ray = CameraManager.Get().sonarCamera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            var target = ray.GetPoint(enter);

            // 射程距離を制限
            Vector3 distance = target - transform.position;
            if (distance.magnitude < _throwLengthMax - 0.3f)
            {
                // 色を変更する
                targetFX.transform.GetChild(0).GetComponent<Renderer>().material = _targetMaterials[(int)TargetMaterialID.possible];
                _currentState = State.ThrowingPossible;
                _dragParticle.GetComponent<ParticleSystem>().Play();
            }
            targetFX.transform.position = target;
        }
    }
}
