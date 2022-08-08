using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerCharacteristic : MonoBehaviour
{
    private Game _game;
    private Image _staminaImage;
    private Image _healthImage;
    private float _maxStamina;
    private float _maxHp;
    private Coroutine _coroutine;

    [Range(10, 500)] public float Health;

    [Range(10, 100)] public int Damage;
    public float RangeAttack;

    public float Stamina;
    public float Speed;

    public bool StaminaActive;

    public void Awake()
    {
        _maxStamina = Stamina;
        _maxHp = Health;
    }
    public void Initialize(Image stamina, Image health)
    {
        _healthImage = health;
        _staminaImage = stamina;
        _healthImage.fillAmount = Health / _maxHp;
        _staminaImage.fillAmount = Stamina / _maxStamina;
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
            Stamina += 0.15f;
        }
        Stamina = _maxStamina;

    }
    #endregion

    public void GetDamage(float damage)
    {
        Health -= damage;
        _healthImage.fillAmount = Health / _maxHp; 
    }

    private void Die()
    {
        _game.ReclaimPlayer(this);
    }
}
