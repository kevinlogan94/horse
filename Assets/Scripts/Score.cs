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
    }
}
