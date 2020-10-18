using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfluenceOverTimePanelScript : MonoBehaviour
{
    public TextMeshProUGUI RewardText;

    private long _rewardValue;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRewardCounter();
    }

    public void CollectAndCloseSplash()
    {
        Monitor.Instance.IncrementInfluence(_rewardValue);
        SplashManager.Instance.CloseSplash();
    }
    
    private void UpdateRewardCounter()
    {
        if (Monitor.LastSavedDateTime == null) return;

        var now = DateTime.UtcNow;
        var timeSinceLastSave = (long) Math.Round(now.Subtract((DateTime) Monitor.LastSavedDateTime).TotalSeconds);
        _rewardValue = Monitor.Instance.GetInfluenceReceivedOverTime(timeSinceLastSave);
        RewardText.text = Monitor.FormatNumberToString(_rewardValue) + " influence";
    }
}
