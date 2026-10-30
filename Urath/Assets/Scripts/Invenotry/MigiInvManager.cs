using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MigiInvManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;

    public void CreateSlots(GameObject slotPrefab, int invSize)
    {
        for(int i = 0; i < invSize; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }
    }
}
