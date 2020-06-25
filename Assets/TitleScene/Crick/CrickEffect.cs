using UnityEngine;
using UnityEngine.EventSystems;

public class CrickEffect : MonoBehaviour
{
  
    [SerializeField]
    ParticleSystem tapEffect = null;              // タップエフェクト
    [SerializeField]
    Camera _camera = null;                        // カメラの座標

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
            tapEffect.transform.position = pos;
            tapEffect.Emit(1);
        }
    }
}
