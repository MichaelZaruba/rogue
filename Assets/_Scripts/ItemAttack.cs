using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemAttack : MonoBehaviour
{
    
    [SerializeField] private GameObject[] _offWhenActivate;
    [SerializeField] private RawImage _imageInfo;
    [SerializeField] private TextMeshProUGUI _textPrice;

    private PlayerAttack _playerAttack;

    public int countPurpleGens;
    public AttackType Type;

    public void Initialize(PlayerAttack playerAttack)
    {
        _playerAttack = playerAttack;
        _textPrice.text = countPurpleGens.ToString();
    }

    public void BuyAttack()
    {
       /* if (countPurpleGens <= GUIManager._instance.Gens)*/
        for(int i = 0; i < _offWhenActivate.Length; i++)
        {
            _offWhenActivate[i].SetActive(false);
        }
        ActivateAttack(Type);
        ShowInfo();
    }

    public void ShowInfo()
    {
        _imageInfo.gameObject.SetActive(true);
    }

    private void ActivateAttack(AttackType type)
    {
       switch (type)
        {
            case AttackType.Default:
               _playerAttack.IsAttackActivate = true;
                break;
            case AttackType.Down:
                _playerAttack.IsThroughDownActivate = true;
                break;
            case AttackType.Through:
                _playerAttack.IsThroughAttackActivate = true;
                break;
        }
    }
}