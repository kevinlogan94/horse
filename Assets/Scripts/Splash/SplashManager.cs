using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class SplashManager : MonoBehaviour
{
    public GameObject SplashPanel;
    public GameObject AchievementPanel;
    public GameObject AdvertisementPanel;
    public GameObject InfluenceOverTimePanel;
    public GameObject BuffPanel;
    public GameObject LockAnimationObject;
    public GameObject CreaturePanel;
    public GameObject CreatureUIPanel;

    public Achievement[] Achievements;
    public Creature[] Creatures;
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

    public void TriggerSplash(string type, string objectName = null)
    {
        SplashPanel.SetActive(true);
        if (type == SplashType.Achievement.ToString())
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
        else if (type == SplashType.Creature.ToString())
        {
            CreaturePanel.SetActive(true);
            // I have an animation event at the end of this that turns on the horse panel
            var creature = Creatures.FirstOrDefault(x => x.Name == objectName);
            if (creature == null)
            {
                Debug.LogWarning("We couldn't find the creature with the name: " + objectName);
                return;
            }

            CreaturePanelScript.Instance.Creature = creature;
            LockAnimationObject.SetActive(true);
        }
        else if (type == SplashType.InfluenceOverTime.ToString())
        {
            InfluenceOverTimePanel.SetActive(true);
        }
        else if (type == SplashType.Buff.ToString())
        {
            BuffPanel.SetActive(true);
        }
        else
        {
            AdvertisementPanel.SetActive(true);
            if (Monitor.UseAnalytics)
            {
                AnalyticsEvent.AdOffer(true);
            }
        }
    }

    public void CloseSplash()
    {
        SplashPanel.SetActive(false);
        AchievementPanel.SetActive(false);
        CreaturePanel.SetActive(false);
        CreatureUIPanel.SetActive(false);
        AdvertisementPanel.SetActive(false);
        InfluenceOverTimePanel.SetActive(false);
        BuffPanel.SetActive(false);
        
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
    Creature,
    Achievement,
    Advertisement,
    InfluenceOverTime,
    Buff
}
