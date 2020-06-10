using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public int activeId = 1;

    public void OnStageButtonClicked()
    {
        SceneManager.LoadScene($"Stage{activeId}");
    }
}
