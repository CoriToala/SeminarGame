using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public string Name;
    public int Category;

    [Header("ActionVariables")]
    public int HpGain;
    public int DefenseBuff;
    public int OffenseBuff;

    [Header("ItemType")]
    public bool isFood = false;
    public bool isArmor = false;
    public bool isAbility = false;
    

    public virtual void PickUp()
    {
        if(ItemPickUpUIController.Instance != null)
        {
            //print("Sent name to pickup Controller");
            ItemPickUpUIController.Instance.ShowItemPickUp(Name);
        }
    }

    public int ShowActions()
    {

        if(Category == 1)
        {
            return HpGain;
        }
        if(Category == 2)
        {
            return DefenseBuff;
        }
        if(Category == 3)
        {
            return OffenseBuff;
        }
        else{
            return 0;
        }
    }


}
