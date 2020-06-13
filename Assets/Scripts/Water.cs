using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // 波紋のコライダー
    [SerializeField]
    private GameObject _collider;

    public AudioSource sonarSound;

    void OnCollisionEnter(Collision other)
    {
        // ソナーを発信
        CameraManager.Get().sonarFx.Pulse(this.transform.position, null, this.gameObject);

        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<Renderer>().enabled = false;

        sonarSound.Play();

        StartCoroutine("DestroyObject");
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
