using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform origionalParent;
    CanvasGroup canvasGroup;
    [SerializeField] GameObject myActions;

    Item myItem;

    void Start()
    {
        //GetSibling that is my actions
        myActions = transform.parent.gameObject.transform.GetChild(0).gameObject;
        canvasGroup = GetComponent<CanvasGroup>();
        myItem = GetComponent<Item>();
        myActions.SetActive(false);
    }

public void OnBeginDrag(PointerEventData eventData)
{
    //Change to be show items
    myActions.SetActive(true);

    //origionalParent = transform.parent; //Save OG Parent
    //transform.SetParent(transform.root);
    //canvasGroup.blocksRaycasts = false;
    //canvasGroup.alpha = 0.6f;
   }

   public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // Follow mouse
  }

    public void OnEndDrag(PointerEventData eventData)
{
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;    
        inventorySlot dropSlot = eventData.pointerEnter?.GetComponent<inventorySlot>();
        inventorySlot orionalSlot = origionalParent.GetComponent<inventorySlot>();
      if(dropSlot == null)
        {
        GameObject item = eventData.pointerEnter;
            if (item != null)
            {
                dropSlot = item.GetComponentInParent<inventorySlot>();
            }
        }

        if(dropSlot != null)
      {
          if(dropSlot.currentItem != null)
            {
                dropSlot.currentItem.transform.SetParent(origionalParent.transform);
                orionalSlot.currentItem = dropSlot.currentItem;
              dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
           }
           else
            {
               orionalSlot.currentItem = null;
             }
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
                  }
        else
      {
            transform.SetParent(origionalParent);
        }
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
  }
}
