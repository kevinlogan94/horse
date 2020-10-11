using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TwitterLogic : MonoBehaviour, IAchievement
{
    public Achievement AchievementObject; 
    public TextMeshProUGUI Title;
    public TextMeshProUGUI RewardDescription;
    public Slider ProgressBar;
    public Image Image;
    
    //TODO update TwitterUrl
    private const string TwitterUrl = "https://twitter.com";
    
    private long _rewardValue;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateTitle();
        Image.sprite = AchievementObject.Artwork;
        RewardDescription.text = AchievementObject.RewardDescription;
        ProgressBar.value = AchievementManager.Instance.FollowedOnTwitter ? AchievementManager.ShareGoal : 0;
        ProgressBar.maxValue = AchievementManager.ShareGoal;
    }

    void Update()
    {
        UpdateRewardCounter();
    }

    public void UpdateTitle()
    {
        Title.text = "Follow on Twitter";
    }

    public void Receive()
    {
        if (ProgressBar.value == 0)
        {
            Monitor.Influence += _rewardValue;
            ProgressBar.value = AchievementManager.ShareGoal;
            AchievementManager.Instance.FollowedOnTwitter = true;
            Application.OpenURL(TwitterUrl);
            AchievementManager.Instance.AchievementGoal++;
            SplashManager.Instance.TriggerSplash(SplashType.Achievement.ToString(), AchievementObject.Name);
        }
    }
    
    public void UpdateRewardCounter()
    {
        _rewardValue = Monitor.Instance.GetInfluenceReceivedOverTime(3600); // 1 hour
        RewardDescription.text = AchievementObject.RewardDescription + "\n(" + Monitor.FormatNumberToString(_rewardValue) + " influence)";
    }
}
