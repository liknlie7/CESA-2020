using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public Transform StartPos;
    public Transform EndPos;

    //スピード
    public float Speed = 2.0f;


    //二点間の距離を入れる
    private float DistanceTwo;

    // Start is called before the first frame update
    void Start()
    {
        //二点間の距離を代入(スピード調整に使う)
        DistanceTwo = Vector3.Distance(StartPos.position, EndPos.position);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        // 現在の位置
        float present_Location = (Time.time * Speed) / DistanceTwo;

        // オブジェクトの移動(ここだけ変わった！)
        transform.position = Vector3.Slerp(StartPos.position, EndPos.position, present_Location);
    }
}



