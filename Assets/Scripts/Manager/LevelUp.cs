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
    public long HorsesEarnedEveryLevelSoFar = 0;
    private bool _jinglePlayedThisLevel = false;

    #region Singleton
    public static LevelUp Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    void Start()
    {
    }
    
    public void UpdateRewardCounter()
    {
        var incrementPerSecond = Monitor.Instance.GetHelperPassiveIncome();
        _levelUpReward = incrementPerSecond * 60;
        LevelUpRewardText.text = Monitor.FormatNumberToString(_levelUpReward) + " horses";
    }

    public void LevelUpPlayer(bool watchAd = false)
    {
        //level up reward 
        if (!watchAd)
        {
            Monitor.Instance.IncrementHorses(_levelUpReward, "Thoroughbred");
        }
        else
        {
            var bonusReward = _levelUpReward * 3;
            Monitor.Instance.IncrementHorses(bonusReward, "Appaloosa");
        }
        
        //Update Level up progress bar
        Slider.maxValue = (int) Math.Round(Slider.maxValue * 3);
        Slider.value = 0; 
        HorsesEarnedEveryLevelSoFar = Monitor.TotalHorsesEarned;
            
        // Level Up Character
        Monitor.PlayerLevel++;
        _levelUpReward = 2 * _levelUpReward;
        LevelUpRewardText.text = _levelUpReward + " horses";
        // GameObject.Find("LevelUpText").GetComponent<TextMeshProUGUI>().text = Monitor.PlayerLevel.ToString();
            
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

    private long HorsesEarnedThisLevel()
    { 
        return Monitor.TotalHorsesEarned - HorsesEarnedEveryLevelSoFar;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("LevelUpText").GetComponent<TextMeshProUGUI>().text = Monitor.PlayerLevel.ToString();
        UpdateSliderProgress();
        ReadyToLevelUp();
    }

    private void ReadyToLevelUp()
    {
        if (Slider.value >= Slider.maxValue)
        {
            UpdateRewardCounter();
            gameObject.GetComponent<Button>().interactable = true;
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
        else
        {
            gameObject.GetComponent<Button>().interactable = false; 
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