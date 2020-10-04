using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public GameObject AchievementPanel;
    public GameObject AchievementPointer;
    public GameObject AchievementExclamationPoint;
    
    public string TwitterUrl = "";
    public string InstagramUrl = "";
    public string FacebookUrl = "";
    
    //login
    public int LoginCount = 1;
    public float LoginGoal = 2;
    
    //clicker
    public long CurrentClickedAmount;
    public float ClickerGoal;
    
    //helper
    public int CurrentHelperAmount;
    public float HelperGoal;
    
    //tutorial
    public bool TutorialCompleted = false;
    
    //sharing and/or reviewing the app
    public const int ShareGoal = 1;
    public bool AppStoreReviewed = false;

    #region Singleton
    public static AchievementManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        ClickerGoal = 300;
        HelperGoal = 30;
    }

    // Update is called once per frame
    void Update()
    {
        ClickerProgress();
        HelperProgress();
        ManageExclamationPoint();
        Tutorial();
    }

    public void Tutorial()
    {
        if ((CurrentClickedAmount >= ClickerGoal 
             || CurrentHelperAmount >= HelperGoal) 
            && !AchievementPanel.activeSelf
            && !TutorialCompleted)
        {
            AchievementPointer.SetActive(true);
        }
        else
        {
            AchievementPointer.SetActive(false);
        }
    }

    public void ManageExclamationPoint()
    {
        var showExclamationPoint = AchievementReady();
        if (SceneManager.Instance.ActiveChapter == 0)
        {
            AchievementExclamationPoint.SetActive(showExclamationPoint);
        }
    }

    public bool AchievementReady()
    {
        var ready = false;
        if (ClickerGoal <= CurrentClickedAmount)
        {
            ready = true;
        } else if (HelperGoal <= CurrentHelperAmount)
        {
            ready = true;
        } else if (LoginGoal <= LoginCount)
        {
            ready = true;
        }
        return ready;
    }
    
    public void ClickerProgress()
    {
        CurrentClickedAmount = IncrementButton.ClickCount;
    }

    public void HelperProgress()
    {
        CurrentHelperAmount = ShopManager.Instance.Helpers.Sum(x => x.AmountOwned);
    }
}