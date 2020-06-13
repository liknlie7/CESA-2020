using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectClearedUI : MonoBehaviour
{

    private string _sceneName;
    private const string RADPOLE_KEYWORD = "_cleared";

    [SerializeField]
    private GameObject _hiddenMap;


    // Start is called before the first frame update
    void Start()
    {
        _sceneName = this.name + RADPOLE_KEYWORD;
        int cleared = PlayerPrefs.GetInt(_sceneName);

        _hiddenMap.SetActive(cleared > 0);
    }
}
