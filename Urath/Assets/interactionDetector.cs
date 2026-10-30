using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class interactionDetector : MonoBehaviour
{
    private Interactable interactableInRange = null;

    public void OnInteract(InputAction.CallbackContext context)
    {
       if (context.performed)
      {
           interactableInRange?.Interact();
           Debug.Log("Interact Button pushed");
      }
   }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  Debug.Log("Trigger HIt");
        if(collision.TryGetComponent(out Interactable interactable))
        {
           // Debug.Log("Read as an interaction");
            bool state = interactable.CanInteract();
            if(!state)
            {
              //  Debug.Log("Could Innteract");
                interactableInRange = interactable;
               // Debug.Log("Found Interaction");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Interactable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
          //  Debug.Log("Lost interaction");
        }
    }
}
