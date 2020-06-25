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
    Animator pageAnimator = null;

    [SerializeField]
    StageSelectManager SSMana = null;
    //左
    public void LeftOnClick()
    {
        SSMana.clickSound.Play();
        pageAnimator.SetInteger("Page", 0);

        rightArrow.SetActive(true);
        leftArrow.SetActive(false);
    }
    //右
    public void RightOnClick()
    {
        SSMana.clickSound.Play();
        pageAnimator.SetInteger("Page", 1);

        rightArrow.SetActive(false);
        leftArrow.SetActive(true);   
    }

}
