using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrollcontroller : MonoBehaviour
{
    [SerializeField]
    GameObject rightArrow = null;

    [SerializeField]
    GameObject leftArrow = null;

    [SerializeField]
    GameObject rightPage = null;

    [SerializeField]
    GameObject leftPage = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //左
    public void LeftOnClick()
    {
        leftPage.SetActive(true);
        rightPage.SetActive(false);

        rightArrow.SetActive(true);
        leftArrow.SetActive(false);
    }
    //右
    public void RightOnClick()
    {
        leftPage.SetActive(false);
        rightPage.SetActive(true);

        rightArrow.SetActive(false);
        leftArrow.SetActive(true);   
    }

}
