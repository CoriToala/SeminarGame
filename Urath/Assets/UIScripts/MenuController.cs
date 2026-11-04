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

    public void DisableOnStart()
    {
      menuCanvas.SetActive(false);
      inventoryCanvas.SetActive(false);
    }

    IEnumerator TurnOffCanvas()
    {
      yield return new WaitForSeconds(0.5f);
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed && !menuCanvas.activeSelf){
          if(!inventoryCanvas.activeSelf && PauseController.isGamePaused){
            return;
          }
          inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);  
          PauseController.SetPause(inventoryCanvas.activeSelf);
        }
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.performed && !inventoryCanvas.activeSelf){
          if(!menuCanvas.activeSelf && PauseController.isGamePaused){
            return;
          }
          menuCanvas.SetActive(!menuCanvas.activeSelf);
          PauseController.SetPause(menuCanvas.activeSelf);
        }
    }
}
