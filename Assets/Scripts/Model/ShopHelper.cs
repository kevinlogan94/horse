using System.Collections;
using System.Collections.Generic;
using System.Linq;
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