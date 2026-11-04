using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public string mapBoundary; //storing the name of the area (will use when adding map transitions)
    public List<InventorySaveData> inventorySaveData;
    public List<AbilitySaveData> abilitySaveData;
    public List<CharacterSaveData> characterSaveData;
    public int partySize;

    public List<ChestSaveData> chestSaveData;
}

[System.Serializable]
public class ChestSaveData
{
    public string chestID;
    public bool isOpened;
}
