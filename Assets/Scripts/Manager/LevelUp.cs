using System;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public Slider Slider;
    public GameObject LevelUpPanel;
    public TextMeshProUGUI LevelUpRewardText;
    public GameObject FingerPointerLevel;
    public GameObject LevelExclamationPoint;
    public GameObject LevelUpTutorialPanel;
    public GameObject BottomNavHidePanel;
    private long _levelUpReward = 20;
    public long InfluenceEarnedEveryLevelSoFar = 0;
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
    
    // Update is called once per frame
    void Update()
    {
        GameObject.Find("LevelUpText").GetComponent<TextMeshProUGUI>().text = "Lvl " + Monitor.PlayerLevel;
        UpdateSliderProgress();
        ReadyToLevelUp();
    }

    public void LevelUpPlayer(bool watchAd = false)
    {
        //level up reward 
        if (!watchAd)
        {
            Monitor.Instance.IncrementInfluence(_levelUpReward);
        }
        else
        {
            var bonusReward = _levelUpReward * 3;
            AdvertisementManager.Instance.ShowRewardedAd(bonusReward);
        }
        
        //Update Level up progress bar
        Slider.maxValue = (int) Math.Round(Slider.maxValue * 3);
        Slider.value = 0; 
        InfluenceEarnedEveryLevelSoFar = Monitor.TotalInfluenceEarned;
            
        // Level Up Character
        Monitor.PlayerLevel++;
        if (Monitor.UseAnalytics)
        {
            AnalyticsEvent.LevelStart(Monitor.PlayerLevel);
        }
        // _levelUpReward = 2 * _levelUpReward;
        // LevelUpRewardText.text = _levelUpReward + " influence";
        // GameObject.Find("LevelUpText").GetComponent<TextMeshProUGUI>().text = Monitor.PlayerLevel.ToString();
            
        //close tutorial
        if (Monitor.PlayerLevel==2)
        {
            Monitor.DestroyObject("FingerPointerLevel");
            LevelUpTutorialPanel.SetActive(false);
            BottomNavHidePanel.SetActive(false);
        }
        
        //reset jingle
        _jinglePlayedThisLevel = false;
            
        // Close Panel
        LevelUpPanel.SetActive(false);
    }

    private long InfluenceEarnedThisLevel()
    { 
        return Monitor.TotalInfluenceEarned - InfluenceEarnedEveryLevelSoFar;
    }

    private void ReadyToLevelUp()
    {
        if (Slider.value >= Slider.maxValue)
        {
            if (SceneManager.Instance.ActiveChapter == 0)
            {
                LevelExclamationPoint.SetActive(true);
            }
            UpdateRewardCounter();
            gameObject.GetComponent<Button>().interactable = true;
            if (Monitor.PlayerLevel == 1 && BottomNavManager.Instance.ActiveView == Views.outlook.ToString())
            {
                BottomNavHidePanel.SetActive(true);
                LevelUpTutorialPanel.SetActive(true);
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
            LevelExclamationPoint.SetActive(false);
        }
    }

    #region UI Methods
    
    private void UpdateRewardCounter()
    {
        _levelUpReward = Monitor.Instance.GetInfluenceReceivedOverTime(600); // 10 minutes
        LevelUpRewardText.text = Monitor.FormatNumberToString(_levelUpReward) + " influence";
    }
    
    public void OpenLevelUpPanel()
    {
        if (Slider.value >= Slider.maxValue)
        {
            FindObjectOfType<AudioManager>().Play("LevelUp");
            LevelUpPanel.SetActive(true);
            if (Monitor.UseAnalytics)
            {
                AnalyticsEvent.AdOffer(true);
            }
        }
    }

    private void UpdateSliderProgress()
    {
        if (InfluenceEarnedThisLevel() < Slider.maxValue)
        {
            Slider.value = InfluenceEarnedThisLevel();
        }
        else
        {
            Slider.value = Slider.maxValue;
        }
    }
    
    #endregion
}