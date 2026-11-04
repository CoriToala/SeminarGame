using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemActions : MonoBehaviour
{
    [SerializeField] GameObject myActions;
    [SerializeField] GameObject inventories;
    private bool actionsUp;
    inventoryController inventoryHandler;
    [SerializeField] TabController tabManager;
    Item itemPrefab;
    BattleManager battleHandler;
    [SerializeField] Image equippedItem;
    [SerializeField] GameObject newAb;

    void Start()
    {
        myActions.SetActive(false);
        inventoryHandler = FindAnyObjectByType<inventoryController>();
        battleHandler = FindAnyObjectByType<BattleManager>();
        tabManager = Object.FindAnyObjectByType<TabController>();
        itemPrefab = GetComponent<Item>();
        inventories.SetActive(false);
        if(equippedItem != null){
            equippedItem.enabled = false;
        }
    }

    public void ShowActions()
    {
        if(actionsUp)
        {
            myActions.SetActive(false);
            actionsUp = false;
        }
        else{
            myActions.SetActive(true);
            actionsUp = true;
        }
    }

    public void OnGive()
    {
        inventories.SetActive(true);
        //seperate menu pops up of character inventoriess

        //Clicking on an inventory relates to a characters inventory
        //if there is an avaible slot 
        //use chest script as a reference
    }

    public void GiveTo(int position)
    {
        //get an int based on who clicked (could use sibling index +1)
        Debug.Log(position.ToString());
        bool itemAdded = inventoryHandler.TransferItem(this.gameObject, position);
                
            if(itemAdded)
            {
                //Item obj = itemPrefab.GetComponent<Item>();
                //obj.PickUp();
                //above is to show item pickup announcement instead add display text that item was transfered to someone else

                Destroy(this.gameObject);
            }
        inventories.SetActive(false);
    }

    public void OnDrop()
    {
        Destroy(this.gameObject);
    }

    public void OnUse()
    {
        int who = tabManager.GetPage();
        //if item is an armor 
        if (itemPrefab.isArmor)
        {
            //Detect which tab is active using tab controller 
            int defense = itemPrefab.DefenseBuff;
            equippedItem.enabled = true;
            //set an int to who tab controller gets and send it to equip armor
            battleHandler.EquipArmor(defense, who);
        }
        //if item is a food
        if (itemPrefab.isFood)
        {
            //Get int from tab for which is active 
            //send into to battle handler
            int health = itemPrefab.HpGain;
            battleHandler.Heal(health, who);
            Destroy(this.gameObject);
            
        }
        //if item is an ability 
        if (itemPrefab.isAbility)
        {
            //battle handler add ability
            //int from tab manager 
            battleHandler.AddAbility(newAb, who, this.gameObject);
        }
        else 
        {
            Debug.Log("Item doesnt have a type selected");
            return;
        }
    }
}
