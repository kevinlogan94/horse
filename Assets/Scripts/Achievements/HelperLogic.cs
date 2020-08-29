using System.Linq;
using Assets.Scripts.Model;
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
    
    // Start is called before the first frame update
    void Start()
    {
        Title.text = AchievementObject.Title;
        Image.sprite = AchievementObject.Artwork;
        RewardDescription.text = AchievementObject.RewardDescription;
        ProgressBar.value = 0;
        ProgressBar.maxValue = AchievementManager.Instance.HelperGoal;
        triggerBarRefresh();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTitle();
        UpdateHelperCount();
    }

    public void UpdateHelperCount()
    {
        ProgressBar.value = ShopManager.Instance.Helpers.Select(x => x.AmountOwned).Sum();
    }

    public void UpdateTitle()
    {
        Title.text = "Purchase " + ProgressBar.maxValue + " Helpers";
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
            
            //trigger bar change
            ProgressBar.value = ProgressBar.value--;
            ProgressBar.value = ProgressBar.value++;
        }
    }
    
    public void triggerBarRefresh()
    {
        //trigger bar change
        ProgressBar.value = ProgressBar.value--;
        ProgressBar.value = ProgressBar.value++;
    }
}
