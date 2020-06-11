// これをアタッチするだけで波紋が出てくれる素敵なスクリプト
// 2020/06/10
// 佐竹晴登

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarGenerator : MonoBehaviour
{
    // ソナー情報
    [SerializeField]
    SonarPulse pulse = null;
    // ソナーが出てくる間隔
    [SerializeField]
    float sonar_Inteval = 0.0f;
    // カウント用
    private float count;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sonar_Inteval < count)
        {
            CameraManager.Get().sonarFx.Pulse(this.transform.position,pulse, this.gameObject);
            count = 0.0f;
        }
        count += Time.deltaTime;
    }
}
