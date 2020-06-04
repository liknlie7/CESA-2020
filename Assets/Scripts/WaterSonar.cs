using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSonar : MonoBehaviour
{
    // ソナーが消えるまでのカウント
    [SerializeField]
    private float MAX_COUNT = 1.7f;
    private float _count = 0.0f;

    void Start()
    {
        // ソナーを発信
        CameraManager.Get().sonarFx.Pulse(this.transform.position);
        Destroy(this.gameObject);
    }

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    _count += Time.deltaTime;
    //    if (_count >= MAX_COUNT)
    //    {
    //        CameraManager.Get().sonarFxSwitcher.SetFlag(false);
    //        Destroy(this.gameObject);
    //    }
    //}
}
