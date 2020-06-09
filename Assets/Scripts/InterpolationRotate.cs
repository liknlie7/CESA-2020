using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolationRotate : MonoBehaviour
{
    public enum RotateMode
    {
        Normal,
        RotateTowards,
        Slerp
    };

    public RotateMode mode;
    //　速度
    private Vector3 velocity;
    //　キャラクターの角度
    private Quaternion rotation;
    //　変更する角度
    [SerializeField]
    private float rotateAngle = 45f;
    //　回転スピード
    [SerializeField]
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        rotation = Quaternion.Euler(0f, transform.eulerAngles.y + Input.GetAxis("Horizontal") * rotateAngle, 0f);

        if (mode == RotateMode.RotateTowards)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        }
        else if (mode == RotateMode.Slerp)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = rotation;
        }
    }
}
