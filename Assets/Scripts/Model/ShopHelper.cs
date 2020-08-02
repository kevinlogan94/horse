using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopHelper : MonoBehaviour
{
    public Helper Helper;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI CountText;
    
    // Start is called before the first frame update
    void Start()
    {
        NameText.text = Helper.Name;
        CostText.text = Helper.Cost.ToString();
        Helper.DynamicCost = Helper.Cost;
    }

    void Update()
    {
        CostText.text = Helper.DynamicCost.ToString();

        var newCount = "";
        switch (Helper.Name)
        {
            case "Farm":
                newCount = ShopManager.Farms.ToString();
                break;
            case "Feeder":
                newCount = ShopManager.Feeders.ToString();
                break;
            default: 
                //nothing
                break;
        }

        CountText.text = newCount;
    }
}
