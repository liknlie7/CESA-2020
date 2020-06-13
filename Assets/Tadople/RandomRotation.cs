using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [Range(0, 360)]
    public float range = 360;

    // Start is called before the first frame update
    void Start()
    {
        var euler = transform.localEulerAngles;
        euler.y += Random.Range(0, range) - range / 2;
        transform.localEulerAngles = euler;
    }
}