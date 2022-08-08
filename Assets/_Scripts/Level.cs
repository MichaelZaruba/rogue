using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
     public SpawnPlayer SpawnPlayer;
     public Finish Finish;
     public List<SpawnEnemy> SpawnsEnemies = new List<SpawnEnemy>();
     public List< Ground> Grounds = new List<Ground>();
     public List<Decorate> Decorate = new List<Decorate>();
     public List<Chest> Chess = new List<Chest>();
     public List<GameItem> GameItem = new List<GameItem>();

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
