using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    public TextMeshProUGUI currencyText; // Reference to the TextMeshProUGUI component
    public int currentCurrency = 0; // Starting currency amount
    public int currencyToAdd = 100; // Amount of currency to add when entering the door

    private bool hasEntered = false; // Flag to prevent multiple triggers
    private RoomController roomController; // Reference to the RoomController

    void Start()
    {
        if (currencyText != null)
        {
            currencyText.text = currentCurrency.ToString();
        }
        else
        {
            Debug.LogError("currencyText is not assigned.");
        }
      
    }

   

    private void UpdateCurrencyText(int currency)
    {
        currencyText.text = currency.ToString();
        Debug.Log("currency update trigger");
    }

    public void AddCurrency()
    {
        Debug.Log("add currency");
        // This method can be used by other scripts to add currency
        currentCurrency += currencyToAdd;
        UpdateCurrencyText(currentCurrency);
    }
}
