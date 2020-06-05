// 波紋当たり判定確認用キューブ
// 2020/06/04
// 佐竹晴登

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ripple")
        {
            this.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ripple")
        {
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
