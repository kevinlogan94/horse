using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monitor : MonoBehaviour
{
    public GameObject FingerPointerIncrementButton;
    public TextMeshProUGUI PassiveIncomeText;
    public static int TotalHorsesEarned = 0;
    public static int Horses = 0;
    public static int PlayerLevel = 1;
    private ObjectPooler _objectPooler;
    private float _bottomHorseSpawnerRegion;
    private float _topHorseSpawnerRegion;

    #region Singleton
    public static Monitor Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public void Start()
    {
        _objectPooler = ObjectPooler.Instance;

        //button click tutorial
        if (TotalHorsesEarned == 0)
        {
            FingerPointerIncrementButton.SetActive(true);
        }
        
        _bottomHorseSpawnerRegion = GameObject.Find("Background").GetComponent<RectTransform>().offsetMin.y;
        var backgroundHeight = GameObject.Find("Background").GetComponent<RectTransform>().rect.height;
        var scorePanelHeight = GameObject.Find("ScorePanel").GetComponent<RectTransform>().rect.height;
        _topHorseSpawnerRegion = backgroundHeight - _bottomHorseSpawnerRegion - scorePanelHeight;
    }

    public void IncrementHorses(int increment, string horseBreed, float lagSeconds = 0)
    {
        Horses += increment;
        TotalHorsesEarned += increment;
        // _objectPooler.SpawnFromPool(horseBreed, new Vector3(0, Random.Range(250, 1500)));
        StartCoroutine(RemoveAfterSeconds(lagSeconds, horseBreed));
    }
    
    //https://forum.unity.com/threads/hide-object-after-time.291287/
    IEnumerator RemoveAfterSeconds(float seconds, string horseBreed)
    {
        yield return new WaitForSeconds(seconds);
        // Debug.Log(_bottomHorseSpawnerRegion);
        // Debug.Log(_topHorseSpawnerRegion);
        _objectPooler.SpawnFromPool(horseBreed, new Vector3(0, Random.Range(350, 1400)));
    }

    public static void DestroyObject(string fingerPointerLabel)
    {
        var fingerPointer = GameObject.Find(fingerPointerLabel);
        if (fingerPointer != null)
        {
            Destroy(fingerPointer);
        }
    }

    public static string FormatNumberToString(int intToConvertAndFormat)
    {
        if (intToConvertAndFormat > 1000000)
        {
            var newInt = Math.Round((double)intToConvertAndFormat / 1000000, 00);
            return newInt + "mil";
        }
        return String.Format("{0:n0}", intToConvertAndFormat);
    }
    
    public void UpdatePassiveIncomeText()
    {
        var passiveIncomeRate = ShopManager.Instance.Helpers.Where(helper => helper.AmountOwned > 0)
            .Sum(helper => helper.AmountOwned * (helper.DynamicIncrement > helper.Increment ? helper.DynamicIncrement : helper.Increment));
        PassiveIncomeText.text = "per second: " + FormatNumberToString(passiveIncomeRate);
    }
}