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
    
    //login
    public int LoginCount = 1;
    public float LoginGoal = 2;
    
    //clicker
    public long CurrentClickedAmount;
    public float ClickerGoal;
    
    //helper
    public int CurrentHelperAmount;
    public float HelperGoal;

    //video/advertisement
    public int CurrentVideoAmount;
    public float VideoGoal;
    
    //achievements
    public int CurrentAchievementAmount;
    public float AchievementGoal;
    
    //story - not saving this in saved file because this is just total chapters
    public float StoryGoal; 

    //sharing and/or reviewing the app
    public const int ShareGoal = 1;
    public bool AppStoreReviewed = false;
    public bool FollowedOnTwitter = false;
    
    //tutorial
    public bool TutorialCompleted = false;
    
    private AudioManager _audioManager;

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
        _audioManager = FindObjectOfType<AudioManager>();
        ClickerGoal = 150;
        HelperGoal = 30;
        VideoGoal = 10;
        AchievementGoal = 10;
        StoryGoal = 1;
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

    public void PlayAchievementSound()
    {
        _audioManager.Play("LevelUp");
    }

    private void ManageExclamationPoint()
    {
        var showExclamationPoint = AchievementReady();
        AchievementExclamationPoint.SetActive(SceneManager.Instance.ActiveChapter == 0 && showExclamationPoint);
    }

    private bool AchievementReady()
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
        } else if (VideoGoal <= CurrentVideoAmount)
        {
            ready = true;
        }
        return ready;
    }

    private void ClickerProgress()
    {
        CurrentClickedAmount = IncrementPanel.ClickCount;
    }

    private void HelperProgress()
    {
        CurrentHelperAmount = ShopManager.Instance.Helpers.Sum(x => x.AmountOwned);
    }
}