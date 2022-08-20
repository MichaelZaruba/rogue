using TMPro;
using UnityEngine;
using UnityEngine.UI;

public  class ItemTalant : MonoBehaviour
{
    private DisplayTalant _displayTalant;
    private TextMeshProUGUI[] _priceUI;
    private Game _game;
    private Talants _talants;

    public TypeTalant Type;
    public int PriceGens;
    private string TalantName; 

    public void Initialize(Game game, Talants talants, DisplayTalant displayTalant)
    {
        _displayTalant = displayTalant;
        _talants = talants;
        _game = game;
        _priceUI = GetComponentsInChildren<TextMeshProUGUI>();
        _priceUI[0].text = PriceGens.ToString();
        _priceUI[1].text = Type.ToString();

        TalantName = Type.ToString();

        if (PlayerPrefs.GetInt(TalantName) != 0)
            TalantActivate();
    }   

    public void TalantActivate()
    {
        _displayTalant.Initialize(Type, gameObject.GetComponentInChildren<Image>());
        _talants.ActivateTalant(Type);
        _priceUI[1].text = Type.ToString() + " ACTIVE";
    }

    public void TalantsBuy()
    {
        PlayerPrefs.SetInt(TalantName, 1);
        TalantActivate();
    }
}