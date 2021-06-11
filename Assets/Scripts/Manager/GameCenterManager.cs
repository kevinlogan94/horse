using System;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Analytics;

public class GameCenterManager : MonoBehaviour
{
    // public string LeaderBoardId;
    public static UnityEngine.SocialPlatforms.IAchievement[] LoadedAchievements; 
    
    // Start is called before the first frame update
    void Start()
    {
        AuthenticateUser();
    }

    private void AuthenticateUser()
    {
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                if (Monitor.UseAnalytics)
                {
                    AnalyticsEvent.UserSignup(Social.Active.ToString());
                }
                Debug.Log($"Login Successful for {Social.localUser.userName}.");
                Social.LoadAchievements(achievements =>
                {
                    Debug.Log(achievements.Length == 0
                        ? "No Achievements found."
                        : $"Achievements Loaded Successfully. Count: {achievements.Length}.");
                    LoadedAchievements = achievements;
                });
                return;
            }
            Debug.Log("Login Failure");
        });
    }
    
    public static void ReportAchievementProgress(string achievementId, double progress)
    {
        if (Social.localUser.authenticated)
        {
            UnityEngine.SocialPlatforms.IAchievement achievement = null;
            Social.LoadAchievements(achievements =>
            {
                achievement = achievements.SingleOrDefault(x => x.id == achievementId);
            });
            if (achievement == null)
            {
                Debug.Log($"Not Found: Achievement {achievementId} was not found when trying to report for {Social.localUser.userName}.");
                return;
            }
            if (achievement.completed)
            {
                Debug.Log($"Completed: Achievement {achievementId} has already been completed by {Social.localUser.userName}");
                return;
            }
            
            Social.ReportProgress(achievementId, progress, success =>
            {
                if (success)
                {
                    Debug.Log($"Progress reported successfully for user {Social.localUser.userName} on achievement {achievementId}");
                    if (Monitor.UseAnalytics && (int)progress == 100)
                    {
                        AnalyticsEvent.AchievementUnlocked(achievementId);
                    }
                    Social.ShowAchievementsUI();
                    return;
                }
                Debug.Log($"Progress report failed for user {Social.localUser.userName} on achievement {achievementId}");
            });
            return;
        }
        Debug.Log("User is not logged in. Achievement report failed.");
    }

    // public void PostScoreOnLeaderBoard(int myScore)
    // {
    //     if (Social.localUser.authenticated)
    //     {
    //         Social.ReportScore(myScore, LeaderBoardId, success =>
    //         {
    //             if (success)
    //             {
    //                 Debug.Log($"Score reported successfully for {Social.localUser.userName} on leaderboard {LeaderBoardId}");
    //                 return;
    //             }
    //             Debug.Log($"Score report failed for {Social.localUser.userName} on leaderboard {LeaderBoardId}");
    //         });
    //     }
    // }
    public enum GameCenterAchievement
    {
        [EnumMember(Value = "master")]
        Master,
        [EnumMember(Value = "scholar")]
        Scholar,
        [EnumMember(Value = "caster")]
        Caster,
        [EnumMember(Value = "high_achiever")]
        HighAchiever,
        [EnumMember(Value = "follower")]
        Follower,
        [EnumMember(Value = "end_of_an_age")]
        EndOfAnAge,
        [EnumMember(Value = "tapper")]
        Tapper,
        [EnumMember(Value = "the_beginning")]
        Beginning,
        [EnumMember(Value = "welcome_back")]
        WelcomeBack,
        [EnumMember(Value = "cycle")]
        Cycle,
        [EnumMember(Value = "appraiser")]
        Appraiser,
        [EnumMember(Value = "thank_you")]
        ThankYou,
        [EnumMember(Value = "collector")]
        Collector,
        [EnumMember(Value = "traveler")]
        Traveler, //TODO Add the portal in to the game for this.
        [EnumMember(Value = "guardian")]
        Guardian
    }
}