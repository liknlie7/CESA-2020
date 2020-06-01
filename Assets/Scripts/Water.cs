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
        Instantiate(_collider, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
