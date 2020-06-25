using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEffect : MonoBehaviour
{
    //　パーティクルシステム
    private ParticleSystem ps;

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