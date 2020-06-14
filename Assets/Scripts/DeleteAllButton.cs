using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteAllButton : MonoBehaviour
{
    public void OnClick()
    {
        PlayerPrefs.DeleteAll();
        this.transform.parent.parent.GetComponent<Animator>().SetTrigger("Refresh");
    }
}
