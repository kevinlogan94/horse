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
        if (Monitor.Feeders <= 0) return;
        var feederRate = (double) Monitor.Feeders / Monitor.FeederFrequency;
        var passiveIncomeRate = feederRate;
        Debug.Log(passiveIncomeRate);
        if (Monitor.Farms > 0)
        {
            var farmRate = (double) Monitor.Farms * 10 / Monitor.FarmFrequency;
            passiveIncomeRate += farmRate;
        }

        PassiveIncomeText.text = "per second: " + passiveIncomeRate;
    }
}
