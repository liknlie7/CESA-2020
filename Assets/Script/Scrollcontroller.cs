using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrollcontroller : MonoBehaviour
{
    

    private  int PaugCount=1;

    private float A = 1200.0f;
    private float B = 800.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //左
    public void LeftOnClick()
    {
        if(PaugCount>1)
        {
            PaugCount--;
            // 移動させたいオブジェクトを取得
            GameObject obj = GameObject.Find("Panel");
            // オブジェクトを移動

            obj.transform.Translate(A - B * PaugCount, 0.0f, 0.0f);
        }

        
    }
    //右
    public void RightOnClick()
    {
        if(PaugCount<2)
        {
            PaugCount++;
            // 移動させたいオブジェクトを取得
            GameObject obj = GameObject.Find("Panel");
            // オブジェクトを移動
            obj.transform.Translate(A - B * PaugCount, 0.0f, 0.0f);
        }
        
    }

}
