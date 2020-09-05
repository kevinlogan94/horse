using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelperLogic : MonoBehaviour, IAchievement
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
        ProgressBar.maxValue = AchievementManager.Instance.HelperGoal;
        TriggerBarRefresh();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTitle();
        UpdateHelperCount();
    }
    
    public void Tutorial()
    {
        //show the tutorial but we also want to wait for them to finish the shop tutorial.
        PersonalPointer.SetActive(!AchievementManager.Instance.TutorialCompleted && ProgressBar.value >= ProgressBar.maxValue);
    }

    public void UpdateHelperCount()
    {
        ProgressBar.value = ShopManager.Instance.Helpers.Select(x => x.AmountOwned).Sum();
    }

    public void UpdateTitle()
    {
        Title.text = "Buy " + ProgressBar.maxValue + " Helpers";
    }

    public void Receive()
    {
        if (ProgressBar.value >= ProgressBar.maxValue)
        {
            ProgressBar.maxValue *= 2;
            AchievementManager.Instance.HelperGoal = ProgressBar.maxValue;
            foreach (var helper in ShopManager.Instance.Helpers)
            {
                helper.DynamicIncrement *= 2;
            }
            // Monitor.Instance.UpdatePassiveIncomeText();
            AchievementManager.Instance.TutorialCompleted = true;
            TriggerBarRefresh();
        }
    }
    
    public void TriggerBarRefresh()
    {
        //trigger bar change
        ProgressBar.value--;
        ProgressBar.value++;
    }
}
