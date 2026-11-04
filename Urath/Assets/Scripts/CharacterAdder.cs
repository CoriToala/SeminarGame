using UnityEngine;

public class CharacterAdder : MonoBehaviour, Interactable
{
    [SerializeField] string characterName; //a string for this name 
    public string characterID { get; private set; }
    private bool wasAdded = false;
    
    //happens when you click on charcter
    //disply text for who you want to add

    [SerializeField] GameObject characterToAdd; //character you wants game object
    private CharacterElementController characterHandler;

    void Start()
    {
        characterHandler = FindAnyObjectByType<CharacterElementController>();
        characterID ??= GlobalHelper.GenerateUniqueID(gameObject);
    }

    public bool CanInteract()
    {
        return wasAdded;
    }

    public void Interact()
    {
        CanAddCharacter();
    }

    private void CanAddCharacter()
    {
        //display text to add character 

    }

    public void WantToAddCharacter()
    {
        //response to button yes being pushed
        if (characterToAdd){
            characterHandler.AddCharacterInformation(characterToAdd);
            wasAdded = true;
            Destroy(gameObject);
        }
        else{
            print("Can not add character");
            wasAdded = false;
        }
    }

    public void DontAddCharacter()
    {
        wasAdded = false;
        //do nothing with this charcater
    }

}
