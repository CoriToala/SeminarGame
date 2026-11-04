using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterElementController : MonoBehaviour
{
    //list for inventories
    //list for battlehuds
    int partyNumber = 0;
    //Character Turns 
    //make so battle hander tells the enemy unit that it can't hit characters above that number 
    [SerializeField] private SnakeManager spawnController;
    private List<GameObject> characterModels = new List<GameObject>();
    private TabController inventoryNames;

    // Start is called before the first frame update
    void Start()
    {
        //SnakeManager spawnController = Object.FindAnyObjectByType<SnakeManager>();
    }

    public List<CharacterSaveData> GetCharacterData()
    {
        List<CharacterSaveData> characterData = new List<CharacterSaveData>();
        partyNumber = spawnController.gameObject.transform.childCount;
        if(partyNumber != 0){
            for (int i = 1; i < partyNumber; i++ ){
                GameObject child = spawnController.gameObject.transform.GetChild(i).gameObject;
                characterData name = child.GetComponent<characterData>();

                characterData.Add(new CharacterSaveData { who = name.who, partyOrder = i, myGameObject = child });
            }
            return characterData;
        }
        else{
            print("Only 1 Migi");
            return null;
        }
        //will report snake manager body size 
            // all will report their sibling index as their party number (0 based so it works)
            //
    } 

    public int GetPartySize()
    {
        partyNumber = spawnController.gameObject.transform.childCount;
        return partyNumber;
    }

    // Update is called once per frame
    public void AddCharacterInformation(GameObject newCharacter)
    {
        spawnController.bodyParts.Add(newCharacter);
        //Update inventory size 

        //Tell Battle HUD not to show this hud
    }

    public void SendCharactrModels(int size, List<CharacterSaveData> characterData)
    {
        if(size > 0){
            if(size != spawnController.bodyParts.Count){
                //Set to first delete all the people in body parts. count (minus the first one)
                foreach (CharacterSaveData data in characterData){
                spawnController.bodyParts.Add(data.myGameObject); //add my this gameObject to the movemnt script
                print(data.who + " is child number " + data.partyOrder.ToString());
                //correctly name inventory tabs 
                }
            }
        }
    }
}
