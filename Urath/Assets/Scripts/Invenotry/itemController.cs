using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemController : MonoBehaviour
{
    Transform origionalParent;
    CanvasGroup canvasGroup;

    public void OnBeginMove()
    {
        origionalParent = transform.parent; // save who parent is 
    }

    public void ShowActions()
    {
        //will use later on 
    }
}
