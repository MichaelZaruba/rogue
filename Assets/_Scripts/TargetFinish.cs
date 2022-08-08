using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetFinish : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _finishText;
    private bool _isFinish;
    private Game _game;
    
    public void Initialize(Game game)
    {
        _game = game;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && _isFinish)
            _game.NextLevel();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Finish>())
        {
            _isFinish = true;
            _finishText.gameObject.SetActive(true);          
        }
    }
 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Finish>())
        {
            _isFinish = false;
            _finishText.gameObject.SetActive(false);
        }
    }
}
