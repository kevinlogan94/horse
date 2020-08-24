using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgrade : MonoBehaviour
{
    public Upgrade Upgrade;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI CountText;
    public TextMeshProUGUI PerSecondIncreaseText;
    public Button UpgradeButton;
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
        NameText.text = Upgrade.Name;
        CostText.text = String.Format("{0:n0}", Upgrade.Cost);
        if (Upgrade.Name == "Clicker")
        {
            Debug.Log("here");
            PerSecondIncreaseText.text = Upgrade.Level > 0 ? (Upgrade.Level * 15) + " p/c" : 15 + " p/c";   
        }
        Upgrade.DynamicCost = Upgrade.Cost;
    }

    // Update is called once per frame
    void Update()
    {
        if (Upgrade.LevelRequirement > Monitor.PlayerLevel)
        {
            UpgradeButton.image.sprite = _disabledImage;
            Avatar.sprite = _lockedImage;
            UpgradeButton.interactable = false;
            CountText.text = "Lvl " + Upgrade.LevelRequirement;
            CountText.fontSize = 18;
            return;
        }
        
        Avatar.sprite = Upgrade.Artwork;
        UpgradeButton.image.sprite = _activeImage;
        UpgradeButton.interactable = true;
        CountText.fontSize = 36;
        
        CostText.text = String.Format("{0:n0}", Upgrade.DynamicCost);

        var newCount = "0";
        var resultUpgrade = ShopManager.Instance.Upgrades.FirstOrDefault(x => x.Name == Upgrade.Name);
        if (resultUpgrade != null)
        {
            newCount = resultUpgrade.Level.ToString();
        }
        else
        {
            Debug.LogWarning("We couldn't find the upgrade: " + Upgrade.Name);
        }

        CountText.text = newCount;
    }
}
