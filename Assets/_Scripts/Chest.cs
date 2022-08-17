using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Chest : MonoBehaviour
{

    [SerializeField] private AttackType _attackType;
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private Light2D _light;

     private bool _playerIsNear;

    public Animator Animator;

    public const string CLOSE = "Close";
    public const string OPEN = "Open";

    private void Update()
    {

        if (!_playerIsNear)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponent<Animator>().enabled = false;
            _light.gameObject.SetActive(false);
            var sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.sprite = _emptySprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            _light.gameObject.SetActive(true);
            Animator.Play(OPEN);
            _playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
            {
            _light.gameObject.SetActive(false);
            Animator.Play(CLOSE);
            }
    }
}