using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] private int _levelPlayer = 1;
    private int _experience;

    public int LevelPlayer { get => _levelPlayer; set => _levelPlayer = value; }
    public int AnExperience { get => _experience; set => _experience = value; }

    public static Experience _instance;

    private void Awake()
    {
        _instance = this;
    }
}
