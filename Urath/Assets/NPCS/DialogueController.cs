using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText, nameText;
    public Transform choicePanel;
    public GameObject choiceButtonPrefab;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        dialoguePanel.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }

    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show);
        dialogueText.gameObject.SetActive(show);
    }

    public void SetNPCInfo(string npcName)
    {
        nameText.text = npcName;
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }
    
    public void ClearChoices()
    {
        foreach (Transform child in choicePanel) {
            Destroy(child.gameObject);
        }
    }

    public void CreateChoiceButton(string choiceText, UnityAction whenClicked)
    {
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choicePanel);
        //print("instantiated button");
        //Debug.Log(choiceText);
        TextMeshProUGUI buttonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = choiceText;
        //print("updated button text");
        Button btn = choiceButton.GetComponent<Button>();
        btn.onClick.AddListener(whenClicked);
    }
}
