using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteAllButton : MonoBehaviour
{
    public void OnClick()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //this.transform.parent.parent.GetComponent<Animator>().SetTrigger("Refresh");
    }
}
