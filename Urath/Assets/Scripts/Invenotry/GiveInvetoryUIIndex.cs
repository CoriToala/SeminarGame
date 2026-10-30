using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveInvetoryUIIndex : MonoBehaviour
{   
    [SerializeField] ItemActions itemActions;
    
    
    public void GiveIndex()
    {
        int position = transform.GetSiblingIndex();
        itemActions.GiveTo(position);
    }
}
