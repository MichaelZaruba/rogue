using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool _playerIsNear;
    private bool _isOpenedChest = false;
    public Animator animator;

    private void Update()
    {
        if (_isOpenedChest)
        {
            animator.Play("Empty");
            return;
        }
        if (!_playerIsNear)
            return;
    
        if (Input.GetKeyDown(KeyCode.E))
            {
            _isOpenedChest = true;
            animator.Play("Empty");
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            animator.Play("Trigger");
            _playerIsNear = true;
        }
    }
}