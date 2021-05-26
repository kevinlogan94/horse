using System;
using System.Collections;
using Unity.Notifications.iOS;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{

    private string _deviceToken;
    private bool _notificationPermissionGranted;
    
    #region Singleton
    public static NotificationManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            //Android notification logic  
        }
        else
        {
            StartCoroutine(RequestAuthorizationAndTriggerNotification());  
        }
    }

    // Update is called once per frame
    void Update()
    {
        // GenerateTimedNotification();
    }

    #region iOS
    
    public void GenerateTimedNotification()
    {
        if (!_notificationPermissionGranted) return;
        
        var scheduledNotifications = iOSNotificationCenter.GetScheduledNotifications();
        if (scheduledNotifications.Length > 0)
        {
            Debug.Log($"Removing Notification: {scheduledNotifications[0].Title}");
            iOSNotificationCenter.RemoveScheduledNotification(scheduledNotifications[0].Identifier);
            Debug.Log("Notification Removed");
        }
        
        var timeTrigger = new iOSNotificationTimeIntervalTrigger
        {
            TimeInterval = new TimeSpan(10, 0, 0),
            Repeats = false
        };

        var notification = new iOSNotification
        {
            Title = "Hey, Stranger!",
            Body = "You have earned a lot of influence while you've been gone! Don't forget to come back and buy more tomes!",
            ShowInForeground = false, // Don't make it prompt during gameplay
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger
        };

        iOSNotificationCenter.ScheduleNotification(notification);
        Debug.Log("Timed Notification Scheduled");
    }

    
    IEnumerator RequestAuthorizationAndTriggerNotification()
    {
        Debug.Log("Request permission for notifications");
        using (var req = new AuthorizationRequest(AuthorizationOption.Alert | AuthorizationOption.Badge, true))
        {
            yield return new WaitForSeconds(5);
            while (!req.IsFinished)
            {
                yield return null;
            }
        
            var res = "\n RequestAuthorization: \n";
            res += "\n finished: " + req.IsFinished;
            res += "\n granted :  " + req.Granted;
            res += "\n error:  " + req.Error;
            res += "\n deviceToken:  " + req.DeviceToken;
            Debug.Log(res);
        
            //store the device token to be used for sending later notifications.
            if (String.IsNullOrEmpty(req.DeviceToken))
            {
                _deviceToken = req.DeviceToken;
                _notificationPermissionGranted = req.Granted;
                GenerateTimedNotification();
            }
        }
    }
    #endregion

    #region Android

    

    #endregion
}
