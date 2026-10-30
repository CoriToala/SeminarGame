using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    private AbilityDictionary abilityDictionary;
    private StatsManager statHandler;
    [SerializeField] private GameObject[] characterUnits;

    void Start()
    {
        abilityDictionary = FindObjectOfType<AbilityDictionary>();
        statHandler = FindObjectOfType<StatsManager>();
    }

    public List<AbilitySaveData> SaveAbilities()
    {
        List<AbilitySaveData> abilData = new List<AbilitySaveData>();
        {
            //need to go through each inventory
            for (int i = 0; i < 5; i++)
            {
                if (characterUnits[i] != null){
                    Unit unit = characterUnits[i].GetComponent<Unit>();
                    foreach(GameObject abilityList in unit.blessings)
                    {
                        Abilities abil = abilityList.GetComponent<Abilities>();
                        abilData.Add(new AbilitySaveData { abilityID = abil.ID, whosSpell = i });
                        Debug.Log("Saved Data: Object Number " + abil.ID.ToString());
                        Debug.Log("Saved Data: Person Number " + i.ToString());
                    }
                }
            }
            foreach( AbilitySaveData ability in abilData )
            {
                print(ability.abilityID + " and " + ability.whosSpell); 
            }
            return abilData;
        }
    }

    public void SetAbilityItems(List<AbilitySaveData> abilityData)
    {
        foreach (GameObject obj in characterUnits)
        {
           Unit unit = obj.GetComponent<Unit>();
           unit.blessings.Clear();
           Debug.Log("Cleared");
        }


        //StartCoroutine(Wait(abilityData));
        foreach(AbilitySaveData data in abilityData)
        {
            print(data);
            Debug.Log("Sent Data: Object Number" + data.abilityID.ToString());

            Debug.Log("Sent Data: Person Number " + data.whosSpell.ToString());
            UpdateAbilities(data);
        }
    }

    public void UpdateAbilities(AbilitySaveData data)
    {
        //StartCoroutine(Wait());
            Debug.Log("Updated spells for " + data.whosSpell.ToString());
            GameObject who = characterUnits[data.whosSpell];
            GameObject abilityPrefab = abilityDictionary.GetAbilityPrefab(data.abilityID);
            if(abilityPrefab != null)
            {
                //Debug.Log("Updated spells for " + data.whosSpell.ToString());
                Unit unit = who.GetComponent<Unit>();
                unit.blessings.Add(abilityPrefab);
            }
    }

    IEnumerator Wait()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(2f);
        Debug.Log("Done wait");
    }
}
