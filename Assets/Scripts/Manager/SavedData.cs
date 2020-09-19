using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SavedData
{
    // Monitor
    public long Horses;
    public long TotalHorsesEarned;
    public int PlayerLevel;
    
    // LevelUp
    public long HorsesEarnedEveryLevelSoFar;
    public float ExperienceRequiredToReachNextLevel;
    
    // IncrementButton
    public long ClickCount;
    public int ClickerLevel;
    
    // ShopManager
    public List<SavedHelper> Helpers = new List<SavedHelper>();
    
    // AchievementManager
    public int LoginCount;
    public float LoginGoal;
    public long CurrentClickedAmount;
    public float ClickerGoal;
    public int CurrentHelperAmount;
    public float HelperGoal;
    public bool TutorialCompleted;

    public SavedData()
    {
        // Monitor
        Horses = Monitor.Horses;
        TotalHorsesEarned = Monitor.TotalHorsesEarned;
        PlayerLevel = Monitor.PlayerLevel;

        // LevelUp
        HorsesEarnedEveryLevelSoFar = LevelUp.Instance.HorsesEarnedEveryLevelSoFar;
        ExperienceRequiredToReachNextLevel = LevelUp.Instance.Slider.maxValue;
        
        // IncrementButton
        ClickCount = IncrementButton.ClickCount;
        ClickerLevel = IncrementButton.ClickerLevel;
        
        // ShopManager
        foreach (var helper in ShopManager.Instance.Helpers)
        {
            var savedHelper = new SavedHelper();
            savedHelper.Name = helper.Name;
            savedHelper.AmountOwned = helper.AmountOwned;
            savedHelper.DynamicCost = helper.DynamicCost;
            Helpers.Add(savedHelper);
        }
        
        // AchievementManager
        LoginCount = AchievementManager.Instance.LoginCount;
        LoginGoal = AchievementManager.Instance.LoginGoal;
        CurrentClickedAmount = AchievementManager.Instance.CurrentClickedAmount;
        ClickerGoal = AchievementManager.Instance.ClickerGoal;
        CurrentHelperAmount = AchievementManager.Instance.CurrentHelperAmount;
        HelperGoal = AchievementManager.Instance.HelperGoal;
        TutorialCompleted = AchievementManager.Instance.TutorialCompleted;
    }

    public void DistributeLoadData()
    {
        // Monitor
        Monitor.Horses = Horses;
        Monitor.TotalHorsesEarned = TotalHorsesEarned;
        Monitor.PlayerLevel = PlayerLevel;
        
        // Level Up
        LevelUp.Instance.HorsesEarnedEveryLevelSoFar = HorsesEarnedEveryLevelSoFar;
        LevelUp.Instance.Slider.maxValue = ExperienceRequiredToReachNextLevel;
        
        // Increment Button
        IncrementButton.ClickCount = ClickCount;
        IncrementButton.ClickerLevel = ClickerLevel;
        
        // Shop Manager
        foreach (var localHelper in ShopManager.Instance.Helpers)
        {
            // Debug.Log(Helpers);
            foreach (var loadedHelper in Helpers.Where(loadedHelper => loadedHelper.Name == localHelper.Name))
            {
                localHelper.DynamicCost = loadedHelper.DynamicCost;
                localHelper.AmountOwned = loadedHelper.AmountOwned;
            }
        }
        
        // Achievement Manager
        AchievementManager.Instance.LoginCount = LoginCount;
        AchievementManager.Instance.LoginGoal = LoginGoal;
        AchievementManager.Instance.CurrentClickedAmount = CurrentClickedAmount;
        AchievementManager.Instance.ClickerGoal = ClickerGoal;
        AchievementManager.Instance.CurrentHelperAmount = CurrentHelperAmount;
        AchievementManager.Instance.HelperGoal = HelperGoal;
        AchievementManager.Instance.TutorialCompleted = TutorialCompleted;
    }

    public static void RefreshData()
    {
        foreach (var helper in ShopManager.Instance.Helpers)
        {
            helper.AmountOwned = 0;
            helper.DynamicCost = helper.Cost;
            helper.DynamicIncrement = helper.Increment;
        }
    }
}
