using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomNavManager : MonoBehaviour
{
    public GameObject ShopPanel;
    public GameObject AchievementPanel;
    public GameObject ScenePanel;
    public GameObject SettingsPanel;
    public GameObject FingerPointerOutlook;

    public Button SettingsButton;
    public Button AchievementButton;
    public Button SceneButton;
    public Button OutlookButton;
    public Button ShopButton;

    public string ActiveView;
    
    private Sprite _basicImage;
    private Sprite _activeImage;

    private AudioManager _audioManager;
    
    #region Singleton
    public static BottomNavManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _basicImage = Resources.Load<Sprite>("Pixel/DefaultNavButton");
        _activeImage = Resources.Load<Sprite>("Pixel/ActiveNavButton");
        
        SelectView("outlook");
        SceneManager.Instance.CheckAndTriggerFirstChapter();
    }

    public void SelectView(string view)
    {
        TurnOffEverything();
        ActiveView = view;
        switch (view)
        {
            case "settings":
                SettingsPanel.SetActive(true);
                SettingsButton.image.sprite = _activeImage;
                _audioManager.Play("Pop");
                break;
            case "shop":
                ShopPanel.SetActive(true);
                ShopButton.image.sprite = _activeImage;
                _audioManager.Play("DoorBell");
                break;
            case "achievements":
                AchievementPanel.SetActive(true);
                AchievementButton.image.sprite = _activeImage;
                _audioManager.Play("Pop");
                break;
            case "scene":
                ScenePanel.SetActive(true);
                SceneButton.image.sprite = _activeImage;
                _audioManager.Play("Pop");
                break;
            default:
                OutlookButton.image.sprite = _activeImage;
                _audioManager.Play("Pop");
                break;
        }

        if (view == "outlook" && FingerPointerOutlook.activeSelf)
        {
            FingerPointerOutlook.SetActive(false);
        }
    }

    private void TurnOffEverything()
    {
        ShopPanel.SetActive(false);
        AchievementPanel.SetActive(false);
        ScenePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        SettingsButton.image.sprite = _basicImage;
        AchievementButton.image.sprite = _basicImage;
        SceneButton.image.sprite = _basicImage;
        OutlookButton.image.sprite = _basicImage;
        ShopButton.image.sprite = _basicImage;
    }
}