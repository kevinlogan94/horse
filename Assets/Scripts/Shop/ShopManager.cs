using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Helper[] Helpers;

    public GameObject ShopPanel;
    public GameObject FingerPointerShop;
    public GameObject FingerPointerFeederButton;
    
    private float _waitTime = 1.0f;

    private Upgrade _clickerUpgradeReference;
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
                helper.AmountOwned++;
                Monitor.Horses -= helper.DynamicCost;
                helper.DynamicCost = (int) Math.Round(helper.DynamicCost * 1.5, 0);
                // Monitor.Instance.UpdatePassiveIncomeText();
                _audioManager.Play("CoinToss");
            }

            if (helper.Name == "Feeder" && helper.AmountOwned == 1)
            {
                FingerPointerShop.SetActive(false);
                FingerPointerFeederButton.SetActive(false);
            }
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
                    var increment = helper.DynamicIncrement > helper.Increment
                        ? helper.DynamicIncrement
                        : helper.Increment;
                    Monitor.Instance.IncrementHorses(increment * helper.AmountOwned, helper.HorseBreed, index*.25f);
                }
            }
        }
    }
}