using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyState))]
public class EnemyStateEditor : Editor
{
    public static readonly string[] NextStateName = { "AlertState", "CrawState" };
    public override void OnInspectorGUI()
    {
        EnemyState enemyState = target as EnemyState;
        //enemyState._stateNum = EditorGUILayout.Popup("NextStateName", enemyState._stateNum, NextStateName);
    }
}
