using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDictionary : MonoBehaviour
{
    public List<Abilities> abilityPrefabs;
    private Dictionary<int, GameObject> abilityDictionary;

    void Awake()
    {
        abilityDictionary = new Dictionary<int, GameObject>();
        
        //auto increments
        for(int i = 0; i < abilityPrefabs.Count; i++)
        {
            if(abilityPrefabs[i] != null)
            {
                abilityPrefabs[i].ID = i + 1;
                //Debug.Log("Ability Dictionary Number" + i.ToString());
            }
        }

        foreach(Abilities ability in abilityPrefabs)
        {
            abilityDictionary[ability.ID] = ability.gameObject;
        }
    }

    public GameObject GetAbilityPrefab(int abilityID)
    {
        abilityDictionary.TryGetValue(abilityID, out GameObject prefab);
        if(prefab == null)
        {
            //Debug.Log($"Ability with ID {abilityID} not found in dictionary");
        }
        return prefab;
    }
}
