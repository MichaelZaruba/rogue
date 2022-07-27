using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] public SpawnPlayer SpawnPlayer;
    [SerializeField] public Finish Finish;
    [SerializeField] public List<SpawnEnemy> SpawnsEnemies = new List<SpawnEnemy>();
    [SerializeField]  public List< Ground> Grounds = new List<Ground>();

    public void Initialize()
    {
        Finish = GetComponentInChildren<Finish>();
        SpawnPlayer = GetComponentInChildren<SpawnPlayer>();

        SpawnEnemy[] spawnsEnemies = GetComponentsInChildren<SpawnEnemy>();
        foreach (var spawnEnemy in spawnsEnemies)
        {
            SpawnsEnemies.Add(spawnEnemy);
        }

        Ground[] grounds = GetComponentsInChildren<Ground>();
        foreach(var ground in grounds)
        {
            Grounds.Add(ground);
        }
    }

}
