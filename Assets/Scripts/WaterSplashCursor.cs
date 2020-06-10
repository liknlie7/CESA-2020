using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WaterSplashCursor : MonoBehaviour
{
    private GameObject _target;

    private float _minLength;
    private float _maxLength;
    
    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(_target.transform.position);
    }
}
