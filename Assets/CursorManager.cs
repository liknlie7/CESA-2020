using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] GameObject DRAG_PARTICLE = null;  // PS_DragStarを割り当てること

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
    }

}
