using EDGEE.Core.Singleton;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{

    public float totalCoins;
    private CoinUIController _uiController;

    void Start()
    {
        Reset();
        _uiController = Object.FindAnyObjectByType<CoinUIController>();
    }
    private void Reset()
    {
        totalCoins = 0;
        if (_uiController != null) _uiController.UpdateCoinText();
    }
    public void AddCoins(float amount = 0.5f)
    {
        totalCoins += amount;
        if (_uiController != null)
        {
            _uiController.UpdateCoinText();
        }
    }
}
