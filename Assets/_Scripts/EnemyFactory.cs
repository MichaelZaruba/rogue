using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class EnemyFactory : GameObjectFactory
{
    [SerializeField] private Enemy _bokalPrefab;
    [SerializeField] private Enemy _panetaPrefab;
    [SerializeField] private Enemy _bacteriaPrefab;
    [SerializeField] private Enemy _chomperPrefab;

    public void Reclaim(Enemy content)
    {
        Destroy(content.gameObject);
    }

    public Enemy Get(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Bokal:
                return Get(_bokalPrefab);
            case EnemyType.Paneta:
                return Get(_panetaPrefab);
            case EnemyType.Bacteria:
                return Get(_bacteriaPrefab);
            case EnemyType.Chomper:
                return Get(_chomperPrefab);
        }
        return null;
    }

    private T Get<T>(T prefab) where T : Enemy
    {
        T instance = CreateGameObjectInstance(prefab);
        return instance;
    }
}