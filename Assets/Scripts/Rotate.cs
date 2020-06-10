using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public enum RotateMode
    {
        Normal,
        RotateTowards,
        Lerp,
        Slerp
    };

    //　速度
    private Vector3 velocity;
    //　キャラクターの角度
    private Quaternion rotation;
    //　変更する角度
    [SerializeField]
    private float rotateAngle = 45f;
    //　回転スピード
    [SerializeField]
    private float rotateSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        rotation = Quaternion.Euler(0f, transform.eulerAngles.y + Input.GetAxis("Horizontal") * rotateAngle, 0f);

    }
}
