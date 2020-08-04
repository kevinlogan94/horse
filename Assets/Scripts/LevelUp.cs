﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public Slider Slider;
    public GameObject LevelUpPanel;
    public TextMeshProUGUI LevelUpRewardText;
    public GameObject FingerPointerLevel;
    private int _levelUpReward = 20;

    void Start()
    {
        LevelUpRewardText.text = _levelUpReward + " horses";
    }

    public void LevelUpPlayer()
    {
            //Update Level up progress bar
            var oldMax = Slider.maxValue;
            Slider.maxValue = (int) Math.Round(Slider.maxValue * 2);
            Slider.minValue = oldMax;
            
            // Level Up Character
            Monitor.Instance.IncrementHorses(_levelUpReward);
            Monitor.PlayerLevel++;
            _levelUpReward = 5 * Monitor.PlayerLevel;
            LevelUpRewardText.text = _levelUpReward + " horses";
            
            //close tutorial
            if (Monitor.PlayerLevel==2)
            {
                Monitor.DestroyObject("FingerPointerLevel");
            }
            
            // Close Panel
            LevelUpPanel.SetActive(false);
    }
    
    public void OpenLevelUpPanel()
    {
        if (Slider.value >= Slider.maxValue)
        {
            FindObjectOfType<AudioManager>().Play("LevelUp");
            LevelUpPanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Monitor.TotalHorsesEarned < Slider.maxValue)
        {
            Slider.value = Monitor.TotalHorsesEarned;
        }
        else
        {
            Slider.value = Slider.maxValue;
        }

        if (Slider.value >= Slider.maxValue && Monitor.PlayerLevel == 1)
        {
            FingerPointerLevel.SetActive(true);
        }
    }
}
