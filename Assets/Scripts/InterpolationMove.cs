using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolationMove : MonoBehaviour
{

    public enum Mode
    {
        Normal,
        MoveTowards,
        Lerp,
        Slerp
    };

    //　キャラクターを動かすモード
    [SerializeField]
    private Mode mode = Mode.Normal;
    //キャラクターコントローラー
    private CharacterController cCon;
    //　キャラクターの速度
    private Vector3 velocity;
    //　キャラクターの角度
    private Quaternion rotation;
    //　変更する角度
    [SerializeField]
    private float rotateAngle = 5.0f;
    //　回転スピード
    [SerializeField]
    public float rotateSpeed;
    //　前の速度
    private Vector3 oldVelocity;

    //　歩く速さ
    [SerializeField]
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cCon = GetComponent<CharacterController>();
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (cCon.isGrounded)
        { //　キャラクターコントローラのコライダが地面と接触してるかどうか
            velocity = Vector3.zero;

            // 
            rotation = Quaternion.Euler(0f, transform.eulerAngles.y + Input.GetAxisRaw("Horizontal") * rotateAngle, 0f);

            //velocity = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));

            if (mode == Mode.MoveTowards)
            {
                velocity = Vector3.MoveTowards(oldVelocity, velocity, moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
            }
            else if (mode == Mode.Lerp)
            {
                velocity = Vector3.Lerp(oldVelocity, velocity, moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
            }
            else if (mode == Mode.Slerp)
            {          
                velocity = Vector3.Slerp(oldVelocity, velocity, moveSpeed * Time.deltaTime);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
            }
            oldVelocity = velocity;

        }
        velocity.y += Physics.gravity.y * Time.deltaTime;

        cCon.Move(velocity * Time.deltaTime);
       // if(Input.GetKeyDown(KeyCode))
            this.transform.position += this.gameObject.transform.forward * Input.GetAxisRaw("Vertical") / 10;
    }
}
