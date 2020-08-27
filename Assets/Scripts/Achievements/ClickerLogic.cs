using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickerLogic : MonoBehaviour, IAchievement
{
    public Achievement AchievementObject; 
    public TextMeshProUGUI Title;
    public TextMeshProUGUI RewardDescription;
    public Slider ProgressBar;
    public Image Image;

    // Start is called before the first frame update
    void Start()
    {
        Title.text = AchievementObject.Title;
        Image.sprite = AchievementObject.Artwork;
        RewardDescription.text = AchievementObject.RewardDescription;
        ProgressBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTitle();
        UpdateProgressValue();
    }

    public void UpdateProgressValue()
    {
        ProgressBar.value = IncrementButton.ClickCount;
    }

    public void UpdateTitle()
    {
        Title.text = "Click the carrot button " + ProgressBar.maxValue + " times";
    }

    public void Receive()
    {
        Debug.Log("It worked");
        if (ProgressBar.value >= ProgressBar.maxValue)
        {
            IncrementButton.ClickerLevel++;
            ProgressBar.maxValue *= 2;
            ProgressBar.value = 0;
        }
        
    }
}
