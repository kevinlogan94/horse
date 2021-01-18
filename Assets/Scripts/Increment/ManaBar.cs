using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private Slider _manabar;
    public int ManaLevel;
    public bool InfiniteManaBuffActive;
    
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
        // Debug.Log(1.0f / Time.smoothDeltaTime);
        ManaRegen();
    }

    private void ManaRegen()
    {
        StartCoroutine(PerformRegeneration(0.270f));
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
        // var manaBarRect = gameObject.GetComponent<Rect>();
        // manaBarRect.width
    }
    
    private IEnumerator PerformRegeneration(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        var denominator = ManaLevel > 1 ? ManaLevel * 1.25 : 1;
        var regen = 0.6 / denominator;
        if (_manabar.value + regen < _manabar.maxValue)
        {
            _manabar.value += (float) regen;
        }
        else
        {
            _manabar.value = _manabar.maxValue;
        }
    }
}
