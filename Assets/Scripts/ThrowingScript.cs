using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Plane = UnityEngine.Plane;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ThrowingScript : MonoBehaviour
{
    /// 射出するオブジェクト
    [SerializeField, Tooltip("射出するオブジェクトをここに割り当てる")]
    private GameObject ThrowingObject;

    [SerializeField,Range(0F,60F), Tooltip("次に水を吐くためのインターバル")]
    private float _throwingIntervalMax;
    private float _throwingInterval = 0.0f;
    private bool  _isThrowing       = true;

    //// 標的のオブジェクト
    //[SerializeField, Tooltip("標的のオブジェクトをここに割り当てる")]
    //private GameObject TargetObject;

    // 射出角度
    [SerializeField, Range(0F, 90F), Tooltip("射出する角度")]
    private float ThrowingAngle;

    // 射出位置オフセット
    [SerializeField, Tooltip("射出位置")]
    private GameObject ThrowingOffset;

    // Start is called before the first frame update
    void Start()
    {
        // 最初から水を吐ける状態にしておく
        _throwingInterval = _throwingIntervalMax;

        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            // 干渉しないようにisTriggerをつける
            collider.isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire") && _isThrowing)
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            var ray = CameraManager.Get().sonarCamera.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float enter))
            {
                var target = ray.GetPoint(enter);
                // Fireボタンでボールを射出する
                ThrowingBall(target);

                // インターバルをリセットする
                _isThrowing = false;
                _throwingInterval = 0.0f;
            }
        }

        // 水を吐くまでのインターバルを設ける
        if(!_isThrowing)
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
}
