using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private Slider _manabar;
    public int ManaLevel;
    public bool InfiniteManaBuffActive;
    private float CurrentFrameRate;
    
    #region Singleton
    public static ManaBar Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        if (ManaLevel == 0)
        {
            ManaLevel = 1;
        }
        _manabar = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentFrameRate = 1.0f / Time.smoothDeltaTime;
        ManaRegen();
    }

    private void ManaRegen()
    {
        var numerator = 0.6;
        if (CurrentFrameRate < 75) // ios Frame rate
        { 
            numerator = 3;
        }
        else if(CurrentFrameRate < 135)
        {
            numerator = 1.65;
        }
        var denominator = ManaLevel > 1 ? ManaLevel * 1.25 : 1;
        var regen = numerator / denominator;
        if (_manabar.value + regen < _manabar.maxValue)
        {
            _manabar.value += (float) regen;
        }
        else
        {
            _manabar.value = _manabar.maxValue;
        }
    }

    private float GetManaDeduction()
    {
        return _manabar.maxValue / (ManaLevel * 3);
    }

    public bool HasEnoughMana()
    {
        var manaDeduction = GetManaDeduction();
        return manaDeduction <= _manabar.value;
    }

    public void DeductMana()
    {
        var manaDeduction = InfiniteManaBuffActive ? 0 : GetManaDeduction();
        if (_manabar.value - manaDeduction >= 0)
        {
            _manabar.value -= manaDeduction;
        }
        else
        {
            _manabar.value = 0;
        }
    }

    public void LevelUpManaBar()
    {
        ManaLevel++;
    }
}
