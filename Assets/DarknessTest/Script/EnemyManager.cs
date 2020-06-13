using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> _enemyList = new List<GameObject>();
    // 敵の優先度管理
    //private int _priorityCount = 0;

    // オブジェクト(エネミーの登録)
    public void RegisterEnemy(GameObject obj)
    {
        _enemyList.Add(obj);
        //obj.GetComponent<NavMeshAgent>().avoidancePriority = _priorityCount;
        //_priorityCount++;
    }
    
    // 敵を全て停止させる
    public void StopAllEnemy()
    {
        // 敵を全て停止
        // オブジェクトにアタッチされているレンダラー以外を停止させる
        foreach (GameObject obj in _enemyList)
        {
            if (obj != null)
            {
                Behaviour[] behavs = obj.GetComponentsInChildren<Behaviour>();
                foreach (var b in behavs)
                {
                    b.enabled = false;
                }
            }
        }
    }

    // 敵を全て再開させる
    public void Resume()
    {
        // 敵を全て再開
        // オブジェクトにアタッチされているレンダラー以外を再開させる
        foreach (GameObject obj in _enemyList)
        {
            if (obj != null)
            {
                Behaviour[] behavs = obj.GetComponentsInChildren<Behaviour>();
                foreach (var b in behavs)
                {
                    b.enabled = true;
                }
            }
        }
    }
}
