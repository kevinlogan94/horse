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
        //by the time we hit this part, we have already received the bonus. Keep that in mind.
        switch (Achievement.Name)
        {
            case "Helper":
                BeforeText.text = Monitor.Instance.GetHelperPassiveIncome() / 2 + "/sec";
                AfterText.text = Monitor.Instance.GetHelperPassiveIncome() + "/sec";
                break;
            case "Clicker":
                BeforeText.text = IncrementButton.GetIncrement(IncrementButton.ClickerLevel - 1, 1) + "/click";
                AfterText.text = IncrementButton.GetIncrement(IncrementButton.ClickerLevel, 1) + "/click";
                break;
            default:
                BeforeText.text = Monitor.Horses - Monitor.Instance.GetHorseReceivedOverTime(3600) + " horses"; // 1 hour
                AfterText.text = Monitor.Horses + " horses";
                break;
        }
    }

}
