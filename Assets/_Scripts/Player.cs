using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttackRange))]
public class Player : MonoBehaviour
{
    private PlayerAttack _playerAttack;
    private AttackInventory _attackInventory;
    private Game _game;

    private Image _staminaImage;
    private Image _healthImage;

    private float _maxStamina;
    private float _maxHp;
    private Coroutine _coroutine;
    
    [Range(10, 500)] public float Health;

    [Range(10, 100)] public int Damage;

    public const string STAMINA = "Stamina";
    public const string HEALTH = "Health";
    public const string DAMAGE = "Damage";

    public float DamageRatio = 1f;
    public float RangeAttack;

    public float Stamina;
    public float Speed;

    public float _speedFillingStamina = 0.25f;

    public bool StaminaActive;

    public void Awake()
    {
        _maxStamina = Stamina;
        _maxHp = Health;
    }

    public void Initialize(Image stamina, Image health, Game game, AttackInventory attackInventory)
    {
        int safeStamina = PlayerPrefs.GetInt(STAMINA);
        int safeHealth = PlayerPrefs.GetInt(HEALTH);
        int safeDamage = PlayerPrefs.GetInt(DAMAGE);

        if (safeStamina != 0)
            Stamina = safeStamina;
        if (safeHealth != 0)
            Health = safeHealth;
        if (safeDamage != 0)
            Damage = safeDamage;

        _attackInventory = attackInventory;
        _healthImage = health;
        _staminaImage = stamina;
        _healthImage.fillAmount = Health / _maxHp;
        _staminaImage.fillAmount = Stamina / _maxStamina;
        _game = game;
        _playerAttack = GetComponent<PlayerAttack>();
        _playerAttack.Initialize(_attackInventory);
    }


    #region STAMINA
    public void MinusStamina(float minus, bool activeStamina)
    {
        Stamina -= minus;
        if (Stamina < 0)
        {
            Stamina = 0;
        }
        if (activeStamina)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
        if (Stamina < _maxStamina && !activeStamina)
        {
            StartFillingStamina();
        }
    }


    private void Update()
    {
        _staminaImage.fillAmount = Stamina / _maxStamina;

        if (Health <= 0)
        {
            Die();
        }
    }

    public void StartFillingStamina()
    { 
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(StaminaFilling());    
    }

    private IEnumerator StaminaFilling()
    {
        yield return new WaitForSeconds(0.1f);
        while (Stamina < _maxStamina)
        {
            yield return new WaitForSeconds(0.05f);
            Stamina += _speedFillingStamina;
        }
        Stamina = _maxStamina;
    }
    #endregion

    public void GetDamage(float damage)
    {
        Health -= damage;
        _healthImage.fillAmount = Health / _maxHp; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Gens>())
        {
            GUIManager._instance.Gens += 1;
            Destroy(collision.gameObject);
        }
    }

    private void Die()
    {
        _game.RestartGameAfterDiePlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NewAttack>())
        {
            var newAttack = collision.GetComponent<NewAttack>();
            newAttack.Initialize(this, _attackInventory);
            newAttack.InRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<NewAttack>())
        {
            var newAttack = collision.GetComponent<NewAttack>();
            newAttack.Initialize(this, _attackInventory);
             newAttack.InRange = false;
        }
    }
}
