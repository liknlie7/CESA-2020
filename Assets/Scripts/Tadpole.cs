using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オタマジャクシ
public class Tadpole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // プレイヤーと当たったら消す（取得される）
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}
