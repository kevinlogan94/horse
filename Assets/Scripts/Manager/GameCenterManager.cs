using System.Linq;
using System.Runtime.Serialization;
using CloudOnce;
using UnityEngine;
using UnityEngine.Analytics;

// https://gamegorillaz.com/blog/game-center-setup-in-unity/
public class GameCenterManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public static void ReportAchievementUnlocked(string achievementId)
    {
        if (!Cloud.IsSignedIn)
        {
            Debug.Log($"Player is not signed in. Cancelling achievement unlock for {Cloud.PlayerDisplayName}");
            return;
        }
        var achievement = Achievements.All.FirstOrDefault(x => x.ID == achievementId);
        if (achievement == null)
        {
            Debug.Log($"Not Found: Achievement {achievementId} was not found when trying to report progress for {Cloud.PlayerDisplayName}.");
            return;
        }
        if (achievement.IsUnlocked)
        {
            Debug.Log($"Completed: Achievement {achievementId} has already been completed by {Cloud.PlayerDisplayName}");
            return;
        }
        
        achievement.Unlock(success =>
        {
            if (success.Result)
            {
                Debug.Log($"Progress reported successfully for {Cloud.PlayerDisplayName} on achievement: {achievementId}");
                if (Monitor.UseAnalytics)
                {
                    AnalyticsEvent.AchievementUnlocked(achievementId);
                }
                return;
            }
            Debug.Log($"Progress report failed for user {Cloud.PlayerDisplayName} on achievement: {achievementId}");
        });
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
        Cycle, //TODO Add replay in to the game for this.
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