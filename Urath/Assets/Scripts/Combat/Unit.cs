using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public string uniqueMessage;
   // public GameObject Body;
    [SerializeField] public List<GameObject> blessings = new List<GameObject>();
    
    public int unitLevel;
    public int maxHP;
    public int currentHP;
    public int maxPP;
    public int currentPP;
    public Sprite mySprite;

    public int randomAttack;

    BattleManager battleHandler;

    //can adda list using scriptable objects each with the moves name with also max and least amount of damage possible
    //from list have a roll done to pick which attack will hit (roll is only done for the enemy attack)
    void Start()
    {
        if(currentHP <= 0){
            currentHP = maxHP;
        }
        
        battleHandler = UnityEngine.Object.FindAnyObjectByType<BattleManager>();
    }

    public void ResetRandom()
    {
        randomAttack = 0;
    }

    public string ChosenAttackName()
    {
        if(randomAttack == 0){
            Random.Range(0, blessings.Count + 1);
            GameObject ability = blessings[randomAttack];
            Abilities obj = ability.GetComponent<Abilities>();
            string attackName = obj.abilityName;
            return attackName;
        }
        else
            return null;
    }

    public int ChosenAttack()
    {
        if(randomAttack == 0)
        {
            Random.Range(0, blessings.Count + 1);
            GameObject ability = blessings[randomAttack];
            Abilities obj = ability.GetComponent<Abilities>();
            string attackName = obj.abilityName;
            int randomDamage = obj.leastDamage;
            return randomDamage;
        }
        else
            return 0;
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if(currentHP <= 0)
            return true;
        else
            return false;
    }

    public void EnemyDied()
    {
        Destroy(this.gameObject);
    }
}
