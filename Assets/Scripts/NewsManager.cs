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
        if (Monitor.Horses == 1 && !Logs[0].Displayed)
        {
            PlayNewsAndAddLog("Intro");
        }
        if (Monitor.Horses == 2 && !Logs[1].Displayed)
        {
            PlayNewsAndAddLog("Welcome");
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
                logGameObject.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 140, 0);
            }
        }
        //extend the height of the content container
        if (_logGameObjects.Count > 4)
        {
            var prefabHeight = obj.GetComponent<RectTransform>().rect.height;
            var prefabParentRect = PrefabParent.GetComponent<RectTransform>();
            prefabParentRect.sizeDelta = new Vector2(prefabParentRect.sizeDelta.x,prefabParentRect.sizeDelta.y + prefabHeight + 50);
        }
        //add this new log to the list for the future
        log.DateDisplayed = DateTime.Now;
        _logGameObjects.Add(obj);
        
    }
}