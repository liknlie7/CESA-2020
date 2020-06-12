using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TadpoleController : MonoBehaviour
{
    private const string RADPOLE_KEYWORD = "_tadpoleNum";
    private string _keyName;

    [SerializeField]
    private GameObject _fxPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _keyName = SceneManager.GetActiveScene().name + RADPOLE_KEYWORD;
        // おたまじゃくしの数を初期化
        PlayerPrefs.SetInt(_keyName, 0);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            int count = PlayerPrefs.GetInt(_keyName);
            count++;
            PlayerPrefs.SetInt(_keyName, count);
            Debug.Log(PlayerPrefs.GetInt(_keyName));

            TadpoleUIController.RefreshUI();

            // TODO::ここで消している あとでアニメーションを付けるかも
            Instantiate(_fxPrefab, this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
