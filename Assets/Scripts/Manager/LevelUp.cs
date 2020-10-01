using System;
using System.Linq;
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
    public GameObject LevelExclamationPoint;
    public GameObject LevelUpTutorialPanel;
    private int _levelUpReward = 20;
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
    
    public void UpdateRewardCounter()
    {
        var incrementPerSecond = Monitor.Instance.GetHelperPassiveIncome();
        _levelUpReward = incrementPerSecond * 60;
        LevelUpRewardText.text = Monitor.FormatNumberToString(_levelUpReward) + " influence";
    }

    public void LevelUpPlayer(bool watchAd = false)
    {
        //level up reward 
        var helperHorse = ShopManager.Instance.Helpers.LastOrDefault(helper => helper.AmountOwned > 0)?.HorseBreed;
        if (!watchAd)
        {
            Monitor.Instance.IncrementInfluence(_levelUpReward, helperHorse);
        }
        else
        {
            var bonusReward = _levelUpReward * 3;
            Monitor.Instance.IncrementInfluence(bonusReward, helperHorse);
        }
        
        //Update Level up progress bar
        Slider.maxValue = (int) Math.Round(Slider.maxValue * 3);
        Slider.value = 0; 
        InfluenceEarnedEveryLevelSoFar = Monitor.TotalInfluenceEarned;
            
        // Level Up Character
        Monitor.PlayerLevel++;
        _levelUpReward = 2 * _levelUpReward;
        LevelUpRewardText.text = _levelUpReward + " influence";
        // GameObject.Find("LevelUpText").GetComponent<TextMeshProUGUI>().text = Monitor.PlayerLevel.ToString();
            
        //close tutorial
        if (Monitor.PlayerLevel==2)
        {
            Monitor.DestroyObject("FingerPointerLevel");
            LevelUpTutorialPanel.SetActive(false);
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

    private long InfluenceEarnedThisLevel()
    { 
        return Monitor.TotalInfluenceEarned - InfluenceEarnedEveryLevelSoFar;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("LevelUpText").GetComponent<TextMeshProUGUI>().text = "Lvl " + Monitor.PlayerLevel;
        UpdateSliderProgress();
        ReadyToLevelUp();
    }

    private void ReadyToLevelUp()
    {
        if (Slider.value >= Slider.maxValue)
        {
            LevelExclamationPoint.SetActive(true);
            LevelUpTutorialPanel.SetActive(true);
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
            LevelExclamationPoint.SetActive(false);
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
}