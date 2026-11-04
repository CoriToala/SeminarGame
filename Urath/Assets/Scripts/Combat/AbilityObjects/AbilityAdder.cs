using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityAdder : MonoBehaviour
{
    [SerializeField] GameObject dialogueText;
    [SerializeField] List<Button> buttonList = new List<Button>();
    [SerializeField] GameObject btns;
    GameObject newAbility;
    GameObject itemAbility;
    Unit person;
    BattleManager battleHandler;

    void Start()
    {
        btns.SetActive(false);
        dialogueText.SetActive(false);
        battleHandler = Object.FindAnyObjectByType<BattleManager>();

    }
    
    public void displayAbilities(GameObject ability, List<string> names, GameObject item, Unit unit)
    {
        newAbility = ability;
        itemAbility = item;
        person = unit;
        dialogueText.SetActive(true);
        dialogueText.GetComponent<TextMeshProUGUI>().text = "This character has too many abilities. Would you like to replace one of them?";
        
        StartCoroutine(Wait(1));
        int Count = Mathf.Min(buttonList.Count, names.Count);
        for (int i = 0; i < Count; i++){
            if (buttonList[i] != null){
                foreach (Button btn in buttonList)
                {
                    TextMeshProUGUI button = btn.GetComponentInChildren<TextMeshProUGUI>();
                    button.text = names[i];
                } 
            }
        }

        btns.SetActive(true);
    }
    
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void replaceAbility(string abilityName)
    {
       battleHandler.ReplacePlayerAbility(abilityName, person, itemAbility, newAbility);
    }

    public void dontReplace()
    {
        dialogueText.SetActive(false);
        btns.SetActive(false);
    }
}
