using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text nameText;
    public Text ppValue;
    public Text healthValue;

    void Awake()
    {
        healthValue.text = string.Empty;
        ppValue.text = string.Empty;
    }

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        healthValue.text = unit.currentHP.ToString();
        ppValue.text = unit.currentPP.ToString();
    }

    public void SetHP(int hp)
    {
        healthValue.text = hp.ToString(); 
    }

    public void SetPP(int pp)
    {
        ppValue.text = pp.ToString();
    }
}
