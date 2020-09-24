using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    public TextMeshProUGUI NewsText;
    public GameObject ShopPanel;
    public Log[] Logs;
    
    #region Singleton
    public static NewsManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (Monitor.PlayerLevel)
        {
            case 2:
                PlayNews("Intro");
                break;
            case 4:
                PlayNews("Noticed");
                break;
            case 7: 
                PlayNews("HorsesOrigin");
                break;
            case 10:
                PlayNews("PlayTest");
                break;
        }

        if (Monitor.TotalInfluenceEarned >= 1000 && Monitor.TotalInfluenceEarned < 10000)
        {
            PlayNews("1,000");
        }
        else if (Monitor.TotalInfluenceEarned >= 10000 && Monitor.TotalInfluenceEarned < 50000)
        {
            PlayNews("10,000");
        } else if (Monitor.TotalInfluenceEarned >= 50000 && Monitor.TotalInfluenceEarned < 100000)
        {
            PlayNews("50,000");
        } else if (Monitor.TotalInfluenceEarned >= 100000 && Monitor.TotalInfluenceEarned < 500000)
        {
            PlayNews("100,000");
        } else if (Monitor.TotalInfluenceEarned >= 500000 && Monitor.TotalInfluenceEarned < 1000000)
        {
            PlayNews("500,000");
        } else if (Monitor.TotalInfluenceEarned >= 1000000)
        {
            PlayNews("1,000,000");
        } 

        if (ShopManager.Instance.Helpers[1].AmountOwned == 1 && !ShopPanel.activeSelf)
        {
            PlayNews("Expansion");
        }

        if (IncrementButton.ClickerLevel == 1 && !ShopPanel.activeSelf)
        {
            PlayNews("ButtonWorks");
        }
    }

    public void PlayNews(string logName)
    {
        var log = Logs.FirstOrDefault(x => x.Name == logName);
        if (log == null)
        {
            Debug.LogWarning("We couldn't find the log: " + logName);
            return;
        }
        if (log.Displayed)
            return;
        if (NewsText.IsActive())
            return;
        if (BottomNavManager.Instance.ActiveView != "outlook")
            return;
        
        NewsText.GetComponent<TextMeshProUGUI>().text = log.Message;
        NewsText.gameObject.SetActive(true);
        log.Displayed = true;
    }
}