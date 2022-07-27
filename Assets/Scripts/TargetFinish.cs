using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetFinish : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _finishText;
    private Game _game;
    
    public void Initialize(Game game)
    {
        _game = game;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Finish>())
        {
            _finishText.gameObject.SetActive(true);
            _game.NextLevel();
        }

        if (Input.GetMouseButtonDown(0) && collision.gameObject.GetComponent<Finish>())
        {
            Debug.Log("tut");
           
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Finish>())
        {
            _finishText.gameObject.SetActive(false);
        }
    }
}
