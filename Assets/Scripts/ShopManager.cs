using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Helper Feeder;
    public Helper Farm;

    public GameObject ShopPanel;
    public TextMeshProUGUI PassiveIncomeText;
    
    public static int Feeders = 0;
    public static int Farms = 0;
    public static int FeederFrequency; //seconds
    public static int FarmFrequency;
    // public 
    private float _timeToFeedWithFeeder = 0;
    private float _timeToFeedWithFarm = 0;

    private AudioManager _audioManager;

    void Start()
    {
        FeederFrequency = Feeder.FrequencyPerSecond;
        FarmFrequency = Farm.FrequencyPerSecond;
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        HelperAction();
    }
    
    public void AddUpgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "feeder":
                if (Monitor.Horses >= Feeder.Cost)
                {
                    Feeders++;
                    Monitor.Horses -= Feeder.Cost;
                    Feeder.DynamicCost *= (int) Math.Round(1.5, 0);
                    UpdatePassiveIncomeText();
                }
                break;
            case "farm":
                if (Monitor.Horses >= Farm.Cost)
                {
                    Farms++;
                    Monitor.Horses -= Farm.Cost;
                    Farm.DynamicCost *= (int) Math.Round(1.5, 0);
                    UpdatePassiveIncomeText();
                }
                break;
        }
    }

    public void OpenShop()
    {
        _audioManager.Play("DoorBell");
        ShopPanel.SetActive(true);
    }
    
    private void HelperAction()
    {
        if (Feeders > 0)
        {
            _timeToFeedWithFeeder = IncrementWithHelper(_timeToFeedWithFeeder, Feeders, FeederFrequency);
        }
    
        if (Farms > 0)
        {
            _timeToFeedWithFarm = IncrementWithHelper(_timeToFeedWithFarm, Farms * 10, FarmFrequency);
        }
    }
    private float IncrementWithHelper(float timeToFeed, int incrementAmount, int timeTillHelperFeedsAgain)
    {
        if (timeToFeed == 0)
        {
            Monitor.Instance.IncrementHorses(incrementAmount);
            timeToFeed = timeTillHelperFeedsAgain + Time.time;
            return timeToFeed;
        } 
        if (Time.time > timeToFeed)
        {
            Monitor.Instance.IncrementHorses(incrementAmount);
            timeToFeed += timeTillHelperFeedsAgain;
            return timeToFeed;
        }

        return timeToFeed;
    }
    
    public void UpdatePassiveIncomeText()
    {
        if (Feeders <= 0) return;
        var feederRate = (double) Feeders / FeederFrequency;
        var passiveIncomeRate = feederRate;
        if (Farms > 0)
        {
            var farmRate = (double) Farms * 10 / FarmFrequency;
            passiveIncomeRate += farmRate;
        }
        PassiveIncomeText.text = "per second: " + passiveIncomeRate;
    }
}
