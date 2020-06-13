using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TadpoleController : MonoBehaviour
{
    [SerializeField] private GameObject _fxPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FixedManager.Get().scoreManager.AddScore();

            TadpoleUIController.RefreshUI();

            // TODO::ここで消している あとでアニメーションを付けるかも
            Instantiate(_fxPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}