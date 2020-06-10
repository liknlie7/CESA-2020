using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEffect : MonoBehaviour
{
    //　元のパーティクルの透明度
    private float alphaValue = 1f;
    //　パーティクルシステム
    private ParticleSystem ps;
    //　Scaleを大きくする間隔時間
    [SerializeField]
    private float scaleUpTime = 0.03f;
    //　ScaleUpする割合
    [SerializeField]
    private float scaleUpParam = 0.1f;
    //　パーティクル削除用の経過時間
    private float elapsedDeleteTime = 0f;
    //　パーティクルを削除するまでの時間
    [SerializeField]
    private float deleteTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
      
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void OnParticleTrigger()
    {

        if (ps != null)
        {

            //　Particle型のインスタンス生成
            List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
            List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

            //　Inside、Enterのパーティクルを取得
            int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
            int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
          

            //　パーティクルデータの設定
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        }
    }
}