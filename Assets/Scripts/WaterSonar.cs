using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSonar : MonoBehaviour
{
    void Start()
    {
        // ソナーを発信
        CameraManager.Get().sonarFx.Pulse(this.transform.position);

        StartCoroutine("DestroyObject");
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
}
