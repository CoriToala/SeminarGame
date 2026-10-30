using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterElementController : MonoBehaviour
{
    //list for inventories
    //list for battlehuds
    int partyNumber;
    //Character Turns 
    //make so battle hander tells the enemy unit that it can't hit characters above that number 
    SnakeManager spawnController;

    // Start is called before the first frame update
    void Start()
    {
        SnakeManager spawnController = FindObjectOfType<SnakeManager>();
    }

    //Will have to add to save controller so that it will add snake manager components
    //Call a method for it to spawn everthing in at start rather than it running on it's own on awake (Could also give starting position so that it doesnt glitch at first)

    // Update is called once per frame
    public void AddCharacterInformation(Unit newUnit, GameObject newCharacter, int TurnNumber)
    {
        partyNumber += 1;
        //party number tells us how many people are in the party 
        //turn number tells us who is being added
        //Add GameObject to Snake Manager
        spawnController.bodyParts.Add(newCharacter);
 


        RevealInfo(TurnNumber);
        //Tell Battle HUD not to show this hud
        //tell inventory not to show this inventory 
    }

    void RevealInfo(int who)
    {
        if(who == 1)
        {
            //Tell BattleUI to show this hud
                //Edit how it is told in battleManager --> Have it call a funcion for an int and depending on that int it will show the neceassary huds and impact enemy unit script
                //Tell Character Manager that people above this number can not have a turn 
            //tell inventory to show this inventory 
        }
        if(who == 2)
        {

        }
    }
}
