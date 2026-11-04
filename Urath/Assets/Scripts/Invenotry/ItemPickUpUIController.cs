using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemPickUpUIController : MonoBehaviour
{
    public static ItemPickUpUIController Instance { get; private set;}

    //[SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueController dialogueHandler;

    [SerializeField] List<TextMeshProUGUI> buttonList = new List<TextMeshProUGUI>();
    [SerializeField] GameObject btns;
    GameObject newAbility;
    GameObject itemAbility;
    Unit person;
    BattleManager battleHandler;

    private bool isTyping;
    //public int maxPopups = 5;
    //public float popupDuration;

    private readonly Queue<GameObject> activePopups = new();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            {
                //Debug.LogError("Multiple ItemPickUPUIController instacnces");
                Destroy(gameObject);
            }
        }
        dialogueHandler = DialogueController.Instance;
    }

    private void Start()
    {
        btns.SetActive(false);
        battleHandler = Object.FindAnyObjectByType<BattleManager>();
    }

    public void ShowItemPickUp(string itemName)
    {
        //print("Couln't find dialogueHandler");
        dialogueHandler.ShowDialogueUI(true);
        PauseController.SetPause(true);

        string pickup = "Migi picked up a " + itemName;
        
        StartCoroutine(TypeLine(pickup));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueHandler.SetDialogueText("");

        foreach(char letter in line){
            dialogueHandler.SetDialogueText(dialogueHandler.dialogueText.text += letter);
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }

    public void FinishedReading()
    {
        StopAllCoroutines();

        dialogueHandler.SetDialogueText("");
        dialogueHandler.ShowDialogueUI(false);
        PauseController.SetPause(false);
    }

    public void displayAbilities(GameObject ability, List<string> names, GameObject item, Unit unit)
    {
        newAbility = ability;
        itemAbility = item;
        person = unit;
        dialogueHandler.ShowDialogueUI(true);

        StartCoroutine(Wait());

        if(!isTyping){
            string tooMany = "This character has too many abilities. Would you like to replace one of them?";
            StartCoroutine(TypeLine(tooMany));
        }

        int Count = Mathf.Min(buttonList.Count, names.Count);
        for (int i = 0; i < Count; i++){
            if (buttonList[i] != null){
                    Debug.Log(names[i]);
                    buttonList[i].text = names[i];
                    //Debug.Log(name);
            }
        }
        btns.SetActive(true);
        //Debug.Log("Continued");
    }
    
    IEnumerator Wait()
    {
       // Debug.Log("Wait called");
        yield return new WaitForSeconds(1.5f);
        //Debug.Log("Wait finished");
    }

    public void replaceAbility(string abilityName)
    {
        battleHandler.ReplacePlayerAbility(abilityName, person, itemAbility, newAbility);
        dialogueHandler.ShowDialogueUI(false);
        btns.SetActive(false);
    }

    public void dontReplace()
    {
        dialogueHandler.ShowDialogueUI(false);
        btns.SetActive(false);
    }
}
