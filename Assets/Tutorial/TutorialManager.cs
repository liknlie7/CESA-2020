// チュートリアルのスクリプト
// 2020/06/13
// 佐竹晴登

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Object = System.Object;

public class TutorialManager : MonoBehaviour
{
    private ThrowingScript throwingScript;

    // チュートリアル用UIのアニメーター
    Animator animator;
    // チュートリアルのタスクの定数
    private enum Mission
    {
        BASIC,  // 動く前に
        MOVE,
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
    TextMeshProUGUI TMP = default;
    // チュートリアルの文章リスト
    private string[] texts = new string[10];
    // ゴールへの説明が出る距離
    const float GOAL_DISTANCE = 5.0f;
    // プレイヤーオブジェクト
    [SerializeField]
    private GameObject player = null;
    // プレイヤースクリプト
    private Player playerScript;
    // プレイヤーの初期位置
    private Vector3 initialP_pos;
    // スコアマネージャー
    [SerializeField]
    private ScoreManager scoreMana = null;
    // リップルマネージャー
    [SerializeField]
    private GameObject rippleDir = null;
    // ゲームオーバースクリプト
    private GameOverStaging gameOverScript;
    // 敵
    [SerializeField]
    private GameObject enemy = null;
    // 敵のスクリプト
    private EnemyController enemyScript;

    private static readonly int Hide = Animator.StringToHash("Hide");

    // 初期化
    private void Start()
    {
        // 文章読み込み(テスト)
        texts[(int)Mission.BASIC] = "右クリックで波紋をだして、\n周りを確認してみよう！";
        texts[(int)Mission.MOVE] = "左クリックで移動\n今回は特別に周りを明るくしておいたよ";
        texts[(int)Mission.ENEMY] = "敵に見つかってしまった！こうなるとお家に逃げるしかない！";
        texts[(int)Mission.OTAMA] = "あ！迷子のおたまじゃくしだ！\n一緒に連れ帰ってあげよう";
        texts[(int)Mission.GOAL] = "緑色のハスの葉の下に家があるよ！\n敵に見つからないお家に帰ろう！";
        texts[(int)Mission.CLEAR] = "ステージクリア！おめでとう！\n次からは闇の中、気を付けて！";
        texts[(int)Mission.GAMEOVER] = "残念！食べられてしまった！\n夜は危険な生物も徘徊しているよ！";

        throwingScript = FindObjectOfType<ThrowingScript>();

        // タスク関数の設定
        task = new Action[(int)Mission.SUM] { AboutBasic, AboutMove, AboutEnemy, AboutOTAMA , AboutGoal,AboutCrear,AboutGameOver};
        ChangeText();
        animator = GetComponent<Animator>();
        currentTask = (int)Mission.BASIC;

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

    IEnumerator BrightUp()
    {
        yield return new WaitForSeconds(2.0f);
        var m = FixedManager.Get().intensityManager.intensityTo = .5f;
    }

    private void AboutBasic()
    {
        if (throwingScript != null && throwingScript.thrownAchievement)
        {
            animator.SetTrigger(Hide);
            currentTask = (int) Mission.MOVE;
            StartCoroutine(BrightUp());
        }

        if (Vector3.Distance(initialP_pos, player.transform.position) > GOAL_DISTANCE)
        {
            animator.SetTrigger(Hide);
            currentTask = (int) Mission.GOAL;
            StartCoroutine(BrightUp());
        }

        ScoreCheck();
        GameOverCheck();
    }

    private void AboutMove()
    {
        if (Vector3.Distance(initialP_pos, player.transform.position) > GOAL_DISTANCE)
        {
            animator.SetTrigger(Hide);
            currentTask = (int) Mission.GOAL;
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
            currentTask = (int)Mission.OTAMA;
        }
    }

    // 敵に発見されたか
    private void EnemyCheck()
    {
        if(enemyScript.GetStateName() == "EnemyAlertState")
        {
            animator.SetTrigger(Hide);
            currentTask = (int)Mission.ENEMY;
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
                currentTask = (int)Mission.CLEAR;
            }
        }   
    }

    private void GameOverCheck()
    {
        if(gameOverScript._isGameOver)
        {
            animator.SetTrigger(Hide);
            currentTask = (int)Mission.GAMEOVER;
        }
    }

    public void ChangeText()
    {
        TMP.text = texts[currentTask];
    }
}
