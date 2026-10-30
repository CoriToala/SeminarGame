using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class battleSystem : MonoBehaviour
{
    
    Unit enemyUnit;

    int turnNumber;
    public int random = 0;

    [SerializeField] BattleManager battleHandler;

    //[Header("Who Died")]
    //int allDead = 0;
    //bool migiDead = false;
    //bool gracieDead = false;
    //bool finneonDead = false;
    //bool jordanDead = false;
    //bool bofiDead = false;
    //player unit should not be shown on screen but it will contain data like health and other aspects 
    //there will need to be one per character

    void Start()
    {
        battleHandler = Object.FindAnyObjectByType<BattleManager>();
    }

    public void EnemyTurn(Unit unit)
    {    
        enemyUnit = unit;
        string name = unit.ChosenAttackName();
        int attackDamage = unit.ChosenAttack();

        if(random == 0){
            random = Random.Range(1, 6);
            Debug.Log("Enemy Controller Picked a Random Attack");
            battleHandler.EnemyAttacked(attackDamage, name, random);
            unit.ResetRandom();
        }
    }


    public void MoveSelected(int damage)
    {
        //call this through battle Manager after player picks move
        CheckEnemyDead(damage, enemyUnit);
    }

    void CheckEnemyDead(int dmg, Unit unit)
    {
        bool isDead = unit.TakeDamage(dmg);
        if(isDead)
        {
            battleHandler.EnemyDied();
            unit.EnemyDied();
        }
        else
        {
            battleHandler.EnemyActionCompleted();
        } 
    }

    
}
