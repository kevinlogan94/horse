using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Helper Feeder;
    public Helper Farm;

    void Start()
    {
        Monitor.FeederFrequency = Feeder.FrequencyPerSecond;
        Monitor.FarmFrequency = Farm.FrequencyPerSecond;
    }
    
    public void AddUpgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "feeder":
                if (Monitor.Horses >= Feeder.Cost)
                {
                    Monitor.Feeders++;
                    Monitor.Horses -= Feeder.Cost;
                    Feeder.Cost *= (int) Math.Round(1.5, 0); 
                }
                break;
            case "farm":
                if (Monitor.Horses >= Farm.Cost)
                {
                    Monitor.Farms++;
                    Monitor.Horses -= Farm.Cost;
                    Farm.Cost *= (int) Math.Round(1.5, 0); 
                }
                break;
        }
    }
}
