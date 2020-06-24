using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSEController : MonoBehaviour
{
    public Animator SwimSE;//泳ぐときのSE
    public AudioClip ShotSE;//波紋を打つ時のSE
    public AudioClip MovePointSE;//移動先決定時のSE

    public AudioSource audioSource;//オーディオソースを持つはこ

    [SerializeField]
    Player _player;

    [SerializeField]
    private float count;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
