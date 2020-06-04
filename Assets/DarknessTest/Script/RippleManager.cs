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

    // 波紋の疑似当たり判定
    [SerializeField]
    GameObject rSphere = null;
    // Start is called before the first frame update
    void Start()
    {
        // SonarFx取得
        GameObject camera = GameObject.Find("Main Camera");

        fx = camera.GetComponent<CameraManager>().sonarFx;

        
    }

    // Update is called once per frame
    void Update()
    {
        // 波紋の管理用リスト
        List<SonarFx.SonarBounds> sonarBounds = new List<SonarFx.SonarBounds>();

        IEnumerator<SonarFx.SonarBounds> enumerator;
        // 波紋情報取得

        enumerator = fx.GetSonarBounds();

        while (enumerator.MoveNext())
        {
            sonarBounds.Add(enumerator.Current);
        }

        int bSum = sonarBounds.Count;
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Ripple");
        Debug.Log(cubes.Length);
        int i = 0;
        // 波紋の数だけプレファブを生成
        foreach (var v in sonarBounds)
        {
            if (cubes.Length <= i)
            {
                // プレファブを生成
                GameObject r = Instantiate(rSphere, v.center, Quaternion.identity);
                // コライダー取得
                r.GetComponent<SphereCollider>().radius = v.radius;
            }
            else if(0 < cubes.Length)
            {
                cubes[i].transform.position = v.center;
                cubes[i].GetComponent<SphereCollider>().radius = v.radius;
                
            }
            
            i++;
        }
        foreach(var v in cubes)
        {
            if (v.GetComponent<SphereCollider>().radius > 8.0f)
            {
                Destroy(v);
            }
        }

        
    }
}
