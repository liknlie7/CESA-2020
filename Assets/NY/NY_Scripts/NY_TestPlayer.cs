using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NY_TestPlayer : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vel = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            vel.z = 1;
        else
        if (Input.GetKey(KeyCode.S))
            vel.z = -1;

        if (Input.GetKey(KeyCode.A))
            vel.x = -1;
        else
        if (Input.GetKey(KeyCode.D))
            vel.x = 1;

        vel *= _moveSpeed * Time.deltaTime;
        this.transform.Translate(vel);
    }
}
