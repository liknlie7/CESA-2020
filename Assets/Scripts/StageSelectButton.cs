using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageSelectButton : MonoBehaviour
{
    public int id;
    private StageSelectManager manager;

    private void Start()
    {
        manager = GetComponentInParent<StageSelectManager>();
    }

    public void OnSubmit()
    {
        manager.activeId = id;
        manager.OnStageButtonClicked();
    }

    public void OnClicked()
    {
        manager.activeId = id;
        if (EventSystem.current.currentSelectedGameObject != gameObject)
            EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
