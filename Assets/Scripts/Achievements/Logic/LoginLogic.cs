using System;
using System.Text.RegularExpressions;
using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginLogic : MonoBehaviour, IAchievement
{
    public Achievement AchievementObject; 
    public TextMeshProUGUI Title;
    public TextMeshProUGUI RewardDescription;
    public Slider ProgressBar;
    public Image Image;
    public GameObject LoginExclamationPoint;

    private TextMeshProUGUI _perSecondCounter;
    private long _rewardValue;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTitle();
        Image.sprite = AchievementObject.Artwork;
        _perSecondCounter = GameObject.Find("PassiveIncome").GetComponent<TextMeshProUGUI>();
        ProgressBar.value = AchievementManager.Instance.LoginCount;
        ProgressBar.maxValue = AchievementManager.Instance.LoginGoal;
    }

    // Update is called once per frame
    void Update()
    {
        ManageExclamationPoint();
        UpdateRewardCounter();
        UpdateTitle();
    }

    public void Receive()
    {
        if (ProgressBar.value >= ProgressBar.maxValue)
        {
            Monitor.Influence += _rewardValue;
            ProgressBar.maxValue++;
            AchievementManager.Instance.TutorialCompleted = true;
            AchievementManager.Instance.LoginGoal = ProgressBar.maxValue;
            //trigger bar change
            ProgressBar.value = ProgressBar.value--;
            ProgressBar.value = ProgressBar.value++;
            SplashManager.Instance.TriggerSplash(SplashType.Achievement.ToString(), AchievementObject.Name);
        }
    }

    public void ManageExclamationPoint()
    {
        if (ProgressBar.value >= ProgressBar.maxValue)
        {
            LoginExclamationPoint.SetActive(true);
        }
        else
        {
            LoginExclamationPoint.SetActive(false);
        }
    }

    public void UpdateTitle()
    {
        Title.text = "Log in for " + ProgressBar.maxValue + " days";
    }

    public void UpdateRewardCounter()
    {
        _rewardValue = Monitor.Instance.GetInfluenceReceivedOverTime(3600); // 1 hour
        RewardDescription.text = AchievementObject.RewardDescription + "\n(" + Monitor.FormatNumberToString(_rewardValue) + " influence)";
    }
}