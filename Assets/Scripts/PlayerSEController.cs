using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSEController : MonoBehaviour
{
	public AudioClip SwimSE;//泳ぐときのSE
	public AudioClip ShotSE;//波紋を打つ時のSE
	public AudioClip MovePointSE;//移動先決定時のSE

	public AudioSource audioSource;//オーディオソースを持つはこ

	[SerializeField]
	Player _player;

	[SerializeField]
	float se_interval = 0.0f;
	private float count;

	// Start is called before the first frame update
	void Start()
	{
		//AudioSource[] audioSources = GetComponents<AudioSource>();
		//SwimSE = audioSources[0];
		//ShotSE = audioSources[1];
	}

	// Update is called once per frame
	void Update()
	{
  //      //指定のキーが押されたら音声ファイルの再生をする
  //      if (_player.Swimming)
  //      {
  //          // TODO:: 要調整(一定間隔で音が鳴るように修正)
  //          if (!audioSource.isPlaying)
  //              audioSource.PlayOneShot(SwimSE);
  //          count = 0;
  //      }
  //      else
  //          audioSource.Stop();
		//if (Input.GetMouseButtonDown(1))
		//{
		//	audioSource.PlayOneShot(ShotSE);
		//}
	}

}
