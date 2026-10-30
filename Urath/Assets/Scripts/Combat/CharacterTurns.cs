using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterTurns : MonoBehaviour
{
    
    BattleManager battleHandler;

    [HideInInspector] public  int whoGoes = 0;
    int dead;

    [HideInInspector] public  bool migiDead = false;
    [HideInInspector] public bool jordanDead = false;
    [HideInInspector] public bool finneonDead = false;
    [HideInInspector] public bool gracieDead = false;
    [HideInInspector] public bool bofiDead = false;

    void Start()
    {
        battleHandler = UnityEngine.Object.FindAnyObjectByType<BattleManager>();
    }

    public void PlayerTurns()
    {
        if(whoGoes == 0){
            if(!migiDead){
                battleHandler.MigiTurn();
           //    Debug.Log("Character Turns started migi turn");
            }
        }
        else{
            JordanTurn();
        }
       // Debug.Log(whoGoes.ToString() + " is next in turn order");
    }
    

    void JordanTurn()
    {
        if(whoGoes == 1)
        {
                if(!jordanDead){
                battleHandler.JordanTurn();
              //  Debug.Log("Character Turns started Jordans turn");
            }
        }
        else
        {
            FinnTurn();
        }
    }
    
    void FinnTurn()
    {
        if(whoGoes == 2){
            if(!finneonDead){
                battleHandler.FinneonTurn();
               // Debug.Log("Character Turns started finneon turn");
            }
        }
        else
        {
            GracieTurn();
        }
    }

    void GracieTurn()
    {
        if(whoGoes == 3){
            if(!gracieDead){
                battleHandler.GracieTurn();
              //  Debug.Log("Character Turns started Gracie's turn");
            }
        }
        else
        {
            BofiTurn();
        }
    }
    void BofiTurn()
    {
        if(whoGoes == 4){
            if(!bofiDead){
                battleHandler.BofiTurn();
              //  Debug.Log("Character Turns started Bofi's turn");
            }
        }
        else
        {
            CheckLife();
        }
    }

    private void CheckLife()
    {
        if(migiDead && jordanDead && finneonDead && gracieDead && bofiDead){
            battleHandler.TeamDied();
            whoGoes = 0;
          //  Debug.Log("All read as dead");
        }
        else if(whoGoes >= 5){
            battleHandler.PlayerFinished();
            whoGoes = 0;
         //   Debug.Log("Turns Completed");
        }
        else{
            PlayerTurns();
          //  Debug.Log("went too fast");
        }
    }
}
