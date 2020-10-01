using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    public GameObject NewsPanel;
    public TextMeshProUGUI NewsText;
    public Log[] Logs;
    
    #region Singleton
    public static NewsManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TutorialNews();
        StoryNews();
        LevelNews();
        ScoreNews();
        AchievementNews();
    }

    private void StoryNews()
    {
        
    }

    private void TutorialNews()
    {
        if (ShopManager.Instance.Helpers[1].AmountOwned == 1)
        {
            PlayNews("Tomes");
        }
        if (BottomNavManager.Instance.ActiveView == "outlook")
        {
            PlayNews("Outlook");
        }
        if (Monitor.Influence >= 2)
        {
            PlayNews("Spells");
        }
        // When you can revisit Xal
        if (Monitor.PlayerLevel >= SceneManager.Instance.Chapters.Where(x => x.Number == 2).Select(x => x.LevelRequirement).FirstOrDefault())
        {
            PlayNews("Xal");
        }
    }

    private void AchievementNews()
    {
        if (AchievementManager.Instance.CurrentClickedAmount >= AchievementManager.Instance.ClickerGoal)
        {
            PlayNews("ClickerAchievement");
        }
        if (AchievementManager.Instance.LoginCount >= AchievementManager.Instance.LoginGoal)
        {
            PlayNews("LoginAchievement");
        }
        if (AchievementManager.Instance.CurrentHelperAmount >= AchievementManager.Instance.HelperGoal)
        {
            PlayNews("TomeAchievement");
        }
    }

    private void LevelNews()
    {
        switch (Monitor.PlayerLevel)
        {
            case 2:
                PlayNews("Level2");
                break;
            case 4:
                PlayNews("Noticed");
                break;
            case 7: 
                PlayNews("Origin");
                break;
            case 10:
                PlayNews("PlayTest");
                break;
        }
    }

    private void ScoreNews()
    {
        if (Monitor.TotalInfluenceEarned >= 1000 && Monitor.TotalInfluenceEarned < 10000)
        {
            PlayNews("1,000");
        }
        else if (Monitor.TotalInfluenceEarned >= 10000 && Monitor.TotalInfluenceEarned < 50000)
        {
            PlayNews("10,000");
        } else if (Monitor.TotalInfluenceEarned >= 50000 && Monitor.TotalInfluenceEarned < 100000)
        {
            PlayNews("50,000");
        } else if (Monitor.TotalInfluenceEarned >= 100000 && Monitor.TotalInfluenceEarned < 500000)
        {
            PlayNews("100,000");
        } else if (Monitor.TotalInfluenceEarned >= 500000 && Monitor.TotalInfluenceEarned < 1000000)
        {
            PlayNews("500,000");
        } else if (Monitor.TotalInfluenceEarned >= 1000000)
        {
            PlayNews("1,000,000");
        } 
    }

    private void PlayNews(string logName)
    {
        var log = Logs.FirstOrDefault(x => x.Name == logName);
        if (log == null)
        {
            Debug.LogWarning("We couldn't find the log: " + logName);
            return;
        }
        if (log.Displayed)
            return;
        if (NewsPanel.activeSelf)
            return;
        if (BottomNavManager.Instance.ActiveView != "outlook")
            return;
        
        NewsPanel.gameObject.SetActive(true);
        NewsText.GetComponent<TextMeshProUGUI>().text = log.Message;
        log.Displayed = true;
    }
}