﻿// 波紋生成管理
// 2020/06/04
// 佐竹晴登

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RippleManager : MonoBehaviour
{
    // スクリプト内で使うエネミー情報
    public class EnemyInfo
    {
        public GameObject obj;

        // 作成時の時間
        public float activateTime;

        // 削除フラグ
        public bool delete;
    };

    public struct WaveBound
    {
        public SonarFx.SonarBounds bound;
        public GameObject obj;
        public SphereCollider collider;
    }

    // 波紋当たり判定のオブジェクト群
    WaveBound[] waveBounds = new WaveBound[16];

    // 波紋の疑似当たり判定
    [SerializeField] GameObject rSphere = null;

    // 衝突したエネミーのリスト
    List<EnemyInfo> colEnemyList = new List<EnemyInfo>();

    // 輪郭を付けている時間
    [SerializeField] float activeOutlineTime = 3.0f;

    // アウトラインが設定されたレイヤーの番号 
    [SerializeField]
    int layerNumber = 10;

    // Start is called before the first frame update
    void Start()
    {
        // SonarFx取得
        var bounds = CameraManager.Get().sonarFx.GetSonarBounds();

        // 波紋当たり判定群の初期化
        int i = 0;
        foreach (var bound in bounds)
        {
            // 最初に全部作っておく
            var obj = Instantiate(rSphere, transform);
            obj.SetActive(false);
            obj.GetComponent<rippleSphere>().sonarbound = bound;
            waveBounds[i] = new WaveBound()
            {
                bound = bound,
                obj = obj,
                collider = obj.GetComponent<SphereCollider>(),
            };
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var waveBound in waveBounds)
        {
            // sonarBoundが有効の時はコライダー有効
            {
                // 登録されているオブジェクトを更新
                if (waveBound.bound.IsValid)
                {
                    waveBound.obj.transform.position = waveBound.bound.center;
                    waveBound.collider.radius = waveBound.bound.Radius;

                    if (waveBound.obj.active  == false)
                    {
                        waveBound.obj.SetActive(true);
                        waveBound.obj.GetComponent<rippleSphere>().InitializeTag();
                    }

                }
                else
                {
                    waveBound.obj.tag = "Enemy";
                    waveBound.obj.SetActive(false);
                }
            }
        }

        // アウトライン継続時間の更新
        var remove = colEnemyList
            .Where(e => Time.time - e.activateTime > activeOutlineTime)
            .Select(e =>
            {
                if (e.obj != null)
                {
                    foreach (var c in e.obj.GetComponentsInChildren<Renderer>())
                    {
                        if (c.gameObject.layer == layerNumber)
                            c.gameObject.layer = 0;
                    }
                }

                e.delete = true;
                return e;
            })
            .ToList();
        colEnemyList.RemoveAll(e => e.delete);
    }

    // アウトラインをつけるエネミーをリストに入れる処理
    public void AddEnemyList(GameObject go)
    {
        // 敵がリストに含まれなければ
        if (!colEnemyList.Select(e => e.obj).Contains(go))
        {
            // 光らせて敵をリストに追加
            EnemyInfo enemyInfo = new EnemyInfo {obj = go, activateTime = Time.time};
            foreach (var c in go.GetComponentsInChildren<Renderer>())
            {
                if (c.gameObject.layer == 0)
                    c.gameObject.layer = layerNumber;
            }

            colEnemyList.Add(enemyInfo);
        }
    }
}