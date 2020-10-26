using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class ShopManager : MonoBehaviour
{
    public Helper[] Helpers;

    public GameObject ShopPanel;
    public GameObject FingerPointerShop;
    public GameObject FingerPointerFeederButton;
    public GameObject ShopExclamationPoint;
    public GameObject FingerPointerXal;

    private float _waitTime = 1.0f;
    private float _currentWaitTime = 1.0f;

    private AudioManager _audioManager;
    
    #region Singleton
    public static ShopManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        ManageExclamationPoint();
        HelperAction();
        
        //shop tutorial
        if (Monitor.PlayerLevel == 1)
        {
            var feeder = Helpers.FirstOrDefault(x => x.Name == "Feeder");
            if (Monitor.Influence >= feeder?.Cost && feeder?.AmountOwned == 0)
            {
                FingerPointerShop.SetActive(!ShopPanel.activeSelf);
                // ShopTutorialPanel.SetActive(true);
                FingerPointerFeederButton.SetActive(ShopPanel.activeSelf);
            }
        }
    }
    
    public void AddHelper(string helperName)
    {
        foreach (var helper in Helpers)
        {
            if (helper.Name == helperName && helper.DynamicCost <= Monitor.Influence)
            {
                Monitor.Influence -= helper.DynamicCost;
                helper.DynamicCost = (int) Math.Round(helper.DynamicCost * 1.5, 0);
                // Monitor.Instance.UpdatePassiveIncomeText();
                _audioManager.Play("CoinToss");
                if (helper.AmountOwned == 0)
                {
                    SplashManager.Instance.TriggerSplash(SplashType.Creature.ToString(), helper.Creature.Name);
                }
                helper.AmountOwned++;
                if (Monitor.UseAnalytics)
                {
                    AnalyticsEvent.AchievementStep(Helpers.Sum(x=>x.AmountOwned), "HelperCount");
                }
            }

            if (helper.Name == "Feeder" && helper.AmountOwned == 1)
            {
                FingerPointerShop.SetActive(false);
                FingerPointerFeederButton.SetActive(false);
                // ShopTutorialPanel.SetActive(false);
                if (SceneManager.Instance.TutorialActive)
                {
                    FingerPointerXal.SetActive(true);
                }
            }
        }
    }

    public void ManageExclamationPoint()
    {
        var showExclamationPoint = false;
        foreach (var helper in Helpers)
        {
            if (helper.LevelRequirement <= Monitor.PlayerLevel 
                && helper.DynamicCost <= Monitor.Influence
                && SceneManager.Instance.ActiveChapter == 0)
            {
                showExclamationPoint = true;
                ShopExclamationPoint.SetActive(true);
            }
        }

        if (!showExclamationPoint)
        {
            ShopExclamationPoint.SetActive(false);
        }
    }
    
    private void HelperAction()
    {
        if (Time.time > _currentWaitTime && !SceneManager.Instance.TutorialActive && !Monitor.Instance.FingerPointerIncrementButton.activeSelf)
        {
            _currentWaitTime = Time.time + _waitTime;
            for (var index = 0; index < Helpers.Length; index++)
            {
                var helper = Helpers[index];    
                if (helper.AmountOwned > 0)
                {
                    var increment = helper.DynamicIncrement > helper.Increment
                        ? helper.DynamicIncrement
                        : helper.Increment;
                    // Monitor.Instance.IncrementInfluence(increment * helper.AmountOwned, helper.Creature.CreatureAnimation, index*.25f);
                }
            }
        }
    }
}