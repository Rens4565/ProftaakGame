using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;

    private void Update()
    {
        currencyText.text = "Coins: " + LevelManager.main.currency;
    }
}