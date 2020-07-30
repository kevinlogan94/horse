using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Monitor : MonoBehaviour
{

    public static int TotalHorsesEarned = 0;
    public static int Horses = 0;
    public static int PlayerLevel = 1;
    
    public static int Feeders = 0;
    public static int Farms = 0;
    public static int FeederFrequency; //seconds
    public static int FarmFrequency;
    // public 
    private float _timeToFeedWithFeeder = 0;
    private float _timeToFeedWithFarm = 0;

    // Update is called once per frame
    void Update()
    {
        HelperAction();
    }

    public static void IncrementHorses(int increment = 1)
    {
        Horses += increment;
        TotalHorsesEarned += increment;
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
            IncrementHorses(incrementAmount);
            timeToFeed = timeTillHelperFeedsAgain + Time.time;
            return timeToFeed;
        } 
        if (Time.time > timeToFeed)
        {
            IncrementHorses(incrementAmount);
            timeToFeed += timeTillHelperFeedsAgain;
            return timeToFeed;
        }

        return timeToFeed;
    }
}
