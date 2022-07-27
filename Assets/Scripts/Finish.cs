using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Finish : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI _finishText;
    private Game _game;
    public void Initialize(Game game)
    {
        _game = game;
    }

}
