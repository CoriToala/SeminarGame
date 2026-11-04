using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleManager : MonoBehaviour
{
    private Unit currentEnemy;
    [SerializeField] private battleSystem enemyController;
    [SerializeField] BattleUI battleUIHandler;
    StatsManager statsHandler;
    [SerializeField] GameObject dialogueBox;
    List<string> names = new List<string>();

    CharacterTurns characterManager;

    public static BattleManager Instance { get; private set; }
    
    private void Awake()
    {
        // Check if an instance already exists in the scene
        if (Instance != null && Instance != this)
        {
            // If a duplicate is found, destroy this object and exit
            Destroy(gameObject);
            return;
        }

        // Assign this object as the definitive instance
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        enemyController = GetComponent<battleSystem>();

        statsHandler = GetComponent<StatsManager>();
        characterManager = GetComponent<CharacterTurns>();

        ResetHealth();
    }

    void ResetHealth()
    {
        //will not use this once I add the health to the save data;
        statsHandler.migiUnit.currentHP = statsHandler.migiUnit.maxHP;
        statsHandler.jordanUnit.currentHP = statsHandler.jordanUnit.maxHP;
        statsHandler.finneonUnit.currentHP = statsHandler.finneonUnit.maxHP;
        statsHandler.gracieUnit.currentHP = statsHandler.gracieUnit.maxHP;
        statsHandler.bofiUnit.currentHP = statsHandler.bofiUnit.maxHP;
    }

    public void StartBattle(Unit unit)
    {
        PauseController.SetPause(true);
        //called by Enemy Controller after he hits the player
        currentEnemy = unit;
        battleUIHandler.ShowBattleArt(currentEnemy);
        SendStatData();
    }

    void SendStatData()
    {
        //Set up Battle UI Variables 
        statsHandler.migiHUD.SetHUD(statsHandler.migiUnit);
        statsHandler.jordanHUD.SetHUD(statsHandler.jordanUnit);
        statsHandler.finneonHUD.SetHUD(statsHandler.finneonUnit);
        statsHandler.gracieHUD.SetHUD(statsHandler.gracieUnit);
        statsHandler.bofiHUD.SetHUD(statsHandler.bofiUnit);
    }

    public void EnemyAttacked(int action, string name, int whosHit)
    {
        //can make this into one function after major checks are dne
        string charName ="";
         if (whosHit == 1)
        {
            charName = "Migi";

            statsHandler.migiUnit.currentHP -= action;
            statsHandler.migiHUD.SetHUD(statsHandler.migiUnit);
            //Play a hit animation
            if(statsHandler.migiUnit.currentHP <= 0){
                characterManager.migiDead = true;
                //Change Hud to show as dead (red image)
               // Debug.Log("character registered as dead");
            }
            battleUIHandler.EnemyAction(name, charName);
           // Debug.Log(whosHit.ToString() + " was hit");
        }
        if(whosHit == 2){
            charName = "Jordan";

            statsHandler.jordanUnit.currentHP -= action;
            statsHandler.jordanHUD.SetHUD(statsHandler.jordanUnit);
            if(statsHandler.jordanUnit.currentHP <= 0){
                characterManager.jordanDead = true;
            }
            battleUIHandler.EnemyAction(name, charName);
          //  Debug.Log(whosHit.ToString() + " was hit");
        }
        if(whosHit == 3){
            charName = "Finneon";

            statsHandler.finneonUnit.currentHP -= action;
            statsHandler.finneonHUD.SetHUD(statsHandler.finneonUnit);
            if(statsHandler.finneonUnit.currentHP <= 0){
                characterManager.finneonDead = true;
            }
            battleUIHandler.EnemyAction(name, charName);
           // Debug.Log(whosHit.ToString()+ " was hit");
        }
        if(whosHit == 4){
            charName = "Gracie";

            statsHandler.gracieUnit.currentHP -= action;
            statsHandler.gracieHUD.SetHUD(statsHandler.gracieUnit);
            if(statsHandler.gracieUnit.currentHP <= 0){
                characterManager.gracieDead = true;
            }
            battleUIHandler.EnemyAction(name, charName);
           // Debug.Log(whosHit.ToString() + " was hit");
        }
        if(whosHit == 5){
            charName = "Bofi";

            statsHandler.bofiUnit.currentHP -= action;
            statsHandler.bofiHUD.SetHUD(statsHandler.bofiUnit);
            if(statsHandler.bofiUnit.currentHP <= 0){
                characterManager.bofiDead = true;
            }
            battleUIHandler.EnemyAction(name, charName);
           // Debug.Log(whosHit.ToString() + " was hit");

        } 
        //characterManager.PlayerTurns();
       // Debug.Log("no one was registed as hit");
    }

    public void EnemyDied()
    {
        battleUIHandler.YouWon();
        //StartCoroutine(Wait(1));
        battleUIHandler.HideBattleUI();
        PauseController.SetPause(false);
        //later will tell characterManager to update each unit and calculate additions;
    }

    public void RanFromBattle()
    {
        //StartCoroutine(Wait(1));
        battleUIHandler.HideBattleUI();
        PauseController.SetPause(false);
    }

    public void EnemyActionCompleted()
    {
        //tell ui manage who is picking a move
        //tell character manager to initate player turns
        characterManager.PlayerTurns();
        enemyController.random = 0;  
       // Debug.Log("Enemy Action was completed");
    }

    void HudOverlay(int hudShow)
    {
        statsHandler.migiHUD.PlayersDone();
        statsHandler.jordanHUD.PlayersDone();
        statsHandler.finneonHUD.PlayersDone();
        statsHandler.gracieHUD.PlayersDone();
        statsHandler.bofiHUD.PlayersDone();

        if(hudShow == 1)
        {
            statsHandler.migiHUD.LightUp();
        }
        if(hudShow == 2)
        {
            statsHandler.jordanHUD.LightUp();
        }
        if(hudShow == 3)
        {
            statsHandler.finneonHUD.LightUp();
        }
        if(hudShow == 4)
        {
            statsHandler.gracieHUD.LightUp();
        }
        if(hudShow == 5)
        {
            statsHandler.bofiHUD.LightUp();
        }
    }
    
    public void MigiTurn()
    {
       // Debug.Log("MigiTurn function fired");
        //Set Up so that the corisponding UI will raise slightly above the existing ones (showing whos turn it is)
        HudOverlay(1);

        battleUIHandler.ShowBattleInteractions();
        MakeList(statsHandler.migiUnit);
        StartCoroutine(Wait(2));
        battleUIHandler.UpdateAbilites(names);
    }
    
    public void JordanTurn()
    {
        HudOverlay(2);

        battleUIHandler.ShowBattleInteractions();
        MakeList(statsHandler.jordanUnit);
        StartCoroutine(Wait(2));
        battleUIHandler.UpdateAbilites(names);

    }

    public void FinneonTurn()
    {
        HudOverlay(3);

        battleUIHandler.ShowBattleInteractions();
        MakeList(statsHandler.finneonUnit);
        StartCoroutine(Wait(2));
        battleUIHandler.UpdateAbilites(names);
    }

    public void GracieTurn()
    {
        HudOverlay(4);

        battleUIHandler.ShowBattleInteractions();
        MakeList(statsHandler.gracieUnit);
        StartCoroutine(Wait(2));
        battleUIHandler.UpdateAbilites(names);
    }

    public void BofiTurn()
    {
        HudOverlay(5);

        battleUIHandler.ShowBattleInteractions();
        MakeList(statsHandler.bofiUnit);
        StartCoroutine(Wait(2));
        battleUIHandler.UpdateAbilites(names);
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
    

    void MakeList(Unit unit)
    {
        //add list for ability pp ints
        foreach (GameObject move in unit.blessings)
           {
                Abilities action = move.GetComponent<Abilities>();
                string ability = action.abilityName;
                names.Add(ability);
                //Debug.Log(ability);
                //add it to get int pp value 

           }
        StartCoroutine(CleanUp(names));
        //call clean up for int 
    }

    IEnumerator CleanUp(List<string> junk)
    {   
        yield return new WaitForSeconds(0.05f);
        junk.Clear();
    }

    public void PlayerFinished()
    {
       //Debug.Log("Battle Manager told enemy controller to start enemy turn");
        enemyController.EnemyTurn(currentEnemy);
    }

    public void TeamDied()
    {
        battleUIHandler.YouLost();
        StartCoroutine(Wait(1));
        battleUIHandler.HideBattleUI();
        PauseController.SetPause(false);
    }

    public void ChosenAbilityName(string buttonName)
    {
        int turn = characterManager.whoGoes;
        //print(turn.ToString());
        Unit person = CheckActive(turn);
        GetAbilityDmg(person, buttonName);
    }
    
    public void ChosenAbilityDamage(int damage)
    {
        enemyController.MoveSelected(damage);
    }
        
    private Unit CheckActive(int turn)
    {
        //Debug.Log("Manager is checking who is the active player");

        if (turn == 0){
            enemyController.random = 0;
            return statsHandler.migiUnit;
           //Debug.Log(btnName + "is the name of ability BattleHandler recieved");
        }
        if(turn == 1){
            enemyController.random = 0;
            return statsHandler.jordanUnit;
          //  Debug.Log("Battle Handler knows it's Jordan's turn");
        }
        if(turn == 2){
            enemyController.random = 0;
            return statsHandler.finneonUnit;
          // Debug.Log("Battle Handler knows it's Finns's turn");
        }
        if(turn == 3){
            enemyController.random = 0;
            return statsHandler.gracieUnit;
          //Debug.Log("Battle Handler knows it's Gracie's turn");
        }
        if(turn == 4){
            enemyController.random = 0;
            return statsHandler.bofiUnit;
        }
        else 
        {
            Debug.Log("no unit found");
            return null;
        }
    }

    void GetAbilityDmg(Unit unit, string ability)
    {
        GameObject abilityObj = unit.blessings.Find(obj => obj.name == ability);
        //GameObject abilityChild = Instantiate(abilityObj);
       // Debug.Log("made children");
        Abilities abil = abilityObj.GetComponent<Abilities>();
        int damage = abil.leastDamage;
        enemyController.MoveSelected(damage);
        battleUIHandler.HideMoves();
        battleUIHandler.HideBattleInteracitons();
        StartCoroutine(Waitforturntofinish());
    }

    IEnumerator Waitforturntofinish()
    {
        characterManager.whoGoes += 1;
        yield return new WaitForSeconds(1f);
        characterManager.PlayerTurns();
        //Debug.Log("next plyer turn started");
    }

    public void EquipArmor(int defense, int who)
    {
        Unit person = CheckActive(who);
        //get secondary int from item action and use to determ character from stat manager
        //add denfense to a charater
    }

    public void Heal(int healthGain, int who)
    {
        Unit person = CheckActive(who);
        person.currentHP += healthGain;
        if (person.currentHP > person.maxHP)
        {
            person.currentHP = person.maxHP;
        }
        //get secondary int from item action to determine character
        //add defense to a character 
    }

    public void AddAbility(GameObject newAbility, int who, GameObject abilityItem)
    {
        Unit person = CheckActive(who);
        if (person.blessings.Count < 5){
            person.blessings.Add(newAbility);
            Destroy(abilityItem);
        }

        else{
            
            dialogueBox.SetActive(true);
            MakeList(person);
            ItemPickUpUIController add = dialogueBox.GetComponent<ItemPickUpUIController>();
            add.displayAbilities(newAbility, names, abilityItem, person);
        }
    }

    public void ReplacePlayerAbility(string oldAbility, Unit unit, GameObject invItem, GameObject newAbility)
    {
        GameObject oldAbilObj = unit.blessings.Find(obj => obj.name == oldAbility);
        unit.blessings.Remove(oldAbilObj);
        unit.blessings.Add(newAbility);
        Destroy(invItem);
    }

    //create a script to start the enemies next turn and only do so if all players are not dead
}
