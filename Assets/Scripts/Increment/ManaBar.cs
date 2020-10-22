using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private Slider _manabar;
    public int ManaLevel;
    
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
        ManaRegen();
    }

    private void ManaRegen()
    {
        var regen = 0.6 / ManaLevel;
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
        switch (ManaLevel)
        {
            case 1:
                return _manabar.maxValue / 4;
            case 2:
                return _manabar.maxValue / 8;
            case 3:
                return _manabar.maxValue / 12;
            case 4:
                return _manabar.maxValue / 16;
            case 5:
                return _manabar.maxValue / 20;
            default:
                return 1;
        }
    }

    public bool HasEnoughMana()
    {
        var manaDeduction = GetManaDeduction();
        return manaDeduction <= _manabar.value;
    }

    public void DeductMana()
    {
        var manaDeduction = GetManaDeduction();
        if (_manabar.value - manaDeduction >= 0)
        {
            _manabar.value -= manaDeduction;
        }
        else
        {
            _manabar.value = 0;
        }
    }
}
