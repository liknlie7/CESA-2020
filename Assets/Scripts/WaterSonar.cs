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
        Camera.main.GetComponent<SonarFx>().SetOrigin(this.transform.position);
        Camera.main.GetComponent<SonarFxSwitcher>().SetFlag(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _count += Time.deltaTime;
        if (_count >= MAX_COUNT)
        {
            Camera.main.GetComponent<SonarFxSwitcher>().SetFlag(false);
            Destroy(this.gameObject);
        }
    }
}
