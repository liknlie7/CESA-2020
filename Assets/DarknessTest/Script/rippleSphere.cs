﻿// 波紋の当たり判定
// 2020/6/04
// 佐竹晴登

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rippleSphere : MonoBehaviour
{
    RippleManager manager = null;

    public SonarFx.SonarBounds sonarbound;
    // 初期化
    void Start()
    {
        // SonarFx取得
        manager = FixedManager.Get().rippleManager;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (sonarbound.source?.CompareTag("Player") ?? false)
            {
                manager.AddEnemyList(other.gameObject);
            }
        }
    }
}
