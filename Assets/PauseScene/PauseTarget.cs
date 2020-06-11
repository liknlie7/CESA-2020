// ポーズさせたいオブジェクトにアタッチするスクリプト
// 2020/06/11
// 佐竹晴登

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("PauseManager").GetComponent<PauseManager>().AddPauseTarget(this.gameObject);
    }
}
