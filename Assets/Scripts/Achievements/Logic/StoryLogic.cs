using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryLogic : MonoBehaviour
{
   public Achievement AchievementObject; 
    public TextMeshProUGUI Title;
    public TextMeshProUGUI RewardDescription;
    public Slider ProgressBar;
    public Image Image;
    public GameObject StoryExclamationPoint;
    
    private long _rewardValue;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateTitle();
        Image.sprite = AchievementObject.Artwork;
        RewardDescription.text = AchievementObject.RewardDescription;
        
        UpdateProgressValue();
        ProgressBar.maxValue = SceneManager.Instance.Chapters.Length;
        TriggerBarRefresh();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTitle();
        UpdateRewardCounter();
        ManageExclamationPoint();
        UpdateProgressValue();
    }
    
    private void UpdateProgressValue()
    {
        var lastReadChapter = SceneManager.Instance.Chapters.LastOrDefault(x=>x.SceneViewed);
        if (lastReadChapter != null && lastReadChapter.Number == SceneManager.Instance.Chapters.Length)
        {
            ProgressBar.value = lastReadChapter.Number;
        }
        else
        {
            ProgressBar.value = 0;
        }
    }

    private void UpdateTitle()
    {
        Title.text = "Finish the Story";
    }

    public void Receive()
    {
        if (ProgressBar.value >= ProgressBar.maxValue)
        {
            Monitor.Influence += _rewardValue;
            ProgressBar.maxValue *= 2;
            AchievementManager.Instance.TutorialCompleted = true;
            AchievementManager.Instance.StoryGoal = ProgressBar.maxValue;
            TriggerBarRefresh();
            AchievementManager.Instance.CurrentAchievementAmount++;
            SplashManager.Instance.TriggerSplash(SplashType.Achievement.ToString(), AchievementObject.Name);
            AchievementManager.Instance.PlayAchievementSound();
        }
    }
    
    private void ManageExclamationPoint()
    {
        StoryExclamationPoint.SetActive(ProgressBar.value >= ProgressBar.maxValue);
    }
    
    private void TriggerBarRefresh()
    {
        ProgressBar.value = ProgressBar.value--;
        ProgressBar.value = ProgressBar.value++;
    }

    private void UpdateRewardCounter()
    {
        _rewardValue = Monitor.Instance.GetInfluenceReceivedOverTime(36000); // 10 hours
        RewardDescription.text = AchievementObject.RewardDescription + "\n(" + Monitor.FormatNumberToString(_rewardValue) + " influence)";
    }
}
