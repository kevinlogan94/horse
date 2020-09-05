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

    private TextMeshProUGUI _perSecondCounter;
    private int _rewardValue;

    // Start is called before the first frame update
    void Start()
    {
        Title.text = AchievementObject.Title;
        Image.sprite = AchievementObject.Artwork;
        _perSecondCounter = GameObject.Find("PassiveIncome").GetComponent<TextMeshProUGUI>();
        ProgressBar.value = AchievementManager.Instance.LoginCount;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRewardCounter();
        UpdateTitle();
    }

    public void Receive()
    {
        if (ProgressBar.value >= ProgressBar.maxValue)
        {
            Monitor.Horses += _rewardValue;
            ProgressBar.maxValue++;
            //trigger bar change
            ProgressBar.value = ProgressBar.value--;
            ProgressBar.value = ProgressBar.value++;
            SplashManager.Instance.TriggerSplash(SplashType.Achievement.ToString(), AchievementObject.Name);
        }
    }

    public void UpdateTitle()
    {
            Title.text = "Log in for " + ProgressBar.maxValue + " days";
    }

    public void UpdateRewardCounter()
    {
        _rewardValue = Monitor.Instance.GetHorseReceivedOverTime(3600); // 1 hour
        RewardDescription.text = AchievementObject.RewardDescription + "\n(" + Monitor.FormatNumberToString(_rewardValue) + " horses)";
    }
}