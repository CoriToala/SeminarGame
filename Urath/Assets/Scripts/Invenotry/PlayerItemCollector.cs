using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
   private inventoryController inventoryHandler;

   void Start()
   {
        inventoryHandler = Object.FindAnyObjectByType<inventoryController>();
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
        if(collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if(item != null)
            {
                bool itemAdded = inventoryHandler.AddItem(collision.gameObject);
                
                if(itemAdded)
                {
                    item.PickUp();
                    Destroy(collision.gameObject);
                }
            }
        }
   }
}
