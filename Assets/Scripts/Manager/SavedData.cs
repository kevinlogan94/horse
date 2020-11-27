using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SavedData
{
    public DateTime SavedDateTime;
    
    // Monitor
    public long Influence;
    public long TotalInfluenceEarned;
    public int PlayerLevel;
    
    //CanvasBackgroundController
    public CanvasBackground CanvasBackground;
    
    // LevelUp
    public long InfluenceEarnedEveryLevelSoFar;
    public float ExperienceRequiredToReachNextLevel;
    
    // IncrementPanel
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
    public int CurrentVideoAmount;
    public float VideoGoal;
    public int CurrentAchievementAmount;
    public float AchievementGoal;
    public bool AppStoreReviewed;
    public bool FollowedOnTwitter;
    public bool TutorialCompleted;

    // SceneManager
    public List<SavedChapter> Chapters = new List<SavedChapter>();
    
    //ManaBar
    public int ManaLevel;
    
    //BuffManager
    public bool BuffTutorialCompleted;
    public bool BuffedThisLevel;

    public SavedData()
    {
        SavedDateTime = DateTime.UtcNow;
        
        // Monitor
        Influence = Monitor.Influence;
        TotalInfluenceEarned = Monitor.TotalInfluenceEarned;
        PlayerLevel = Monitor.PlayerLevel;
        
        //CanvasBackgroundController
        CanvasBackground = CanvasBackgroundController.Instance.CurrentCanvasBackground;

        // LevelUp
        InfluenceEarnedEveryLevelSoFar = LevelUp.Instance.InfluenceEarnedEveryLevelSoFar;
        ExperienceRequiredToReachNextLevel = LevelUp.Instance.Slider.maxValue;
        
        // IncrementPanel
        ClickCount = IncrementPanel.ClickCount;
        ClickerIncrement = IncrementPanel.ClickerIncrement;
        
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
        CurrentVideoAmount = AchievementManager.Instance.CurrentVideoAmount;
        VideoGoal = AchievementManager.Instance.VideoGoal;
        CurrentAchievementAmount = AchievementManager.Instance.CurrentAchievementAmount;
        AchievementGoal = AchievementManager.Instance.AchievementGoal;
        AppStoreReviewed = AchievementManager.Instance.AppStoreReviewed;
        FollowedOnTwitter = AchievementManager.Instance.FollowedOnTwitter;
        TutorialCompleted = AchievementManager.Instance.TutorialCompleted;
        
        // SceneManager
        foreach (var chapter in SceneManager.Instance.Chapters)
        {
            var savedChapter = new SavedChapter();
            savedChapter.Number = chapter.Number;
            savedChapter.SceneViewed = chapter.SceneViewed;
            Chapters.Add(savedChapter);
        }
        
        //ManaBar
        ManaLevel = ManaBar.Instance.ManaLevel;
        
        //BuffManager
        BuffTutorialCompleted = BuffManager.Instance.BuffTutorialCompleted;
        BuffedThisLevel = BuffManager.Instance.BuffedThisLevel;
    }

    public void DistributeLoadData()
    {
        // Monitor
        Monitor.Influence = Influence;
        Monitor.TotalInfluenceEarned = TotalInfluenceEarned;
        Monitor.PlayerLevel = PlayerLevel;
        Monitor.LastSavedDateTime = SavedDateTime;
        
        // CanvasBackgroundController
        CanvasBackgroundController.Instance.CurrentCanvasBackground = CanvasBackground;
        
        // Level Up
        LevelUp.Instance.InfluenceEarnedEveryLevelSoFar = InfluenceEarnedEveryLevelSoFar;
        LevelUp.Instance.Slider.maxValue = ExperienceRequiredToReachNextLevel;
        
        // Increment Panel
        IncrementPanel.ClickCount = ClickCount;
        IncrementPanel.ClickerIncrement = ClickerIncrement;
        
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
        AchievementManager.Instance.CurrentVideoAmount = CurrentVideoAmount;
        AchievementManager.Instance.VideoGoal = VideoGoal;
        AchievementManager.Instance.CurrentAchievementAmount = CurrentAchievementAmount;
        AchievementManager.Instance.AchievementGoal = AchievementGoal;
        AchievementManager.Instance.AppStoreReviewed = AppStoreReviewed;
        AchievementManager.Instance.FollowedOnTwitter = FollowedOnTwitter;
        AchievementManager.Instance.TutorialCompleted = TutorialCompleted;
        
        // SceneManager
        foreach (var localChapter in SceneManager.Instance.Chapters)
        {
            var loadedMatchedChapter =
                Chapters.FirstOrDefault(loadedChapter => loadedChapter.Number == localChapter.Number);
            if (loadedMatchedChapter != null) localChapter.SceneViewed = loadedMatchedChapter.SceneViewed;
        }
        
        //ManaBar
        ManaBar.Instance.ManaLevel = ManaLevel;
        
        //BuffManager
        BuffManager.Instance.BuffTutorialCompleted = BuffTutorialCompleted;
        BuffManager.Instance.BuffedThisLevel = BuffedThisLevel;
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