using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeEnemyAttack : MonoBehaviour, IAttacker
{

    [SerializeField] protected EnemyAttackType _type;


    public virtual void MeleeAttack()
    {
       
    }

    public virtual void RangeAttack()
    {
        throw new System.NotImplementedException();
    }
}
public enum EnemyAttackType
{
    Melee,
    Range
}
