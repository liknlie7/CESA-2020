using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSEController : MonoBehaviour
{
    public AudioClip SwimSE;//泳ぐときのSE
    public AudioClip ShotSE;//波紋を打つ時のSE
    AudioSource AS;//オーディオソースを持つはこ

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        //AudioSource[] audioSources = GetComponents<AudioSource>();
        //SwimSE = audioSources[0];
        //ShotSE = audioSources[1];
        AS = GetComponent<AudioSource>();
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //指定のキーが押されたら音声ファイルの再生をする
        if (_player.Swimming)
        {
            // TODO:: 要調整(一定間隔で音が鳴るように修正)
            AS.PlayOneShot(SwimSE);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            AS.PlayOneShot(ShotSE);
        }
    }

}
