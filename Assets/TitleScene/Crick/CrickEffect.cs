using UnityEngine;

public class CrickEffect : MonoBehaviour
{
  
    [SerializeField]
    ParticleSystem tapEffect;              // タップエフェクト
    [SerializeField]
    Camera _camera;                        // カメラの座標

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
            tapEffect.transform.position = pos;
            tapEffect.Emit(1);
        }
    }
}
