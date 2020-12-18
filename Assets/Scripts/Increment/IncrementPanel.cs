using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class IncrementPanel : MonoBehaviour
{
    private ObjectPooler _objectPooler;
    private AudioManager _audioManager;
    private float _waitTime;
    public static long ClickerIncrement = 1;
    public static long ClickCount = 0;
    public static long IncrementsThisSecond = 0;

    #region Singleton
    public static IncrementPanel Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
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
    
    private void PerformIncrement()
    {
        var randomNumber = Random.Range(0.0f, 3.0f);
        long increment;
        var creatureToSpawn = ShopManager.Instance.Helpers.LastOrDefault(helper => helper.AmountOwned > 0)?.Creature.CreatureAnimation;
        
        if (randomNumber <= 0.03)
        {
            increment = GetClickerIncrement(ClickerIncrement, 9);
        } 
        else if (randomNumber <= 0.30)
        {
            increment = GetClickerIncrement(ClickerIncrement, 3);
        }
        else
        {
            increment = GetClickerIncrement(ClickerIncrement, 1);
        }
            
        _audioManager.Play("Cork", randomNumber);
        if (randomNumber <= 0.03)
        {
            var obj = _objectPooler.SpawnFromPool("IncrementBonusText", Input.mousePosition);
            var child = obj.transform.Find("IncrementBonusTextChild")?.gameObject;
            if (child != null)
            {
                child.GetComponentInChildren<TextMeshProUGUI>().text = "+" + Monitor.FormatNumberToString(increment);
            }
        }
        else
        {
            var obj = _objectPooler.SpawnFromPool("IncrementText", Input.mousePosition);
            var child = obj.transform.Find("IncrementTextChild")?.gameObject;
            if (child != null)
            {
                child.GetComponentInChildren<TextMeshProUGUI>().text = "+" + Monitor.FormatNumberToString(increment);
            }
        }

        if (creatureToSpawn != null)
        {
            Monitor.Instance.IncrementInfluence(increment, (CreatureAnimations) creatureToSpawn);
        }
        ClickCount++;
        IncrementsThisSecond+=increment;
        var pointer = GameObject.Find("FingerPointerIncrementPanel");
        if (pointer)
        {
            pointer.SetActive(false);
        }

        if (ClickCount%10 == 0 && Monitor.UseAnalytics)
        {
                AnalyticsEvent.AchievementStep((int)ClickCount, "ClickCount");
        }
        
        ManaBar.Instance.DeductMana();
        SpawnIncrementAnimation();
        // Monitor.DestroyObject("FingerPointerIncrementPanel");
    }

    public void Increment()
    {
        if (ManaBar.Instance.HasEnoughMana())
        {
            PerformIncrement();
            _audioManager.Play("MagicSpell");
        }
        if (BuffManager.Instance.BuffActive)
        {
            BuffManager.Instance.CountDownStarted = true;
        }
    }

    private void SpawnIncrementAnimation()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _objectPooler.SpawnFromPool("Cast", Input.GetTouch(0).position);
        }
        else
        {
            _objectPooler.SpawnFromPool("Cast", Input.mousePosition);   
        }
    }

    public static long GetClickerIncrement(long clickerIncrement, int multIncrease)
    {
        return clickerIncrement * multIncrease;
    }
}
