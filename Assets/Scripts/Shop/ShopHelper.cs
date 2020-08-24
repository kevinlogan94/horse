using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class ShopHelper : MonoBehaviour
{
    public Helper Helper;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI CountText;
    public TextMeshProUGUI PerSecondIncreaseText;
    public Button HelperButton;
    public Image Avatar;
    private Sprite _disabledImage;
    private Sprite _activeImage;
    private Sprite _lockedImage;

    void Awake()
    {
        _disabledImage = Resources.Load<Sprite>("achiev_box_pressed");
        _activeImage = Resources.Load<Sprite>("achiev_box");
        _lockedImage = Resources.Load<Sprite>("lvl_lock_block");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        NameText.text = Helper.Name;
        CostText.text = String.Format("{0:n0}", Helper.Cost);
        PerSecondIncreaseText.text = Helper.Increment + " p/s";
        Helper.DynamicCost = Helper.Cost;
    }

    void Update()
    {
        if (Helper.LevelRequirement > Monitor.PlayerLevel)
        {
            HelperButton.image.sprite = _disabledImage;
            Avatar.sprite = _lockedImage;
            HelperButton.interactable = false;
            CountText.text = "Lvl " + Helper.LevelRequirement;
            CountText.fontSize = 18;
            return;
        }

        Avatar.sprite = Helper.Artwork;
        HelperButton.image.sprite = _activeImage;
        HelperButton.interactable = true;
        CountText.fontSize = 36;
        
        CostText.text = String.Format("{0:n0}", Helper.DynamicCost);

        var newCount = "0";
        var resultHelper = ShopManager.Instance.Helpers.FirstOrDefault(x => x.Name == Helper.Name);
        if (resultHelper != null)
        {
            newCount = resultHelper.AmountOwned.ToString();
        }
        else
        {
            Debug.LogWarning("We couldn't find the helper: " + Helper.Name);
        }

        CountText.text = newCount;
    }
}