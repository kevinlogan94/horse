using System;
using System.Linq;
using Assets.Scripts.Model;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Helper[] Helpers;

    public GameObject ShopPanel;
    public GameObject ShopTutorialPanel;
    public GameObject FingerPointerShop;
    public GameObject FingerPointerFeederButton;
    public GameObject ShopExclamationPoint;

    private float _waitTime = 1.0f;

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
            if (Monitor.Horses >= feeder?.Cost && feeder?.AmountOwned == 0)
            {
                FingerPointerShop.SetActive(true);
                ShopTutorialPanel.SetActive(true);
                FingerPointerFeederButton.SetActive(ShopPanel.activeSelf);
            }
        }
    }
    
    public void AddHelper(string helperName)
    {
        foreach (var helper in Helpers)
        {
            if (helper.Name == helperName && helper.DynamicCost <= Monitor.Horses)
            {
                Monitor.Horses -= helper.DynamicCost;
                helper.DynamicCost = (int) Math.Round(helper.DynamicCost * 1.5, 0);
                // Monitor.Instance.UpdatePassiveIncomeText();
                _audioManager.Play("CoinToss");
                if (helper.AmountOwned == 0)
                {
                    SplashManager.Instance.TriggerSplash(SplashType.Horse.ToString(), helper.HorseBreed);
                }
                helper.AmountOwned++;
            }

            if (helper.Name == "Feeder" && helper.AmountOwned == 1)
            {
                FingerPointerShop.SetActive(false);
                FingerPointerFeederButton.SetActive(false);
                ShopTutorialPanel.SetActive(false);
            }
        }
    }

    public void ManageExclamationPoint()
    {
        var showExclamationPoint = false;
        foreach (var helper in Helpers)
        {
            if (helper.LevelRequirement <= Monitor.PlayerLevel 
                && helper.DynamicCost <= Monitor.Horses)
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
        if (Time.time > _waitTime)
        {
            _waitTime = Time.time + 1.0f;
            for (var index = 0; index < Helpers.Length; index++)
            {
                var helper = Helpers[index];    
                if (helper.AmountOwned > 0)
                {
                    var increment = helper.DynamicIncrement > helper.Increment
                        ? helper.DynamicIncrement
                        : helper.Increment;
                    Monitor.Instance.IncrementHorses(increment * helper.AmountOwned, helper.HorseBreed, index*.25f);
                }
            }
        }
    }
}