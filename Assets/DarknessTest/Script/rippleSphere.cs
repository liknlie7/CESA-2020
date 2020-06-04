// 波紋の当たり判定
// 2020/6/04
// 佐竹晴登

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rippleSphere : MonoBehaviour
{
    // SonarFXスクリプト(波紋情報)
    SonarFx fx = null;

    // 自分の波紋ナンバー(配列から)
    [System.NonSerialized]
    public int myNum = 0;

    // 初期化
    void Start()
    {
        // カメラから波紋情報を取得
        GameObject camera = GameObject.Find("Main Camera");

        fx = camera.GetComponent<SonarFx>();
    }

    // 更新
    void Update()
    {
       
        // 波紋大きさを設定
        // this.transform.localScale = 

    }
}
