using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // 波紋のコライダー
    [SerializeField]
    private GameObject _collider;

    public GameObject source;

    void OnCollisionEnter(Collision other)
    {
        // ソナーを発信
        CameraManager.Get().sonarFx.Pulse(this.transform.position, null, source);

        Destroy(this.gameObject);
    }
}
