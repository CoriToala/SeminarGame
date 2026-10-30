using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemPickUpUIController : MonoBehaviour
{
    public static ItemPickUpUIController Instance { get; private set;}

    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] List<TextMeshProUGUI> buttonList = new List<TextMeshProUGUI>();
    [SerializeField] GameObject btns;
    GameObject newAbility;
    GameObject itemAbility;
    Unit person;
    BattleManager battleHandler;
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
                Debug.LogError("Multiple ItemPickUPUIController instacnces");
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
        btns.SetActive(false);
        battleHandler = UnityEngine.Object.FindObjectOfType<BattleManager>();
    }

    public void ShowItemPickUp(string itemName)
    {
        this.gameObject.SetActive(true);
        //og code had it instantiating a popup prefab
        //TextMeshProUGUI dialogueBox = dialogueText.GetComponent<TextMeshProUGUI>();
        //dialogueBox.text = "Migi pick up a " + itemName;
        Debug.Log(itemName);
    }

    public void FinishedReading()
    {
        this.gameObject.SetActive(false);
      //  activePopups.Enqueue(dialogueText);
      //  if(activePopups.Count > 1)
      //  {
      //      activePopups.Dequeue();
      //  }
      //  else{
       //     this.gameObject.SetActive(false);
        //}
    }

    public void displayAbilities(GameObject ability, List<string> names, GameObject item, Unit unit)
    {
        newAbility = ability;
        itemAbility = item;
        person = unit;
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "This character has too many abilities. Would you like to replace one of them?";

        int Count = Mathf.Min(buttonList.Count, names.Count);
        for (int i = 0; i < Count; i++){
            if (buttonList[i] != null){
                    Debug.Log(names[i]);
                    buttonList[i].text = names[i];
                    //Debug.Log(name);
            }
        }
        
        StartCoroutine(Wait());

        //Debug.Log("Continued");
    }
    
    IEnumerator Wait()
    {
       // Debug.Log("Wait called");
        yield return new WaitForSeconds(2f);
        dialogueText.text = "Would you like to replace one of them?";
        btns.SetActive(true);
        //Debug.Log("Wait finished");
    }

    public void replaceAbility(string abilityName)
    {
        battleHandler.ReplacePlayerAbility(abilityName, person, itemAbility, newAbility);
        this.gameObject.SetActive(false);
        btns.SetActive(false);
    }

    public void dontReplace()
    {
        dialogueText.gameObject.SetActive(false);
        btns.SetActive(false);
    }
}
