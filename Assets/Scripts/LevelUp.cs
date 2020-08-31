using System;
using System.Text.RegularExpressions;
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
    private int _horsesEarnedEveryLevelSoFar = 0;
    private bool _jinglePlayedThisLevel = false;

    private TextMeshProUGUI _perSecondCounter;

    void Start()
    {
        _perSecondCounter = GameObject.Find("PassiveIncome").GetComponent<TextMeshProUGUI>();
    }
    
    public void UpdateRewardCounter()
    {
        var incrementPerSecond = int.Parse(Regex.Replace(_perSecondCounter.text, "[^0-9]", ""));
        _levelUpReward = incrementPerSecond * 60;
        LevelUpRewardText.text = Monitor.FormatNumberToString(_levelUpReward) + " horses";
    }

    public void LevelUpPlayer(bool watchAd = false)
    {
        //level up reward 
        if (!watchAd)
        {
            Monitor.Instance.IncrementHorses(_levelUpReward, "Horse");
        }
        else
        {
            var bonusReward = _levelUpReward * 3;
            Monitor.Instance.IncrementHorses(bonusReward, "Appaloosa");
        }
        
        //Update Level up progress bar
        Slider.maxValue = (int) Math.Round(Slider.maxValue * 3);
        Slider.value = 0; 
        _horsesEarnedEveryLevelSoFar = Monitor.TotalHorsesEarned;
            
        // Level Up Character
        Monitor.PlayerLevel++;
        _levelUpReward = 2 * _levelUpReward;
        LevelUpRewardText.text = _levelUpReward + " horses";
        GameObject.Find("LevelUpText").GetComponent<TextMeshProUGUI>().text = Monitor.PlayerLevel.ToString();
            
        //close tutorial
        if (Monitor.PlayerLevel==2)
        {
            Monitor.DestroyObject("FingerPointerLevel");
        }
        
        //reset jingle
        _jinglePlayedThisLevel = false;
            
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

    private int HorsesEarnedThisLevel()
    {
        return Monitor.TotalHorsesEarned - _horsesEarnedEveryLevelSoFar;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSliderProgress();
        if (Slider.value >= Slider.maxValue)
        {
            UpdateRewardCounter();
            if (Monitor.PlayerLevel == 1)
            {
                FingerPointerLevel.SetActive(true);
            }
            else if (!_jinglePlayedThisLevel)
            {
                FindObjectOfType<AudioManager>().Play("LevelUp2");
                _jinglePlayedThisLevel = true;
            }
        }
    }

    private void UpdateSliderProgress()
    {
        if (HorsesEarnedThisLevel() < Slider.maxValue)
        {
            Slider.value = HorsesEarnedThisLevel();
        }
        else
        {
            Slider.value = Slider.maxValue;
        }
    }
}