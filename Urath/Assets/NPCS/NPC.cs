using UnityEngine;
using TMPro;
using System.Collections;

public class NPC : MonoBehaviour, Interactable
{
    public NPCdialogue dialogueData;
    [SerializeField] private DialogueController dialogueHandler;
    //public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    private void Start()
    {
        dialogueHandler = DialogueController.Instance;
    }

    public bool CanInteract()
    {
        //print("CanInteract hit");
        return isDialogueActive;
    }

    public void Interact()
    {
        if( dialogueData == null || (PauseController.isGamePaused && !isDialogueActive)){
            //print("returned null");
            return;
        }
        if(isDialogueActive)
        {
            //print("showing next line");
            NextLine();
        }
        else
        {
           // print("started dialogue");
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        dialogueHandler.SetNPCInfo(dialogueData.npcName);
        dialogueHandler.ShowDialogueUI(true);
        PauseController.SetPause(true);

        DisplayFirsytLine();
    }

    void NextLine()
    {
        //Debug.Log("Dialogue Index is" + dialogueIndex.ToString());
        if (isTyping)
        {
            //Skip typing animaion and show full line 
            StopAllCoroutines();
            dialogueHandler.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        //Clear out Choices
        dialogueHandler.ClearChoices();

        //Check endDialogue
        if(dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex]){
            print("End dialogue");
            EndDialogue();
            return;
        }

        //Check if have choices
        foreach(DialogueChoice dialogueChoice in dialogueData.choices){
           // print("Read there is a choice");
           // Debug.Log("dialoguce choice is " + dialogueChoice.dialogueIndex.ToString());
            //Debug.Log("Dialogue Index is" + dialogueIndex.ToString());
            if(dialogueChoice.dialogueIndex == dialogueIndex)
            {
                print("Cycling through choices");
                //display choices
                DisplayChoices(dialogueChoice);
                return;
            }
        }

        if(dialogueIndex + 1 < dialogueData.dialogueLines.Length)
        {
            //if another line, type next line
           // print("start typing line");
            DisplayCurrentLine();
        }
        else{
             print("end line");
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueHandler.SetDialogueText("");
       // print("Typing line");

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex]){
            dialogueHandler.SetDialogueText(dialogueHandler.dialogueText.text += letter);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
            print("Called Next Line");
        }
    }

    void DisplayChoices(DialogueChoice choice)
    {
        for(int i = 0; i < choice.choices.Length; i++){
            int nextIndex = choice.nextDialogueIndexes[i] - 1;
            dialogueHandler.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex));
        }
    }

    void ChooseOption(int nextIndex)
    {
        dialogueIndex = nextIndex;
        dialogueHandler.ClearChoices();
        DisplayCurrentLine();
    }

     void DisplayFirsytLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }
    
    void DisplayCurrentLine()
    {
        dialogueIndex += 1;
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueHandler.SetDialogueText("");
        dialogueHandler.ShowDialogueUI(false);
        PauseController.SetPause(false);
    }
}
