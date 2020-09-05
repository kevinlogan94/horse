using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    public GameObject SplashPanel;
    public GameObject RevealPanel;
    public GameObject AchievementPanel;
    public GameObject HorsePanel;

    public Achievement[] Achievements;
    public Helper[] Helpers;
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
        RevealPanel.SetActive(true);
        if (type == "Achievement")
        {
            AchievementPanel.SetActive(true);
            // AchievementPanelScript.UpdateAchievementObject(scriptableObjectName);
        }
        else
        {
            HorsePanel.SetActive(true);
        }
    }
}
