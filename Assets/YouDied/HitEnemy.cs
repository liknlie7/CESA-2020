using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    [SerializeField]
    string EnemyTag;
    [SerializeField]
    GameObject GameOverObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        GameOverObject.SetActive(true);
    }

    void OnCollisionEnter(Collision collision)
    {
        //衝突したオブジェクトが敵だった時
        if (collision.gameObject.CompareTag(EnemyTag))
        {
            GameOverObject.SetActive(true);
        }
    }
}

