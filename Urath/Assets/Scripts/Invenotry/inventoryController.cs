using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryController : MonoBehaviour
{
   // [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] int slotCount;
    //public GameObject[] itemPrefabs;
    [SerializeField] private GameObject[] characterInventorys;

    private ItemDictionary itemDictionary;

    void Start()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();

       // for(int i = 0; i < slotCount; i++)
      //  {
           // inventorySlot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<inventorySlot>();
           // if(i < itemPrefabs.Length)
          ////  {
          //      GameObject item = Instantiate(itemPrefabs[i], slot.transform);
         //       item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //        slot.currentItem = item;
        //    }
        // }
    }

    public bool TransferItem(GameObject item, int inventory)
    {
        foreach(Transform slotTransform in characterInventorys[inventory].transform) //auto adds to Migi inventory
        {
            inventorySlot slot = slotTransform.GetComponent<inventorySlot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(item, slotTransform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }
        Debug.Log("Inventory is full");
        return false; 
    }

    public bool AddItem(GameObject itemPrefab)
    {
        //Look for an emptyslot
        foreach(Transform slotTransform in characterInventorys[0].transform) //auto adds to Migi inventory
        {
            inventorySlot slot = slotTransform.GetComponent<inventorySlot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slotTransform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }
        Debug.Log("Inventory is full");
        return false;
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        {
            //need to go through each inventory
            for (int i = 0; i < 5; i++)
            {
                if (characterInventorys[i] != null){
                    foreach(Transform slotTransform in characterInventorys[i].transform)
                    {
                        inventorySlot slot = slotTransform.GetComponent<inventorySlot>();
                        if(slot.currentItem != null)
                        {
                            Item item = slot.currentItem.GetComponent<Item>();
                            invData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex(), whichInv = i});
                        }
                    }
                }
            }
            return invData;
        }
    }

    public void SetInventoryItems(List<InventorySaveData> invData)
    {
        //Clear out inventory
        foreach(GameObject obj in characterInventorys)
        {
            foreach(Transform child in obj.transform)
            {
                Destroy(child.gameObject);
            }
        }

        //Create new slots
        foreach(GameObject obj in characterInventorys)
        {
            MigiInvManager inv = obj.GetComponent<MigiInvManager>();
            inv.CreateSlots(slotPrefab, slotCount);
        }

        foreach(InventorySaveData data in invData)
        {

            CheckInv(data);
        }
    }

    void CheckInv(InventorySaveData data)
    {
        GameObject inv = characterInventorys[data.whichInv];
        inventorySlot slot = inv.transform.GetChild(data.slotIndex).GetComponent<inventorySlot>();
            GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
            if(itemPrefab != null)
            {
                GameObject item = Instantiate(itemPrefab, slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
    }
}
