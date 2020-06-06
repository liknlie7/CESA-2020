// 波紋生成管理
// 2020/06/04
// 佐竹晴登

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RippleManager : MonoBehaviour
{
    // SonarFXスクリプト(波紋情報)
    SonarFx fx = null;
    // 波紋の管理用リスト
    List<SonarFx.SonarBounds> sonarBounds = new List<SonarFx.SonarBounds>();
    // エミュメレーター
    IEnumerator<SonarFx.SonarBounds> enumerator;

    // 波紋当たり判定のオブジェクト群
    GameObject[] rSpheres = new GameObject[16];
    // 波紋の疑似当たり判定
    [SerializeField]
    GameObject rSphere = null;

    // Start is called before the first frame update
    void Start()
    {
        // SonarFx取得
        GameObject camera = GameObject.Find("Main Camera");

        fx = camera.GetComponent<CameraManager>().sonarFx;

        

        // 波紋当たり判定群の初期化
        for (int i = 0; i < rSpheres.Length; i++)
        {
            rSpheres[i] = null;
        }

        // 波紋情報取得
        enumerator = fx.GetSonarBounds();
        // リストに空きを作っておく
        while (enumerator.MoveNext())
        {
            sonarBounds.Add(enumerator.Current);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 波紋情報更新
        enumerator = fx.GetSonarBounds();
        int i = 0;
        while (enumerator.MoveNext())
        {
            sonarBounds[i] = enumerator.Current;
            i++;
        }

        i = 0;
        // 波紋の数だけプレファブを生成
        foreach (var v in sonarBounds)
        {
            // sonarBoundが有効の時のみ
            if(sonarBounds[i].isValid)
            {
                if (rSpheres[i] == null)
                {
                    // プレファブを生成
                    rSpheres[i] = Instantiate(rSphere, v.center, Quaternion.identity);
                    // コライダー取得
                    rSpheres[i].GetComponent<SphereCollider>().radius = v.radius;
                }
                else
                {
                    // 登録されているオブジェクトを更新
                    rSpheres[i].transform.position = v.center;
                    rSpheres[i].GetComponent<SphereCollider>().radius = v.radius;
                }
                // 波紋自身の設定されたrangeを超えたら削除する
                if (rSpheres[i].GetComponent<SphereCollider>().radius > (sonarBounds[i].maxRadius - 1.0f))
                {
                    Destroy(rSpheres[i]);
                    rSpheres[i] = null;
                }
            }
            i++;
        }

        
    }
}
