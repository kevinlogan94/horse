using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public Chapter[] Chapters;
    public GameObject TextBox;

    public string[] Banter;
    private int _banterIndex = 0;
    private readonly float _banterWaitTime = 5f;
    private float _currentBanterWaitTime = 5f;
    private bool _banterActive = false;

    public string[] Tutorial;
    public GameObject ScenePanel;

    private int _chapterIndex;
    private int _activeChapter;

    public bool TutorialActive;
    private int _tutorialIndex;
    
    #region Singleton
    public static SceneManager Instance;

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
        DisableBanterAfterNoInteraction();
        
        //progress in the tutorial after player purchases from the shop.
        if (_tutorialIndex == 2 && ShopManager.Instance.Helpers[0].AmountOwned >= 1 && ScenePanel.activeSelf)
        {
            TriggerTutorial();
        }
    }

    public void TriggerChat()
    {
        if (TutorialActive)
        {
            TriggerTutorial();
        }
        else if (_activeChapter == 0)
        {
            TriggerBanter();
        }
        else
        {
            TriggerChapter(_activeChapter);
        }
    }

    private void TriggerChapter(int chapterNumber)
    {
        var chapter = Chapters.FirstOrDefault(x => x.Number == chapterNumber);
        if (chapter == null)
        {
            Debug.LogWarning("We couldn't find Chapter " + chapterNumber);
            return;   
        }

        _activeChapter = chapterNumber;
        _banterActive = false;
        TextBox.SetActive(true);
        var textMeshPro = TextBox.GetComponentInChildren<TextMeshProUGUI>();

        textMeshPro.text = chapter.Quotes[_chapterIndex];
        
        if (_chapterIndex < chapter.Quotes.Length - 1)
        {
            _chapterIndex++;   
        }
        else
        {
            _chapterIndex = 0;
            _activeChapter = 0;
            chapter.SceneViewed = true;
            TextBox.SetActive(false);
            
            if (chapterNumber == 1)
            {
                TriggerTutorial();
            }
        }
    }

    private void TriggerTutorial()
    {
        TutorialActive = true;
        if (_tutorialIndex < Tutorial.Length - 1)
        {
            var textMeshPro = TextBox.GetComponentInChildren<TextMeshProUGUI>();
            switch (_tutorialIndex)
            {
                // Set up user to to go to shop
                case 1 when ShopManager.Instance.Helpers[0].AmountOwned == 0:
                {
                    TextBox.SetActive(true);
                    textMeshPro.text = Tutorial[_tutorialIndex];
                    _tutorialIndex++;
                
                    // Trigger the shop tutorial
                    Monitor.Influence = ShopManager.Instance.Helpers[0].DynamicCost;
                    break;
                }
                case 2 when ShopManager.Instance.Helpers[0].AmountOwned == 0:
                {
                    break;
                }
                default:
                    TextBox.SetActive(true);
                    textMeshPro.text = Tutorial[_tutorialIndex];
                    _tutorialIndex++;
                    break;
            }
        }
        else
        {
            Monitor.Instance.TriggerOutlookTutorial();
            _tutorialIndex = 0;
            TutorialActive = false;
            TextBox.SetActive(false);
        }
    }

    private void TriggerBanter()
    {
        _currentBanterWaitTime = Time.time + _banterWaitTime;
        _banterActive = true;
        TextBox.SetActive(true);
        var textMeshPro = TextBox.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = Banter[_banterIndex];

        if (_banterIndex < Banter.Length - 1)
        {
            _banterIndex++;   
        }
        else
        {
            _banterIndex = 0;
        }
    }

    private void DisableBanterAfterNoInteraction()
    {
        if (_banterActive && TextBox.activeSelf && Time.time > _currentBanterWaitTime)
        {
            _banterActive = false;
            TextBox.SetActive(false);
        }
    }

    public void CheckAndTriggerFirstChapter()
    {
        var chapter1 = Chapters.FirstOrDefault(chapter => chapter.Number == 1);
        if (chapter1 == null)
        {
            Debug.LogWarning("We could find chapter 1.");
            return;
        }
        if (!chapter1.SceneViewed)
        {
            BottomNavManager.Instance.SelectView("scene");
            TriggerChapter(1);
        }
    }
}
