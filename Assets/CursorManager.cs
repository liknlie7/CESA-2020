using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] GameObject DRAG_PARTICLE;  // PS_DragStarを割り当てること

    private GameObject m_DragParticle;
    private ParticleSystem m_DragParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        // パーティクルを生成
        m_DragParticle = Instantiate(DRAG_PARTICLE);

        m_DragParticleSystem = m_DragParticle.GetComponent<ParticleSystem>();
        m_DragParticleSystem.Play();
        // cursorを非表示
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var plane = new Plane(Vector3.up, new Vector3(0.0f,0.8f,0.0f));
        var ray = CameraManager.Get().sonarCamera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            var target = ray.GetPoint(enter);
            m_DragParticle.transform.position = target;
        }

        // パーティクルをマウスカーソルに追従させる
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //mousePosition.z = 20f;  // ※Canvasよりは手前に位置させること
        //m_DragParticle.transform.position = mousePosition;

        // マウス操作
        //if (Input.GetMouseButtonDown(0))
        {
            // 左ボタンダウンを検知したら、マウスカーソル位置からキラキラエフェクトを再生する。
            //m_DragParticleSystem.Play();    // ループ再生(ParticleSystemのLoopingがtrueだから)
        }
        //if (Input.GetMouseButtonUp(0))
        {
            // 左ボタンアップを検知したら、Particleの放出を停止する
          //  m_DragParticleSystem.Stop();
        }
    }

}
