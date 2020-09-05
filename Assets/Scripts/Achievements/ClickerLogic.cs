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
    public GameObject PersonalPointer;

    // Start is called before the first frame update
    void Start()
    {
        Title.text = AchievementObject.Title;
        Image.sprite = AchievementObject.Artwork;
        RewardDescription.text = AchievementObject.RewardDescription;
        ProgressBar.value = 0;
        ProgressBar.maxValue = AchievementManager.Instance.ClickerGoal;
        TriggerBarRefresh();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTitle();
        UpdateProgressValue();
        Tutorial();
    }

    public void UpdateProgressValue()
    {
        ProgressBar.value = IncrementButton.ClickCount;
    }

    public void UpdateTitle()
    {
        Title.text = "Click the carrot " + ProgressBar.maxValue + " times";
    }
    
    public void Tutorial()
    {
        //show the tutorial but we also want to wait for them to finish the shop tutorial.
        PersonalPointer.SetActive(!AchievementManager.Instance.TutorialCompleted && ProgressBar.value >= ProgressBar.maxValue);
    }

    public void Receive()
    {
        if (ProgressBar.value >= ProgressBar.maxValue)
        {
            IncrementButton.ClickerLevel++;
            ProgressBar.maxValue *= 2;
            AchievementManager.Instance.TutorialCompleted = true;
            AchievementManager.Instance.ClickerGoal = ProgressBar.maxValue;
            
            //update horses
            var coreHorse = ShopManager.Instance.Helpers[IncrementButton.ClickerLevel].HorseBreed;
            var secondaryHorse = ShopManager.Instance.Helpers[IncrementButton.ClickerLevel + 1].HorseBreed;
            ObjectPooler.Instance.ReOptimizeHorsePools(coreHorse, secondaryHorse);
            TriggerBarRefresh();
            SplashManager.Instance.TriggerSplash(SplashType.Achievement.ToString(), AchievementObject.Name);
        }
    }

    public void TriggerBarRefresh()
    {
        //trigger bar change
        ProgressBar.value--;
        ProgressBar.value++;
    }
}
