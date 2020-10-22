using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class AchievementPanelScript : MonoBehaviour
{
    public Achievement Achievement;
    public Image Image;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI BeforeText;
    public TextMeshProUGUI AfterText;
    
    #region Singleton
    public static AchievementPanelScript Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    // Start is called before the first frame update
    void Update()
    {
        Description.text = Achievement.RewardDescription;
        Image.sprite = Achievement.Artwork;
        DefineBeforeAndAfterText();
    }

    private void DefineBeforeAndAfterText()
    {
        const int tenHoursInSeconds = 36000;
        const int hourInSeconds = 3600;
        
        //by the time we hit this part, we have already received the bonus. Keep that in mind.
        switch (Achievement.Name)
        {
            case "Helper":
                BeforeText.text = Monitor.Instance.GetHelperPassiveIncome() / 2 + "/sec";
                AfterText.text = Monitor.Instance.GetHelperPassiveIncome() + "/sec";
                break;
            case "Clicker":
                BeforeText.text = IncrementPanel.GetClickerIncrement(IncrementPanel.ClickerIncrement / ClickerLogic.ClickerIncrease, 1) + "/click";
                AfterText.text = IncrementPanel.GetClickerIncrement(IncrementPanel.ClickerIncrement, 1) + "/click";
                break;
            case "Xal":
                BeforeText.text = IncrementPanel.GetClickerIncrement(IncrementPanel.ClickerIncrement / SceneManager.ClickerIncrease, 1) + "/click";
                AfterText.text = IncrementPanel.GetClickerIncrement(IncrementPanel.ClickerIncrement, 1) + "/click";
                break;
            case "Video": case "Story":
                BeforeText.text = Monitor.Influence - Monitor.Instance.GetInfluenceReceivedOverTime(tenHoursInSeconds) + " influence"; // 10 hours
                AfterText.text = Monitor.Influence + " influence";
                break;
            default:
                BeforeText.text = Monitor.Influence - Monitor.Instance.GetInfluenceReceivedOverTime(hourInSeconds) + " influence"; // 1 hour
                AfterText.text = Monitor.Influence + " influence";
                break;
        }
    }

}
