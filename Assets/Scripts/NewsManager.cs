using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    public TextMeshProUGUI NewsText;
    public GameObject Prefab;
    public GameObject PrefabParent;
    public GameObject ShopPanel;
    public Log[] Logs;
    private List<GameObject> _logGameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var log in Logs)
        {
            log.Displayed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (Monitor.PlayerLevel)
        {
            case 2:
                PlayNewsAndAddLog("Intro");
                break;
            case 4:
                PlayNewsAndAddLog("Noticed");
                break;
            case 7: 
                PlayNewsAndAddLog("HorsesOrigin");
                break;
            case 10:
                PlayNewsAndAddLog("PlayTest");
                break;
        }

        if (Monitor.TotalHorsesEarned >= 1000 && Monitor.TotalHorsesEarned < 10000)
        {
            PlayNewsAndAddLog("1,000");
        }
        else if (Monitor.TotalHorsesEarned >= 10000 && Monitor.TotalHorsesEarned < 50000)
        {
            PlayNewsAndAddLog("10,000");
        } else if (Monitor.TotalHorsesEarned >= 50000 && Monitor.TotalHorsesEarned < 100000)
        {
            PlayNewsAndAddLog("50,000");
        } else if (Monitor.TotalHorsesEarned >= 100000 && Monitor.TotalHorsesEarned < 500000)
        {
            PlayNewsAndAddLog("100,000");
        } else if (Monitor.TotalHorsesEarned >= 500000 && Monitor.TotalHorsesEarned < 1000000)
        {
            PlayNewsAndAddLog("500,000");
        } else if (Monitor.TotalHorsesEarned >= 1000000)
        {
            PlayNewsAndAddLog("1,000,000");
        } 

        if (ShopManager.Instance.Helpers[1].AmountOwned == 1 && !ShopPanel.activeSelf)
        {
            PlayNewsAndAddLog("Expansion");
        }

        if (IncrementButton.ClickerLevel == 1 && !ShopPanel.activeSelf)
        {
            PlayNewsAndAddLog("ButtonWorks");
        }
    }

    void PlayNewsAndAddLog(string logName)
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
        
        NewsText.GetComponent<TextMeshProUGUI>().text = log.Message;
        NewsText.gameObject.SetActive(true);
        log.Displayed = true;
        AddLog(log);
    }

    void AddLog(Log log)
    {
        //define and add the log
        var obj = Instantiate(Prefab);   
        obj.GetComponentInChildren<TextMeshProUGUI>().text = log.Message;
        obj.transform.SetParent(PrefabParent.transform, false);
        if (_logGameObjects.Any())
        {
            //move down the old logs
            foreach (var logGameObject in _logGameObjects)
            {
                logGameObject.transform.position = new Vector3(logGameObject.transform.position.x, logGameObject.transform.position.y - 220, 0);
            }
        }
        //extend the height of the content container
        if (_logGameObjects.Count >= 3)
        {
            var prefabHeight = obj.GetComponent<RectTransform>().rect.height;
            var prefabParentRect = PrefabParent.GetComponent<RectTransform>();
            prefabParentRect.sizeDelta = new Vector2(prefabParentRect.sizeDelta.x,prefabParentRect.sizeDelta.y + prefabHeight);
            
            //push up the logs after we extend the height.
            const int pushUp = 100;
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + pushUp, 0);
            foreach (var logGameObject in _logGameObjects)
            {
                logGameObject.transform.position = new Vector3(logGameObject.transform.position.x, logGameObject.transform.position.y + pushUp, 0);
            }
        }
        //add this new log to the list for the future
        log.DateDisplayed = DateTime.Now;
        _logGameObjects.Add(obj);
        
    }
}