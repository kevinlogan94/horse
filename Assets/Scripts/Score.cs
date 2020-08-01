using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI PassiveIncomeText;

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Monitor.Horses.ToString();
        UpdatePassiveIncomeText();
    }
    
    private void UpdatePassiveIncomeText()
    {
        if (ShopManager.Feeders <= 0) return;
        var feederRate = (double) ShopManager.Feeders / ShopManager.FeederFrequency;
        var passiveIncomeRate = feederRate;
        if (ShopManager.Farms > 0)
        {
            var farmRate = (double) ShopManager.Farms * 10 / ShopManager.FarmFrequency;
            passiveIncomeRate += farmRate;
        }

        PassiveIncomeText.text = "per second: " + passiveIncomeRate;
    }
}
