using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Gens _gen;
    [SerializeField] private TextMeshProUGUI _healthUI;
    [SerializeField] private Image _healthBar;

    [SerializeField] private float _health = 100;
    [SerializeField] private int _amountGens;

    [SerializeField] protected float _speed;
    [SerializeField] protected int _damage;

    [SerializeField] protected Rigidbody2D _rigidbody;
   
    private Game _game;

    private List<Gens> _gens = new List<Gens>();

    private float _startHealth; 

    public void Initialize(Game game)
    {
        _startHealth = _health;
        _healthUI.text = _health.ToString();
        _game = game;
    }

    public void GetDamage(int damage)
    {
        _health -= damage;
        _healthBar.fillAmount = _health / _startHealth;
        _healthUI.text = _health.ToString();
        if (_health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
           // collision.gameObject.GetComponent<Player>().GetDamage(_damage);
        }
    }

    private void Die()
    {
        int countGens = Random.Range(3, 10);
        for (int i = 0; i < countGens; i++)
        {
            Gens gen = Instantiate(_gen);
            _gens.Add(gen);
            gen.transform.position = new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f), transform.position.z);
        }
        _game.ReclaimEnemy(this);

    }
}