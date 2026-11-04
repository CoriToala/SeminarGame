using UnityEngine;

[System.Serializable]
public class CharacterSaveData
{
    public string who;
    public int partyOrder; //can not be 0 as that is starting charcater
    public GameObject myGameObject;
    //dictates order in battle manager, tab manager, snake manager, and spawn 
}
