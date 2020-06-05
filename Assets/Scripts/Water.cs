using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // 波紋のコライダー
    [SerializeField]
    private GameObject _collider;
    void OnCollisionEnter(Collision other)
    {
        // ソナーを発信
        CameraManager.Get().sonarFx.Pulse(this.transform.position);

        Destroy(this.gameObject);
    }
}
