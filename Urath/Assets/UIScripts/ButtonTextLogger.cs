using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonTextLogger : MonoBehaviour
{
  
    BattleManager battleHandler;
    [SerializeField]ItemPickUpUIController abilityAdder;


    void Start()
    {
      battleHandler = UnityEngine.Object.FindAnyObjectByType<BattleManager>();
      abilityAdder = UnityEngine.Object.FindAnyObjectByType<ItemPickUpUIController>();
    }
    
    public void OnBattleButton()
    {
        Text myText = GetComponentInChildren<Text>();
        string name = myText.text;
        //Debug.Log(name);
        battleHandler.ChosenAbilityName(name);
    }

    public void OnDialogueButton()
    {
        TextMeshProUGUI myText = GetComponentInChildren<TextMeshProUGUI>();
        string name = myText.text;
        Debug.Log(name);
        abilityAdder.replaceAbility(name);
    }
}
