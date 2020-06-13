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
    Animator pageAnimator;

    //左
    public void LeftOnClick()
    {
        pageAnimator.SetInteger("Page", 0);

        rightArrow.SetActive(true);
        leftArrow.SetActive(false);
    }
    //右
    public void RightOnClick()
    {
        pageAnimator.SetInteger("Page", 1);

        rightArrow.SetActive(false);
        leftArrow.SetActive(true);   
    }

}
