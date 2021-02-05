using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monitor : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject CreditsPanel;
    public GameObject FingerPointerIncrementButton;
    public GameObject FingerPointerOutlook;
    public TextMeshProUGUI PassiveIncomeText;
    public static long TotalInfluenceEarned = 0;
    public static long Influence = 0;
    public static int PlayerLevel = 1;
    public static DateTime? LastSavedDateTime;
    private ObjectPooler _objectPooler;
    private float _bottomCreatureSpawnerRegion;
    private float _topCreatureSpawnerRegion;
    private DateTime _timeOfLastFrame;

    //TODO turn analytics back on
    public static bool UseAnalytics = false;
    public const bool UseBetaSurvey = true;
    public static bool BetaSurveyDisplayed = false;

    #region Singleton
    public static Monitor Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    void Start()
    {
        SaveGame.Load();
        if (LastSavedDateTime != null)
        {
            SplashManager.Instance.TriggerSplash(SplashType.InfluenceOverTime.ToString());
        }
        _timeOfLastFrame = DateTime.UtcNow; // Need to have it start somewhere
        _objectPooler = ObjectPooler.Instance;
        var worldCorners = new Vector3[4];
        GameObject.Find("SpawnPanel").GetComponent<RectTransform>().GetWorldCorners(worldCorners);
        _topCreatureSpawnerRegion = worldCorners[1].y;
        _bottomCreatureSpawnerRegion = worldCorners[0].y;
    }

    void Update()
    {
        IncrementInfluenceForTimeAwayFromGameWithoutKillingApp();
        ManageBetaSurvey();
    }

    public void ManageBetaSurvey()
    {
        if (!UseBetaSurvey || PlayerLevel != 11 || BetaSurveyDisplayed) return;
        BetaSurveyDisplayed = true;
        SplashManager.Instance.TriggerSplash(SplashType.Survey.ToString());
    }

    public void TriggerOutlookTutorial()
    {
        FingerPointerIncrementButton.SetActive(true);
        FingerPointerOutlook.SetActive(true);
    }

    public void IncrementInfluence(long increment, Creature creature = null, float lagSeconds = 0)
    {
        SaveGame.Save();
        Influence += increment;
        TotalInfluenceEarned += increment;
        // _objectPooler.SpawnFromPool(horseBreed, new Vector3(0, Random.Range(250, 1500)));
        if (creature != null && CreatureCanSpawn(creature))
        {
            StartCoroutine(SpawnCreatureAfterSeconds(lagSeconds, creature.CreatureAnimation));
        }
    }
    
    //https://forum.unity.com/threads/hide-object-after-time.291287/
    IEnumerator SpawnCreatureAfterSeconds(float seconds, CreatureAnimations creatureAnimation)
    {
        yield return new WaitForSeconds(seconds);
        var spawnedGameObject = _objectPooler.SpawnFromPool("Creature", new Vector3(0, Random.Range(_bottomCreatureSpawnerRegion, _topCreatureSpawnerRegion)));
        PlayAnimationOnGameObject(spawnedGameObject, creatureAnimation);
    }

    // https://www.youtube.com/watch?v=nBkiSJ5z-hE
    private void PlayAnimationOnGameObject(GameObject gameObjectToTriggerAnimation, CreatureAnimations creatureAnimation)
    {
        var animator = gameObjectToTriggerAnimation.GetComponent<Animator>();
        if (animator == null)
        {
          Debug.LogWarning("We couldn't find an animator on the object: " + gameObjectToTriggerAnimation.name);
          return;
        }
        animator.Play(creatureAnimation.ToString());
    }
    
    public void UpdatePassiveIncomeText()
    {
        var passiveIncomeRate = GetHelperPassiveIncome();
        passiveIncomeRate += IncrementPanel.IncrementsThisSecond;
        PassiveIncomeText.text = FormatNumberToString(passiveIncomeRate) + "/sec";
    }

    #region Private Methods
    //This is if the player closes the app to go to a different one then comes back to it later without killing the app.
    private void IncrementInfluenceForTimeAwayFromGameWithoutKillingApp()
    {
        var now = DateTime.UtcNow;
        var timeAwayFromGame = (long) Math.Round(now.Subtract(_timeOfLastFrame).TotalSeconds);
        if (timeAwayFromGame > 5)
        {
            Debug.Log("Giving influence for time since last frame processed.");
            IncrementInfluence(GetInfluenceReceivedOverTime(timeAwayFromGame));
        }
        _timeOfLastFrame = now;
    }

    #endregion

    #region Helper Methods
    
    private bool CreatureCanSpawn(Creature creature)
    {
        switch (creature.Name)
        {
            case "Elk": 
            case "Raiju":
            case "Basilisk":
                return CanvasBackgroundController.Instance.CurrentCanvasBackground == CanvasBackground.Meadow;
            case "Hippocampus":
            case "Bluecap":
            case "Griffin":
                return CanvasBackgroundController.Instance.CurrentCanvasBackground == CanvasBackground.River;
            case "Abraxas":
            case "Phoenix":
            case "Void Spawn":
            case "Wraith":
                return CanvasBackgroundController.Instance.CurrentCanvasBackground == CanvasBackground.Altar;
            default:
                return false;
        }
    }
    
    // public bool PanelsAreDisplaying()
    // {
    //     if (SceneManager.Instance.ScenePanel.activeSelf)
    //     {
    //         return true;
    //     }
    //     if (ShopManager.Instance.ShopPanel.activeSelf)
    //     {
    //         return true;
    //     }
    //     if (SplashManager.Instance.SplashPanel.activeSelf)
    //     {
    //         return true;
    //     }
    //     if (AchievementManager.Instance.AchievementPanel.activeSelf)
    //     {
    //         return true;
    //     }
    //     if (LevelUp.Instance.LevelUpPanel.activeSelf)
    //     {
    //         return true;
    //     }
    //     if (CreditsPanel.activeSelf)
    //     {
    //         return true;
    //     }
    //     if (SettingsPanel.activeSelf)
    //     {
    //         return true;
    //     }
    //     return false;
    // }
    
    public static void DestroyObject(string fingerPointerLabel)
    {
        var fingerPointer = GameObject.Find(fingerPointerLabel);
        if (fingerPointer != null)
        {
            Destroy(fingerPointer);
        }
    }

    public static string FormatNumberToString(long intToConvertAndFormat)
    {
        if (intToConvertAndFormat >= 1000000 && intToConvertAndFormat < 1000000000)
        {
            var newInt = Math.Round((double)intToConvertAndFormat / 1000000, 2);
            return newInt + "mill";
        }  
        if (intToConvertAndFormat >= 1000000000 && intToConvertAndFormat < 1000000000000)
        {
            var newInt = Math.Round((double)intToConvertAndFormat / 1000000000, 2);
            return newInt + "bill";
        }
        if (intToConvertAndFormat >= 1000000000000)
        {
            var newInt = Math.Round((double)intToConvertAndFormat / 1000000000000, 2);
            return newInt + "trill";
        }
        return String.Format("{0:n0}", intToConvertAndFormat);
    }

    public long GetHelperPassiveIncome()
    {
        return ShopManager.Instance.Helpers.Where(helper => helper.AmountOwned > 0)
            .Sum(helper => helper.AmountOwned * (helper.DynamicIncrement > helper.Increment 
                ? helper.DynamicIncrement 
                : helper.Increment));
    }

    public long GetInfluenceReceivedOverTime(long seconds)
    {
        var incrementPerSecond = GetHelperPassiveIncome();
        return incrementPerSecond * seconds; 
    }

    #endregion
}

public enum CreatureAnimations
{
    WaterHorseAnimation = 1,
    FireHorseAnimation = 2,
    RaijuRunAnimation = 4,
    WraithAnimation = 5,
    ElkAnimation = 6,
    WispAnimation = 7,
    GriffinAnimation = 8,
    BasiliskAnimation = 9,
    PhoenixAnimation = 10,
    VoidSpawnAnimation = 11,
    None = 99
}