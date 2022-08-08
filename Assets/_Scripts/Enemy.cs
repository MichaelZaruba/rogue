using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthUI;
    [SerializeField] private Image _healthBar;

    [SerializeField] private float _health = 100;

    [SerializeField] private int _damage;

    [SerializeField] protected Rigidbody2D _rigidbody;

    [SerializeField] protected float _speed;

    private float _startHealth;


    private Game _game;

    public void Initialize(Game game)
    {
        _startHealth = _health;
        _healthUI.text = _health.ToString();
           _game = game;
    }

    public void takeDamage(int damage)
    {
        _health -= damage;
        _healthBar.fillAmount = _health / _startHealth;
        _healthUI.text = _health.ToString();
        if (_health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("zdec");
        if (collision.gameObject.GetComponent<PlayerCharacteristic>())
        {
            Debug.Log("tut");
            collision.gameObject.GetComponent<PlayerCharacteristic>().GetDamage(_damage);
        }
    }

    private void Die()
    {
        _game.ReclaimEnemy(this);
    }
}