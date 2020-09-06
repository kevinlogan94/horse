using System.Linq;
using UnityEngine;

public class SplashManager : MonoBehaviour
{
    public GameObject SplashPanel;
    public GameObject AchievementPanel;
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

    public void TriggerSplash(string type, string scriptableObjectName)
    {
        SplashPanel.SetActive(true);
        if (type == "Achievement")
        {
            AchievementPanel.SetActive(true);
            var achievementObject = Achievements.FirstOrDefault(x => x.Name == scriptableObjectName);
            if (achievementObject == null)
            {
                Debug.LogWarning("We couldn't find the Achievement with the name: " + scriptableObjectName);
                return;
            }
            AchievementPanelScript.Instance.Achievement = achievementObject; 
        }
        else
        {
            HorsePanel.SetActive(true);
            // I have an animation event at the end of this that turns on the horse panel
            var horseObject = HorseObjects.FirstOrDefault(x => x.Name == scriptableObjectName);
            if (horseObject == null)
            {
                Debug.LogWarning("We couldn't find the horse object with the name: " + scriptableObjectName);
                return;
            }

            NewHorseScript.Instance.Horse = horseObject;
            LockAnimationObject.SetActive(true);
        }
    }

    public void CloseSplash()
    {
        SplashPanel.SetActive(false);
        AchievementPanel.SetActive(false);
        HorsePanel.SetActive(false);
        HorseUIPanel.SetActive(false);
        // AchievementUIPanel.SetActive(false);
    }
}
