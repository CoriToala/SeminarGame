using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set;}

    [Header("Units")]
    public Unit migiUnit;
    public Unit jordanUnit;
    public Unit finneonUnit;
    public Unit gracieUnit;
    public Unit bofiUnit;

    [Header("BattleHuds")]
    public BattleHud migiHUD;
    public BattleHud gracieHUD;
    public BattleHud bofiHUD;
    public BattleHud finneonHUD;
    public BattleHud jordanHUD;
}
