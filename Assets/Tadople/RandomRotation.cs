using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var euler = transform.localEulerAngles;
        euler.y = Random.Range(0, 360);
        transform.localEulerAngles = euler;
    }
}
