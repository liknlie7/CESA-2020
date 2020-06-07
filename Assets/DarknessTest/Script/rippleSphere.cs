// 波紋の当たり判定
// 2020/6/04
// 佐竹晴登

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rippleSphere : MonoBehaviour
{
    RippleManager manager = null;
    
    // 初期化
    void Start()
    {
        // SonarFx取得
        GameObject dir = GameObject.Find("RippleDirector");

        manager = dir.GetComponent<RippleManager>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log(other.gameObject.name);
            manager.AddEnemyList(other.gameObject);
        }
    }
}
