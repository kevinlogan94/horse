using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Feeder : MonoBehaviour
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
    }

    void Update()
    {
        CostText.text = Helper.Cost.ToString();
        CountText.text = Monitor.Feeders.ToString();
    }
}
