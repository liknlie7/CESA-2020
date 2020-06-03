// タイトルロゴアニメーションの管理
// 2020/06/02
// 佐竹晴登

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TItleLogoController : MonoBehaviour
{
    // タイトル管理オブジェクト
    [SerializeField] GameObject titleDirector = null;
    // ロゴのアニメーター
    Animator logoAnimator = null;

    // 初期化
    private void Start()
    {
        logoAnimator = this.GetComponent<Animator>();
    }
    // アニメーションが終わった時の処理
    public void LogoAnimeEnd()
    {
        
        logoAnimator.Play("TitleWait");
        titleDirector.GetComponent<TitleController>().TitleActive();
    }
}
