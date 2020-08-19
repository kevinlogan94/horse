﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Helper[] Helpers;
    public Upgrade[] Upgrades;

    public GameObject ShopPanel;
    public TextMeshProUGUI PassiveIncomeText;
    public GameObject FingerPointerShop;
    
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
        foreach (var helper in Helpers)
        {
            helper.AmountOwned = 0;
        }

        foreach (var upgrade in Upgrades)
        {
            upgrade.Level = 0;
        }

        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        HelperAction();
        
        //shop tutorial
        if (Monitor.PlayerLevel == 1)
        {
            var feeder = Helpers.FirstOrDefault(x => x.Name == "Feeder");
            if (Monitor.Horses >= feeder?.Cost && feeder?.AmountOwned == 0)
            {
                FingerPointerShop.SetActive(true);
            }
        }
    }
    
    public void AddHelper(string helperName)
    {
        foreach (var helper in Helpers)
        {
            if (helper.Name == helperName && helper.DynamicCost <= Monitor.Horses)
            {
                helper.AmountOwned++;
                Monitor.Horses -= helper.DynamicCost;
                helper.DynamicCost = (int) Math.Round(helper.DynamicCost * 2.5, 0);
                UpdatePassiveIncomeText();
                _audioManager.Play("CoinToss");
            }

            if (helper.Name == "Feeder" && helper.AmountOwned == 1)
            {
                Monitor.DestroyObject("FingerPointerShop");
                Monitor.DestroyObject("FingerPointerFeederButton");
            }
        }
    }
    
    public void AddUpgrade(string upgradeName)
    {
        foreach (var upgrade in Upgrades)
        {
            if (upgrade.Name == upgradeName && upgrade.DynamicCost <= Monitor.Horses)
            {
                upgrade.Level++;
                Monitor.Horses -= upgrade.DynamicCost;
                upgrade.DynamicCost *= 3;
                _audioManager.Play("CoinToss");
            }

            // if (upgrade.Name == "Feeder" && upgrade.AmountOwned == 1)
            // {
            //     Monitor.DestroyObject("FingerPointerShop");
            //     Monitor.DestroyObject("FingerPointerFeederButton");
            // }
        }
    }
    

    public void OpenShop()
    {
        _audioManager.Play("DoorBell");
        ShopPanel.SetActive(true);
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
                    Monitor.Instance.IncrementHorses(helper.Increment * helper.AmountOwned, helper.HorseBreed, index*.25f);
                }
            }
        }
    }
    
    public void UpdatePassiveIncomeText()
    {
        var passiveIncomeRate = Helpers.Where(helper => helper.AmountOwned > 0).Sum(helper => helper.AmountOwned * helper.Increment);
        PassiveIncomeText.text = "per second: " + String.Format("{0:n0}", passiveIncomeRate);
    }
}
