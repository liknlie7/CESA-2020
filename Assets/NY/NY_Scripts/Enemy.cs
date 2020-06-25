using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum STATE
    {
        CRAWL,      // 巡回
        ALERT,      // 警戒
        TRACKING    // 追跡
    }

    // 仮にpublicにしてる
    public bool _isPlayerDiscovery = false;     // プレイヤーを発見したか

    [SerializeField]
    private string     _targetTagName = "Player";                  // ターゲットのタグ名
    [SerializeField]
    private float      _fovAngle = 60.0f;                          // 視野角
    [SerializeField]
    private float      _fovLength = 20.0f;                         // 視野の長さ
    [SerializeField]
    private GameObject _discovery = null;                                 // 発見時のびっくりアイコン
    [SerializeField]
    private GameObject _question = null;                                  // 警戒時のクエスチョンアイコン
    [SerializeField]
    private List<Transform> _searchRoutes = new List<Transform>(); // 巡回ルート
    
    private int          _routeNum = 1;                 // 現在巡回している番号
    private NavMeshAgent _agent;                        // 自身のナビメッシュエージェント
    private Transform    _player;                       // プレイヤー
    private STATE        _currentState = STATE.CRAWL;   // 現在の状態
    private STATE        _lastState    = STATE.CRAWL;   // 1フレーム前の状態

    [SerializeField]
    private float _discoveryAttentionTime = 10.0f;      // 追跡失敗時にプレイヤーを探す時間
    [SerializeField]
    private float _crawlAttentionTime     =  8.0f;      // 警戒時にプレイヤーを探す時間
    private float _attentionTime  = 0.0f;               // 注意時間 : この時間が一定時間を超えると巡回ステートに戻る
    private float _alertGauge     = 0.0f;               // 警戒ゲージ/プレイヤーを見続けていると増加する
    private const float ALERT_GAUGE_MAX = 4.0f;         // 警戒ゲージの最大量

    // TODO:: 一時的な変数 要再検討
    private bool _isTracking2alert = false; // 追跡ステートから警戒ステートに変更したか

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag(_targetTagName).transform;

        // デバッグ用  索敵範囲を可視化
        Vector3 pos = new Vector3(this.transform.position.x, -0.005f, this.transform.position.z);
        GameObject searchLengthObj = Instantiate((GameObject)Resources.Load("SearchLength"),pos,this.transform.rotation);
        searchLengthObj.transform.localScale = new Vector3(_fovLength*2, 0.02f, _fovLength*2);
        searchLengthObj.transform.parent = this.transform;

        // ナビメッシュエージェントを取得/初期目的地を設定
        _agent = this.GetComponent<NavMeshAgent>();
        _agent.SetDestination(_searchRoutes[_routeNum].position);

        // びっくりアイコンを非アクティブに
        _discovery.SetActive(false);
        _question.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(_currentState)
        {
            // 巡回ステート
            case STATE.CRAWL:
                CrawlState();
                break;

            // 警戒ステート
            case STATE.ALERT:
                AlertState();
                break;

            // 発見ステート
            case STATE.TRACKING:
                TrackingState();
                break;

            default:
                break;
        }
        _lastState = _currentState;
    }

    // 巡回ステート
    void CrawlState()
    {
        Vector3 playerPos = _player.position;
        float distance = Vector3.Distance(playerPos, this.transform.position);
        // プレイヤーとの距離が一定範囲内なら索敵開始
        if (distance < _fovLength)
            _isPlayerDiscovery = IsSearch();
        else
            _isPlayerDiscovery = false;

        // 巡回移動
        CrawlMove();
        // 発見マークがアクティブなら非アクティブにする
        if (_question.activeInHierarchy)
            _question.SetActive(false);

        // プレイヤーを見つけたら警戒ステートに変更
        if (_isPlayerDiscovery)
        {
            _currentState = STATE.ALERT;
            _attentionTime = _crawlAttentionTime;
        }
    }

    // 警戒ステート
    void AlertState()
    {
        _discovery.SetActive(false);
        _question.SetActive(true);
        // プレイヤーを見つけていたら
        if (IsSearch())
        {
            // 警戒ゲージを増加
            _alertGauge += Time.deltaTime;
            _agent.SetDestination(_player.position);
            // 注意時間をリセット
            if (_isTracking2alert)
                _currentState = STATE.TRACKING;
            else
                _attentionTime = _crawlAttentionTime;
        }
        else
            _alertGauge = 0.0f;

        // もし、プレイヤーが居た付近に着いたら、周りを見渡す
        if (_agent.remainingDistance <= 0.1f)
        {
            Vector3 rotate = new Vector3(0.0f, Mathf.Cos(Time.time) * 0.5f, 0.0f);
            this.transform.Rotate(rotate);
            _attentionTime -= Time.deltaTime;
        }

        // 警戒時間が経過すると巡回ステートへ
        if (_attentionTime <= 0.0f)
        {
            // 一番近い巡回ルートを検索
            SearchCrawRoute();
            _alertGauge = 0.0f;
            _currentState = STATE.CRAWL;
        }

        // 一艇時間プレイヤーを見続けると追跡ステートへ
        if(_alertGauge >= ALERT_GAUGE_MAX)
        {
            _alertGauge = 0.0f;
            _currentState = STATE.TRACKING;
        }


    }

    // 追跡ステート
    void TrackingState()
    {
        PlayerTrackingMove();
        _question.SetActive(false);
        _discovery.SetActive(true);
        _isTracking2alert = true;

        if(!IsPlayerSee())
        {
            _currentState = STATE.ALERT;
            _attentionTime = _discoveryAttentionTime;
        }
    }

    // 視野内にプレイヤーがいるか索敵
    bool IsSearch()
    {
        Vector3 playerPos = _player.position;
        Vector3 dir = this.transform.forward;

        // プレイヤーが視野範囲内に入ったら
        if (Vector3.Angle((playerPos - this.transform.position).normalized, dir) <= _fovAngle/2)
            return IsPlayerSee();

        return false;
    }

    // プレイヤーを障害物なしに視認できたか
    bool IsPlayerSee()
    {
        // プレイヤーを障害物なしに視認できたか
        Vector3 playerPos = _player.position;
        Vector3 searchDire = playerPos - this.transform.position;
        Ray ray = new Ray(this.transform.position, searchDire.normalized);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _fovLength))
        {
            if (hit.collider.gameObject.tag == _targetTagName)
                return true;
        }
        return false;
    }

    // 巡回時の移動処理
    void CrawlMove()
    {
        // 目的地周辺に就いたら目的地を次の場所に変更
        if (_agent.remainingDistance < 0.3f)
        {
            _routeNum++;
            _routeNum = _routeNum % _searchRoutes.Count;
            _agent.SetDestination(_searchRoutes[_routeNum].position);
        }
    }

    // 追跡中の移動処理
    void PlayerTrackingMove()
    {
        _agent.SetDestination(_player.position);
    }

    // 巡回ルートを検索
    void SearchCrawRoute()
    {
        float distance = Mathf.Infinity;
        int routeNum = 0;
        foreach(Transform route in _searchRoutes)
        {
            float dist = Vector3.Distance(route.position,this.transform.position);
            if (distance >= dist)
            {
                distance = dist;
                _routeNum = routeNum;
            }
            routeNum++;
        }
    }
}
