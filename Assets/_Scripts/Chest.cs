using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField] private AttackType attackType;
    [SerializeField] private GameObject _attackPrefab;
    private bool _playerIsNear;
    private bool _isOpenedChest = false;
    public Animator animator;

    public const string EMPTY = "Empty";
    public const string TRIGGER = "Trigger";

    private void Update()
    {
        if (_isOpenedChest)
        {
            animator.Play(EMPTY);
            return;
        }
        if (!_playerIsNear)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _isOpenedChest = true;
            
            animator.Play(EMPTY);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            animator.Play(TRIGGER);
            _playerIsNear = true;
        }
    }
}