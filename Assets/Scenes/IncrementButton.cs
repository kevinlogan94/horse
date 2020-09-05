﻿using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class IncrementButton : MonoBehaviour
{
    private ObjectPooler _objectPooler;
    private AudioManager _audioManager;
    private float _waitTime;
    public static int ClickerLevel = 0;
    public static int ClickCount = 0;
    public static int IncrementsThisSecond = 0;

    public void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        UpdatePassiveIncomeAndRefresh();
    }

    private void UpdatePassiveIncomeAndRefresh()
    {
        if (Time.time > _waitTime)
        {
            _waitTime = Time.time + 1.0f;
            Monitor.Instance.UpdatePassiveIncomeText();
            IncrementsThisSecond = 0;
        }
    }
    
    public void Increment()
    {
        var randomNumber = Random.Range(0.0f, 3.0f);
        var increment = 1;
        if (randomNumber <= 0.03)
        {
            increment = ClickerLevel > 0 ? (int)Math.Pow(135, ClickerLevel) : 9;
            Monitor.Instance.IncrementHorses(increment, "Unicorn");
        } 
        else if (randomNumber <= 0.30)
        {
            var helperHorse = ShopManager.Instance.Helpers[ClickerLevel + 1].HorseBreed;
            increment = ClickerLevel > 0 ? (int)Math.Pow(45, ClickerLevel) : 3;
            Monitor.Instance.IncrementHorses(increment, helperHorse);
        }
        else
        {
            var helperHorse = ShopManager.Instance.Helpers[ClickerLevel].HorseBreed;
            increment = ClickerLevel > 0 ? (int)Math.Pow(15, ClickerLevel) : increment;
            Monitor.Instance.IncrementHorses(increment, helperHorse);
        }
            
        _audioManager.Play("Cork", randomNumber);
        // if (randomNumber <= 0.03)
        // {
        //     var obj = _objectPooler.SpawnFromPool("IncrementBonusText");
        //     obj.GetComponentInChildren<TextMeshProUGUI>().text = "+" + increment;
        // }
        // else
        // {
            var obj = _objectPooler.SpawnFromPool("IncrementText");
            obj.GetComponentInChildren<TextMeshProUGUI>().text = "+" + Monitor.FormatNumberToString(increment);
        // }

        ClickCount++;
        IncrementsThisSecond+=increment;
        Monitor.DestroyObject("FingerPointerIncrementButton");
    }
}
