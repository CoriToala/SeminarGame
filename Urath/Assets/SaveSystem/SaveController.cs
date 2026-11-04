using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Cinemachine;
using System.Linq;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private inventoryController inventoryController;
    private AbilityController abilityController;
    [SerializeField]private CharacterElementController characterController;
    SnakeManager spawnController;

    private Chest[] chests;

    void Start()
    {
        //define save location
        
        InitializeComponents();
        LoadGame();
    }

    private void  InitializeComponents()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController = GetComponent<inventoryController>();
        abilityController = GetComponent<AbilityController>();
        characterController = GetComponent<CharacterElementController>();
        spawnController = FindAnyObjectByType<SnakeManager>();

        chests = Object.FindObjectsByType<Chest>(FindObjectsSortMode.None);
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            mapBoundary = Object.FindAnyObjectByType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name,
            inventorySaveData = inventoryController.GetInventoryItems(),
            abilitySaveData = abilityController.SaveAbilities(),
            partySize = characterController.GetPartySize(),
            characterSaveData = characterController.GetCharacterData(),
            chestSaveData = GetChestsState(),
            //HowManyPlayers
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    private List<ChestSaveData> GetChestsState()
    {
        List<ChestSaveData> chestStates = new List<ChestSaveData>();
        foreach(Chest chest in chests){
            ChestSaveData chestSaveData = new ChestSaveData{
                chestID = chest.chestID,
                isOpened = chest.isOpened,
            };
            chestStates.Add(chestSaveData);
        }
        return chestStates;
    }

    public void LoadGame()
    {
        if(File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            //Change to send position to charcater controller 
            

            Object.FindAnyObjectByType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
            
            inventoryController.SetInventoryNumber(saveData.partySize);
            //print("Number of Inventories Set");
            inventoryController.SetInventoryItems(saveData.inventorySaveData);
            //print("Items in Inventory Sent");
            characterController.SendCharactrModels(saveData.partySize, saveData.characterSaveData); //Spawn in currently unlocked characters
            spawnController.startingPosition = saveData.playerPosition;

            abilityController.SetAbilityItems(saveData.abilitySaveData);
           // GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;

            LoadChestStates(saveData.chestSaveData);
        }
        else
        {
            print("No Current Save");
            SaveGame();
            inventoryController.SetInventoryNumber(1);
            inventoryController.SetInventoryItems(new List<InventorySaveData>());
        }
    }

    private void LoadChestStates(List<ChestSaveData> chestStates)
    {
        foreach(Chest chest in chests){
            ChestSaveData chestSaveData = chestStates.FirstOrDefault(c => c.chestID == chest.chestID);

            if(chestSaveData != null){
                chest.SetOpened(chestSaveData.isOpened);
            }
        }
    }
}
