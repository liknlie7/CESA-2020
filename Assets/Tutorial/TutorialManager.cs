// チュートリアルのスクリプト
// 2020/06/13
// 佐竹晴登

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    // チュートリアル用UIのアニメーター
    Animator animator;
    // チュートリアルのタスクの定数
    private enum TASK
    {
        BASIC,  // 動く前に
        ENEMY,  // 敵について
        OTAMA,  // おたまじゃくしについて
        GOAL,   // ゴールについて
        CLEAR,  // ステージクリア
        GAMEOVER, // ゲームオーバー

        SUM     // 合計
    }
    // タスク
    private Action[] task;
    // 現在のタスク番号
    int currentTask;

    // チュートリアルのテキスト
    [SerializeField]
    TextMeshProUGUI TMP;
    // チュートリアルの文章リスト
    private string[] texts = new string[10];
    // ゴールへの説明が出る距離
    const float GOAL_DISTANCE = 5.0f;
    // プレイヤーオブジェクト
    [SerializeField]
    private GameObject player;
    // プレイヤースクリプト
    private Player playerScript;
    // プレイヤーの初期位置
    private Vector3 initialP_pos;
    // スコアマネージャー
    [SerializeField]
    private ScoreManager scoreMana;
    // リップルマネージャー
    [SerializeField]
    private GameObject rippleDir;
    // ゲームオーバースクリプト
    private GameOverStaging gameOverScript;
    // 敵
    [SerializeField]
    private GameObject enemy;
    // 敵のスクリプト
    private EnemyController enemyScript;

    private static readonly int Hide = Animator.StringToHash("Hide");

    // 初期化
    private void Start()
    {
        // 文章読み込み(テスト)
        texts[(int)TASK.BASIC] = "ポインタの位置に左クリックで移動\n右クリックで波紋を周りを確認できるぞ！";
        texts[(int)TASK.ENEMY] = "敵に見つかってしまった！こうなると家に逃げるしかない！";
        texts[(int)TASK.OTAMA] = "おや、迷子のおたまじゃくしだ！\n一緒に連れ帰ってあげよう";
        texts[(int)TASK.GOAL] = "緑色のハスの葉の下に家があるぞ！\n敵に見つからないうちに帰ろう！";
        texts[(int)TASK.CLEAR] = "ステージクリアだ！おめでとう！\n本編では本当の闇の中帰ることになるぞ！";
        texts[(int)TASK.GAMEOVER] = "残念！食べられてしまった！\n夜は危険な生物も徘徊しているぞ！";

        // タスク関数の設定
        task = new Action[(int)TASK.SUM] { AboutBasic, AboutEnemy, AboutOTAMA , AboutGoal,AboutCrear,AboutGameOver};
        ChangeText();
        animator = GetComponent<Animator>();
        currentTask = (int)TASK.BASIC;

        playerScript = player.GetComponent<Player>();
        initialP_pos = player.transform.position;

        gameOverScript = rippleDir.GetComponent<GameOverStaging>();
        enemyScript = enemy.GetComponent<EnemyController>();
    }
    // 更新
    private void Update()
    {
        task[currentTask]();
    }

    private void AboutBasic()
    {
        if(Vector3.Distance(initialP_pos,player.transform.position) > GOAL_DISTANCE)
        {
            animator.SetTrigger(Hide);
            currentTask = (int)TASK.GOAL;
        }
        ScoreCheck();
        GameOverCheck();
    }
    private void AboutEnemy()
    {
        CrearCheck();
        GameOverCheck();
    }
    private void AboutOTAMA()
    {
        CrearCheck();
        GameOverCheck();
    }
    private void AboutGoal()
    {
        ScoreCheck();
        CrearCheck();
        EnemyCheck();
        GameOverCheck();
    }
    private void AboutGameOver()
    {

    }
    private void AboutCrear()
    {
        
    }
    // スコアを取得したかどうか
    private void ScoreCheck()
    {
        if (scoreMana._score > 0)
        {
            animator.SetTrigger(Hide);
            currentTask = (int)TASK.OTAMA;
        }
    }
    // 敵に発見されたか
    private void EnemyCheck()
    {
        if(enemyScript.GetStateName() == "EnemyAlertState")
        {
            animator.SetTrigger(Hide);
            currentTask = (int)TASK.ENEMY;
        }
    }
    // クリアしたかどうか
    private void CrearCheck()
    {
        if(player != null)
        {
            if (playerScript.GetGoalFlag())
            {
                animator.SetTrigger(Hide);
                currentTask = (int)TASK.CLEAR;
            }
        }   
    }
    private void GameOverCheck()
    {
        if(gameOverScript._isGameOver)
        {
            animator.SetTrigger(Hide);
            currentTask = (int)TASK.GAMEOVER;
        }
    }
    public void ChangeText()
    {
        TMP.text = texts[currentTask];
    }
}
