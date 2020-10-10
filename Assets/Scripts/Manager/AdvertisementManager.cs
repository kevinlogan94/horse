using System.Linq;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisementManager : MonoBehaviour, IUnityAdsListener
{
    private const string IosGameId = "3857318";
    private const string AndroidGameId = "3857319";
    private const string RewardVideoPlacementId = "rewardedVideo";
    private const string SkippableAdPlacementId = "video";
    public const bool TestMode = true;

    private long _reward;
    
    #region Singleton
    public static AdvertisementManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        var gameId = Application.platform == RuntimePlatform.Android ? AndroidGameId : IosGameId;
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, TestMode);
    }

    public void ShowRewardedAd(long reward)
    {
        _reward = reward;
        Advertisement.Show(RewardVideoPlacementId);
    }

    public void ShowSkippableAd()
    {
        Advertisement.Show(SkippableAdPlacementId);
    }

    private void TriggerReward()
    {
        var helperHorse = ShopManager.Instance.Helpers.LastOrDefault(helper => helper.AmountOwned > 0)?.HorseBreed;
        Monitor.Instance.IncrementInfluence(_reward, helperHorse);
    }
    
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished when placementId == RewardVideoPlacementId:
                Debug.Log("Reward the player");
                TriggerReward();
                AchievementManager.Instance.CurrentWatchedAdvertisements++;
                break;
            case ShowResult.Finished when placementId == SkippableAdPlacementId:
                Debug.Log("Finished skippable ad");
                AchievementManager.Instance.CurrentWatchedAdvertisements++;
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped");
                break;
            case ShowResult.Failed:
                Debug.LogWarning("The ad didn't finish due to an error");
                break;
        }
    }
    
    public void OnUnityAdsReady(string placementId)
    {
        //ads are ready
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError(message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //Analytics
    }
}
