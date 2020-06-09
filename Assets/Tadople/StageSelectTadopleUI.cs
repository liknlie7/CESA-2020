using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectTadopleUI : MonoBehaviour
{

    private string _sceneName;
    private const string RADPOLE_KEYWORD = "_tadpoleNum";
    private int _tadopleNum = 0;

    private const float WIDTH = 0.3335f;
    private const float OFFSET = 0.05f;

    [SerializeField]
    private Image _higlight;


    // Start is called before the first frame update
    void Start()
    {
        _sceneName = this.name + RADPOLE_KEYWORD;
        _tadopleNum = PlayerPrefs.GetInt(_sceneName);

        _higlight.fillAmount = WIDTH * _tadopleNum;
    }
}
