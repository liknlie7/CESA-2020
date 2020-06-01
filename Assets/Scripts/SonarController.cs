using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] const float activetime = 10.0f;
    [SerializeField] GameObject Camera;
    float count = 0;
    bool flg;

    void Start()
    {
        flg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flg)
        {
            if (count >= activetime)
            {
                flg = false;
            }
                
            
            count += Time.deltaTime;
        }
        else
        {
            // スペースキーで自分からソナーが出る。
            if(Input.GetKeyDown(KeyCode.Space))
            {
                flg = true;
                count = 0.0f;
                Camera.GetComponent<SonarFx>().SetOrigin(this.transform.position);
            }
        }
        Camera.GetComponent<SonarFxSwitcher>().SetFlag(flg);
    }
}
