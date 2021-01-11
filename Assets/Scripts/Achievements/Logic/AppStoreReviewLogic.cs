using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppStoreReviewLogic : MonoBehaviour, IAchievement
{
    public Achievement AchievementObject; 
    public TextMeshProUGUI Title;
    public TextMeshProUGUI RewardDescription;
    public Slider ProgressBar;
    public Image Image;
    
    //TODO define android and apple store review url
    private const string AndroidAppStoreReviewUrl = "";
    private const string IosAppStoreReviewUrl = "";
    
    private long _rewardValue;
    
    // Start is called before the first frame update
    void Start()
    {
        
        UpdateTitle();
        Image.sprite = AchievementObject.Artwork;
        RewardDescription.text = AchievementObject.RewardDescription;
        ProgressBar.value = AchievementManager.Instance.AppStoreReviewed ? AchievementManager.ShareGoal : 0;
        ProgressBar.maxValue = AchievementManager.ShareGoal;
    }

    void Update()
    {
        UpdateRewardCounter();
    }

    public void UpdateTitle()
    {
        Title.text = "Write a Review";
    }

    public void Receive()
    {
        if (ProgressBar.value == 0)
        {
            Monitor.Influence += _rewardValue;
            ProgressBar.value = AchievementManager.ShareGoal;
            AchievementManager.Instance.AppStoreReviewed = true;
            if (Application.platform == RuntimePlatform.Android)
            {
                Debug.Log("Android App Store Review Prompt");
                Application.OpenURL(AndroidAppStoreReviewUrl);
            }
            else
            {
                Debug.Log("Apple App Store Review Prompt");
                Application.OpenURL(IosAppStoreReviewUrl);
            }

            AchievementManager.Instance.CurrentAchievementAmount++;
            SplashManager.Instance.TriggerSplash(SplashType.Achievement.ToString(), AchievementObject.Name);
            AchievementManager.Instance.PlayAchievementSound();
        }
    }
    
    public void UpdateRewardCounter()
    {
        _rewardValue = Monitor.Instance.GetInfluenceReceivedOverTime(3600); // 1 hour
        RewardDescription.text = AchievementObject.RewardDescription + "\n(" + Monitor.FormatNumberToString(_rewardValue) + " influence)";
    }
}
