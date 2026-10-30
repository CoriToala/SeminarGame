using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject inventoryCanvas;
    // Start is called before the first frame update
    void Start()
    {
       menuCanvas.SetActive(false); 
       inventoryCanvas.SetActive(false);
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed){
          inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);  
        }
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.performed){
          menuCanvas.SetActive(!menuCanvas.activeSelf);  
        }
    }
}
