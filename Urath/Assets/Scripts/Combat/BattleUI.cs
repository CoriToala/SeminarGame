using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class BattleUI : MonoBehaviour
{

    [HideInInspector] public Unit enemy;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image enemyImage;
    [SerializeField] private Image background;

    [SerializeField] private GameObject abilityInputs;

    [SerializeField] List<TextMeshProUGUI> buttonList = new List<TextMeshProUGUI>();

    [SerializeField] private GameObject normalInputs;
    //[SerializeField] private GameObject battleUI;
    //[SerializeField] private GameObject battleHudParent;

    [SerializeField]BattleManager battleHandler;
    public int random = 0;

    void Start()
    {
        this.gameObject.SetActive(false);
        normalInputs.SetActive(false);
        abilityInputs.SetActive(false);

        battleHandler = UnityEngine.Object.FindAnyObjectByType<BattleManager>();
    }


    public void HideBattleInteracitons()
    {
        this.gameObject.SetActive(true);
        normalInputs.SetActive(false);
    }

    public void ShowBattleInteractions()
    {
       // Debug.Log("Show Battle Interactions was called");
        normalInputs.SetActive(true);
    }

    public void HideBattleUI()
    {
        abilityInputs.SetActive(false);
        normalInputs.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void HideMoves()
    {
        abilityInputs.SetActive(false);
    }

    public void ShowBattleArt(Unit unit)
    {
        this.gameObject.SetActive(true);
        normalInputs.SetActive(false);
        HideMoves();
        //enemyImage.enabled = true;
        SetEnemyImage(unit.mySprite);
        //background.enabled = true;
        enemy = unit;
        StartText();
    }

    void SetEnemyImage(Sprite newSprite)
    {
        enemyImage.sprite = newSprite;
    }

    IEnumerator MovetoCombat()
    {
       //Debug.Log("Started Wait function");
        yield return new WaitForSeconds(2f);
       //Debug.Log("tried to talk to battle mamager");
       battleHandler.PlayerFinished();
       random = 0;
       //Debug.Log("Told Battle Manager to start enemy turn");
    }

    void StartText()
    {
        //float time = Time.timeScale;
        //Debug.Log("Time " + time.ToString());
        string opt1 = "A wild " + enemy.unitName + " appeared!";
        string opt2 = enemy.uniqueMessage;
        if(random == 0){
            random = UnityEngine.Random.Range(1, 3);
            if(random == 1){
                dialogueText.text = opt1;
            }
            else{
                dialogueText.text = opt2;
            }
            //Debug.Log("Made Start Text");
            //Debug.Log("tried to talk to battle mamager")
        }
        StartCoroutine(MovetoCombat());
    }

    public void YouWon()
    {
        dialogueText.text = "CONGRATULATIONS!";
        HideBattleUI();
        //add so it says what levels or stat improvments you recieved;
    }

    public void YouLost()
    {
        dialogueText.text = "You Lost. Better luck next time.";
        HideBattleUI();
        //add unique text about dying and initiate that state 
    }

    IEnumerator WaitToDoPlayerAction()
    {
        //Debug.Log("EnemyAcction");
        yield return new WaitForSeconds(2f);
        battleHandler.EnemyActionCompleted();

    }

    public void EnemyAction(string attackUsed, string name)
    {
       // Debug.Log(enemy.unitName + " attacks " + name + " using an " + attackUsed);
        dialogueText.text = enemy.unitName + " attacks " + name + " using an " + attackUsed;
        StartCoroutine(WaitToDoPlayerAction());
    }

    public void UpdateAbilites(List<string> movesNames) 
    {
        //print(movesNames.Count.ToString());
        int Count = Mathf.Min(buttonList.Count, movesNames.Count);
        for (int i = 0; i < Count; i++){
            if (buttonList[i] != null){
                buttonList[i].text = movesNames[i];
                //add so that the button size changes depedning on the text size (may need to switch to text mesh pro) 
            }
        }
        //add so that it will go through a list of pp values which will be next to the button
    }

    public void OnAttackButton()
    {
        abilityInputs.SetActive(true);
    }

    public void OnRunButton()
    {
        dialogueText.text = "you ran away like a coward";
        battleHandler.RanFromBattle();
        HideBattleUI();
    }
}
