using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SavedData
{
    public DateTime SavedDateTime = DateTime.Now;
    
    // Monitor
    public long Influence;
    public long TotalInfluenceEarned;
    public int PlayerLevel;
    
    // LevelUp
    public long InfluenceEarnedEveryLevelSoFar;
    public float ExperienceRequiredToReachNextLevel;
    
    // IncrementButton
    public long ClickCount;
    public long ClickerIncrement;
    
    // ShopManager
    public List<SavedHelper> Helpers = new List<SavedHelper>();
    
    // NewsManager
    public List<SavedLog> Logs = new List<SavedLog>();
    
    // AchievementManager
    public int LoginCount;
    public float LoginGoal;
    public long CurrentClickedAmount;
    public float ClickerGoal;
    public int CurrentHelperAmount;
    public float HelperGoal;
    public bool TutorialCompleted;
    public int CurrentVideoAmount;
    public float VideoGoal;
    public bool AppStoreReviewed;
    public bool FollowedOnTwitter;

    // SceneManager
    public List<SavedChapter> Chapters = new List<SavedChapter>();

    public SavedData()
    {
        // Monitor
        Influence = Monitor.Influence;
        TotalInfluenceEarned = Monitor.TotalInfluenceEarned;
        PlayerLevel = Monitor.PlayerLevel;

        // LevelUp
        InfluenceEarnedEveryLevelSoFar = LevelUp.Instance.InfluenceEarnedEveryLevelSoFar;
        ExperienceRequiredToReachNextLevel = LevelUp.Instance.Slider.maxValue;
        
        // IncrementButton
        ClickCount = IncrementButton.ClickCount;
        ClickerIncrement = IncrementButton.ClickerIncrement;
        
        // ShopManager
        foreach (var helper in ShopManager.Instance.Helpers)
        {
            var savedHelper = new SavedHelper();
            savedHelper.Name = helper.Name;
            savedHelper.AmountOwned = helper.AmountOwned;
            savedHelper.DynamicCost = helper.DynamicCost;
            savedHelper.DynamicIncrement = helper.DynamicIncrement;
            Helpers.Add(savedHelper);
        }
        
        // NewsManager
        foreach (var log in NewsManager.Instance.Logs)
        {
            var savedLog = new SavedLog();
            savedLog.Name = log.Name;
            savedLog.Displayed = log.Displayed;
            Logs.Add(savedLog);
        }
        
        // AchievementManager
        LoginCount = AchievementManager.Instance.LoginCount;
        LoginGoal = AchievementManager.Instance.LoginGoal;
        CurrentClickedAmount = AchievementManager.Instance.CurrentClickedAmount;
        ClickerGoal = AchievementManager.Instance.ClickerGoal;
        CurrentHelperAmount = AchievementManager.Instance.CurrentHelperAmount;
        HelperGoal = AchievementManager.Instance.HelperGoal;
        TutorialCompleted = AchievementManager.Instance.TutorialCompleted;
        CurrentVideoAmount = AchievementManager.Instance.CurrentVideoAmount;
        VideoGoal = AchievementManager.Instance.VideoGoal;
        AppStoreReviewed = AchievementManager.Instance.AppStoreReviewed;
        FollowedOnTwitter = AchievementManager.Instance.FollowedOnTwitter;
        
        // SceneManager
        foreach (var chapter in SceneManager.Instance.Chapters)
        {
            var savedChapter = new SavedChapter();
            savedChapter.Number = chapter.Number;
            savedChapter.SceneViewed = chapter.SceneViewed;
            Chapters.Add(savedChapter);
        }
    }

    public void DistributeLoadData()
    {
        // Monitor
        Monitor.Influence = Influence;
        Monitor.TotalInfluenceEarned = TotalInfluenceEarned;
        Monitor.PlayerLevel = PlayerLevel;
        
        // Level Up
        LevelUp.Instance.InfluenceEarnedEveryLevelSoFar = InfluenceEarnedEveryLevelSoFar;
        LevelUp.Instance.Slider.maxValue = ExperienceRequiredToReachNextLevel;
        
        // Increment Button
        IncrementButton.ClickCount = ClickCount;
        IncrementButton.ClickerIncrement = ClickerIncrement;
        
        // Shop Manager
        foreach (var localHelper in ShopManager.Instance.Helpers)
        {
            // Debug.Log(Helpers);
            var loadedMatchedHelper = Helpers.FirstOrDefault(loadedHelper => loadedHelper.Name == localHelper.Name);
            if (loadedMatchedHelper != null)
            {
                localHelper.DynamicCost = loadedMatchedHelper.DynamicCost;
                localHelper.AmountOwned = loadedMatchedHelper.AmountOwned;
                localHelper.DynamicIncrement = loadedMatchedHelper.DynamicIncrement;
            }
        }
        
        // NewsManager
        foreach (var localLog in NewsManager.Instance.Logs)
        {
            var loadedMatchedLog = Logs.FirstOrDefault(loadedLog => loadedLog.Name == localLog.Name);
            if (loadedMatchedLog != null) localLog.Displayed = loadedMatchedLog.Displayed;
        }
        
        // Achievement Manager
        AchievementManager.Instance.LoginCount = LoginCount;
        AchievementManager.Instance.LoginGoal = LoginGoal;
        AchievementManager.Instance.CurrentClickedAmount = CurrentClickedAmount;
        AchievementManager.Instance.ClickerGoal = ClickerGoal;
        AchievementManager.Instance.CurrentHelperAmount = CurrentHelperAmount;
        AchievementManager.Instance.HelperGoal = HelperGoal;
        AchievementManager.Instance.TutorialCompleted = TutorialCompleted;
        AchievementManager.Instance.CurrentVideoAmount = CurrentVideoAmount;
        AchievementManager.Instance.VideoGoal = VideoGoal;
        AchievementManager.Instance.AppStoreReviewed = AppStoreReviewed;
        AchievementManager.Instance.FollowedOnTwitter = FollowedOnTwitter;
        
        // SceneManager
        foreach (var localChapter in SceneManager.Instance.Chapters)
        {
            var loadedMatchedChapter =
                Chapters.FirstOrDefault(loadedChapter => loadedChapter.Number == localChapter.Number);
            if (loadedMatchedChapter != null) localChapter.SceneViewed = loadedMatchedChapter.SceneViewed;
        }
    }

    public static void RefreshData()
    {
        foreach (var helper in ShopManager.Instance.Helpers)
        {
            helper.AmountOwned = 0;
            helper.DynamicCost = helper.Cost;
            helper.DynamicIncrement = helper.Increment;
        }
        foreach (var log in NewsManager.Instance.Logs)
        {
            log.Displayed = false;
        }
        foreach (var chapter in SceneManager.Instance.Chapters)
        {
            chapter.SceneViewed = false;
        }
    }
}