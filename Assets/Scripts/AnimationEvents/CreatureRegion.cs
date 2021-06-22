using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatureRegion : MonoBehaviour
{
    public GameObject MagicObject;
    private ObjectPooler _objectPooler;

    public void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }
    
    public void DisableActiveState()
    {
        gameObject.SetActive(false);
    }

    public void TriggerMagicOnCreature()
    {
        SetActiveMagicObject();
        TriggerIncrementAnimation();
    }

    private void SetActiveMagicObject()
    {
        MagicObject.SetActive(true);
    }

    private void TriggerIncrementAnimation()
    {
        var increment = IncrementPanel.GetClickerIncrement(IncrementPanel.ClickerIncrement, 10);
        var obj = _objectPooler.SpawnFromPool("IncrementBonusText", Input.mousePosition);
        var child = obj.transform.Find("IncrementBonusTextChild")?.gameObject;
        if (child != null)
        {
            child.GetComponentInChildren<TextMeshProUGUI>().text = "+" + Monitor.FormatNumberToString(increment);
        }
    }
}
