using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

//Note: The Magic and CreatureRegion gameobjects share this.
public class CreatureRegion : MonoBehaviour
{
    public GameObject MagicObject;
    public GameObject CreatureObject;
    private ObjectPooler _objectPooler;
    private AudioManager _audioManager;

    public void Start()
    {
        
        _objectPooler = ObjectPooler.Instance;
        _audioManager = FindObjectOfType<AudioManager>();
    }
    
    public void ResetAndDisableActiveState()
    {
        CreatureObject.SetActive(true);
        MagicObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void DisableCreatureObject()
    {
        CreatureObject.SetActive(false);
    }

    public void EnableMagicIdleAnimation()
    {
        var magicAnimator = MagicObject.GetComponent<Animator>();
        if (magicAnimator == null)
        {
            Debug.LogWarning("We couldn't find the animator for the magic gameobject.");
            return;
        }
        magicAnimator.Play(MagicAnimations.MagicIdle.ToString());
    }

    public void TriggerMagicOnCreature()
    {
        if (ManaBar.Instance.HasEnoughMana() && !MagicObject.activeSelf)
        {
            MagicObject.SetActive(true);
            PerformIncrement();
            _audioManager.Play("MagicSpell");
        }
        if (BuffManager.Instance.BuffActive)
        {
            BuffManager.Instance.CountDownStarted = true;
        }
    }

    private void PerformIncrement()
    {
        long increment;
        var randomNumber = Random.Range(0.0f, 1.0f);
        if (randomNumber <= 0.20)
        {
            increment = IncrementPanel.GetClickerIncrement(IncrementPanel.ClickerIncrement, 8);
        }
        else
        {
            increment = IncrementPanel.GetClickerIncrement(IncrementPanel.ClickerIncrement, 5);
        }
        
        var obj = _objectPooler.SpawnFromPool("IncrementBonusText", Input.mousePosition);
        var child = obj.transform.Find("IncrementBonusTextChild")?.gameObject;
        if (child != null)
        {
            child.GetComponentInChildren<TextMeshProUGUI>().text = "+" + Monitor.FormatNumberToString(increment);
        }

        ManaBar.Instance.DeductMana();
        Monitor.Instance.IncrementInfluence(increment);
        IncrementPanel.IncrementsThisSecond+=increment;
        IncrementPanel.ClickCount++;
        
        if (!BuffManager.Instance.BuffActive)
        {
            BuffManager.Instance.ClickCountSinceLastBuff++;   
        }
        else
        {
            BuffManager.Instance.ClickCountForThisBuffSession++;
        }
        
        var pointer = GameObject.Find("FingerPointerIncrementPanel");
        if (pointer)
        {
            pointer.SetActive(false);
        }

        if (IncrementPanel.ClickCount%10 == 0 && Monitor.UseAnalytics)
        {
            Analytics.CustomEvent("ClickIncrementButton", new Dictionary<string, object>
            {
                {"ClickCount", IncrementPanel.ClickCount}
            });
        }
    }
}

public enum MagicAnimations
{
    MagicEffect,
    MagicIdle
}