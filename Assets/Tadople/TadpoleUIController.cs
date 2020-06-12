using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TadpoleUIController : MonoBehaviour
{
    [SerializeField]
    private Image _higlight;
    
    private const float WIDTH = 0.3335f;
    private const float ADD_SPEED = 0.8f;
    private const float OFFSET = 0.05f;
    static private float  _targetFillAmount = 0.0f;
    static private string _keyName;
    static bool           _isAddition       = false;
    //[SerializeField]
    //int count = 0;

    void Start()
    {
        _targetFillAmount = 0.0f;
        _higlight.fillAmount = 0.0f;
    }

    void FixedUpdate()
    {
        // Fillamountを増加中
        if (_isAddition && (_higlight.fillAmount < _targetFillAmount - OFFSET))
            _higlight.fillAmount += ADD_SPEED * Time.deltaTime;
        else if (_isAddition)
        {
            _higlight.fillAmount = _targetFillAmount;
            _isAddition = false;
        }
    }

    // UIをリフレッシュ
    static public void RefreshUI()
    {
        int count = FixedManager.Get().scoreManager._score;
        _targetFillAmount = WIDTH * count;
        _isAddition = true;
    }
}
