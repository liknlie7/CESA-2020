using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> _enemyList = new List<GameObject>();
    
    // オブジェクト(エネミーの登録)
    public void RegisterEnemy(GameObject obj)
    {
        _enemyList.Add(obj);
    }
    
    // 敵を全て停止させる
    public void StopAllEnemy()
    {
        // 敵を全て停止
        foreach(GameObject obj in _enemyList)
        {
            obj.GetComponent<EnemyController>().StopNavMeshAgent();
            obj.GetComponent<EnemyController>().enabled = false;
        }
    }
}
