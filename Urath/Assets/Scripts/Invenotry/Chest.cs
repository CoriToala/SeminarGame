using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, Interactable
{
    public bool isOpened { get; private set;}
    public string chestID { get; private set; }
    public GameObject itemPrefab; //item chest gives
    public Sprite openedSprite;
    private inventoryController inventoryHandler;

    void Start()
    {
        chestID ??= GlobalHelper.GenerateUniqueID(gameObject); 
        inventoryHandler = FindAnyObjectByType<inventoryController>();
    }

    public bool CanInteract()
    {
       // Debug.Log("Chest is opened" + isOpened);
        return isOpened;
    }

    public void Interact()
    {
        //Debug.Log("Interaction Call Recieved");
        if (CanInteract())
        {
            return;
        }
        else
        {
            print("Opening Chest");
            OpenChest();
        }
    }

    private void OpenChest()
    {
        if (itemPrefab)
        {
            bool itemAdded = inventoryHandler.AddItem(itemPrefab);
                
                if(itemAdded)
                {
                    Item obj = itemPrefab.GetComponent<Item>();
                    obj.PickUp();
                    //Debug.Log("Added to inventory");
                    SetOpened(true);
                }
        }

    }

    public void SetOpened(bool opened)
    {
        if(isOpened = opened)
        {
            //Debug.Log("Set as opened");
            GetComponent<SpriteRenderer>().sprite = openedSprite;
        }
    }
}
