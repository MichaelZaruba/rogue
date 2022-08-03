using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
   public EnemyType Type = EnemyType.Small;
}
public enum EnemyType
{
    Big,
    Medium,
    Small
}
