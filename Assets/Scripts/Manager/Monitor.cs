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
    public static long TotalInfluenceEarned = 0;
    public static long Influence = 0;
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
        SaveGame.Load();
        
        _objectPooler = ObjectPooler.Instance;

        //button click tutorial
        if (TotalInfluenceEarned == 0)
        {
            FingerPointerIncrementButton.SetActive(true);
        }
        
        _bottomHorseSpawnerRegion = GameObject.Find("Background").GetComponent<RectTransform>().offsetMin.y;
        var backgroundHeight = GameObject.Find("Background").GetComponent<RectTransform>().rect.height;
        var scorePanelHeight = GameObject.Find("ScorePanel").GetComponent<RectTransform>().rect.height;
        _topHorseSpawnerRegion = backgroundHeight - _bottomHorseSpawnerRegion - scorePanelHeight;
    }

    public void IncrementInfluence(int increment, string horseBreed, float lagSeconds = 0)
    {
        SaveGame.Save();
        Influence += increment;
        TotalInfluenceEarned += increment;
        // _objectPooler.SpawnFromPool(horseBreed, new Vector3(0, Random.Range(250, 1500)));
        StartCoroutine(SpawnHorseAfterSeconds(lagSeconds, horseBreed));
    }
    
    //https://forum.unity.com/threads/hide-object-after-time.291287/
    IEnumerator SpawnHorseAfterSeconds(float seconds, string horseBreed)
    {
        yield return new WaitForSeconds(seconds);
        // Debug.Log(_bottomHorseSpawnerRegion);
        // Debug.Log(_topHorseSpawnerRegion);
        _objectPooler.SpawnFromPool(horseBreed, new Vector3(0, Random.Range(350, 1400)));
    }

    /* TODO
     * I need to finish this.
     * https://www.youtube.com/watch?v=nBkiSJ5z-hE
     */
    private void PlayAnimationOnGameObject(GameObject gameObjectToTriggerAnimation, string newAnimation)
    {
        var animator = gameObjectToTriggerAnimation.GetComponent<Animator>();
        if (animator == null)
        {
          Debug.LogWarning("We couldn't find an animator on the object: " + gameObjectToTriggerAnimation.name);
          return;
        }
        animator.Play(newAnimation);
    }

    public static void DestroyObject(string fingerPointerLabel)
    {
        var fingerPointer = GameObject.Find(fingerPointerLabel);
        if (fingerPointer != null)
        {
            Destroy(fingerPointer);
        }
    }

    public static string FormatNumberToString(long intToConvertAndFormat)
    {
        if (intToConvertAndFormat >= 1000000 && intToConvertAndFormat < 1000000000)
        {
            var newInt = Math.Round((double)intToConvertAndFormat / 1000000, 2);
            return newInt + "mill";
        }  
        if (intToConvertAndFormat >= 1000000000 && intToConvertAndFormat < 1000000000000)
        {
            var newInt = Math.Round((double)intToConvertAndFormat / 1000000000, 2);
            return newInt + "bill";
        }
        if (intToConvertAndFormat >= 1000000000000)
        {
            var newInt = Math.Round((double)intToConvertAndFormat / 1000000000000, 2);
            return newInt + "trill";
        }
        return String.Format("{0:n0}", intToConvertAndFormat);
    }
    
    public void UpdatePassiveIncomeText()
    {
        var passiveIncomeRate = GetHelperPassiveIncome();
        passiveIncomeRate += IncrementButton.IncrementsThisSecond;
        PassiveIncomeText.text = FormatNumberToString(passiveIncomeRate) + "/sec";
    }

    public int GetHelperPassiveIncome()
    {
        return ShopManager.Instance.Helpers.Where(helper => helper.AmountOwned > 0)
            .Sum(helper => helper.AmountOwned * (helper.DynamicIncrement > helper.Increment ? helper.DynamicIncrement : helper.Increment));
    }

    public int GetInfluenceReceivedOverTime(int seconds)
    {
        var incrementPerSecond = GetHelperPassiveIncome();
        return incrementPerSecond * seconds; 
    }
}