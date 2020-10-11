using System.Linq;
using UnityEngine;

public class SplashManager : MonoBehaviour
{
    public GameObject SplashPanel;
    public GameObject AchievementPanel;
    public GameObject AdvertisementPanel;
    public GameObject LockAnimationObject;
    public GameObject HorsePanel;
    public GameObject HorseUIPanel;

    public Achievement[] Achievements;
    public HorseObject[] HorseObjects;
    public static SplashManager Instance;

    #region Singleton
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerSplash(string type, string objectName)
    {
        SplashPanel.SetActive(true);
        if (type == "Achievement")
        {
            AchievementPanel.SetActive(true);
            var achievementObject = Achievements.FirstOrDefault(x => x.Name == objectName);
            if (achievementObject == null)
            {
                Debug.LogWarning("We couldn't find the Achievement with the name: " + objectName);
                return;
            }
            AchievementPanelScript.Instance.Achievement = achievementObject; 
        }
        else if (type == "Horse")
        {
            HorsePanel.SetActive(true);
            // I have an animation event at the end of this that turns on the horse panel
            var horseObject = HorseObjects.FirstOrDefault(x => x.Name == objectName);
            if (horseObject == null)
            {
                Debug.LogWarning("We couldn't find the horse object with the name: " + objectName);
                return;
            }

            NewHorseScript.Instance.Horse = horseObject;
            LockAnimationObject.SetActive(true);
        }
        else
        {
            AdvertisementPanel.SetActive(true);
        }
    }

    public void CloseSplash()
    {
        SplashPanel.SetActive(false);
        AchievementPanel.SetActive(false);
        HorsePanel.SetActive(false);
        HorseUIPanel.SetActive(false);
        AdvertisementPanel.SetActive(false);
        
        // show ad after speaking to Xal
        if (AchievementPanelScript.Instance != null 
            && AchievementPanelScript.Instance.Achievement.Name == "Xal")
        {
            AdvertisementManager.Instance.ShowSkippableAd();
        }
        // AchievementUIPanel.SetActive(false);
    }
}

public enum SplashType
{
    Horse,
    Achievement,
    Advertisement
}
