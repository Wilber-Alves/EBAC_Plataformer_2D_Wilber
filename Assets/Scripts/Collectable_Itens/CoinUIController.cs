using UnityEngine;
using TMPro;

public class CoinUIController : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void Start()
    {
    
        UpdateCoinText();
    }


    public void UpdateCoinText()
    {
        if (coinText != null)
        {
 
            coinText.text = ItemManager.Instance.totalCoins.valueFloat.ToString("F0");
        }
    }
}
