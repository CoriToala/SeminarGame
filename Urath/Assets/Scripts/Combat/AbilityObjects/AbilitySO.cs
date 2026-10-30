using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class AbilitySO : ScriptableObject
{
   [field: SerializeField]
   public int ppCost;
   
   [field: SerializeField]
   public string nameAbility;

   [field: SerializeField]
   public int damage;

   [field: SerializeField]
   public string statusEffect;

   [field: SerializeField]
   public int heal;
}
