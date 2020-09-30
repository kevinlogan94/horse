using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChapterButton : MonoBehaviour
{
    public TextMeshProUGUI ChapterNameText;
    public TextMeshProUGUI ChapterNumberText;
    public Image Avatar;
    private Sprite _disabledImage;
    private Sprite _activeImage;
    private Sprite _lockedImage;
    private Sprite _portalImage;
    private Chapter _nextChapter;
    
    // Start is called before the first frame update
    void Start()
    {
        _disabledImage = Resources.Load<Sprite>("achiev_box_pressed");
        _activeImage = Resources.Load<Sprite>("achiev_box");
        _lockedImage = Resources.Load<Sprite>("lvl_lock_block");
        _portalImage = Resources.Load<Sprite>("Pixel/portal");
    }

    // Update is called once per frame
    void Update()
    {
        _nextChapter = SceneManager.Instance.Chapters.FirstOrDefault(chapter => !chapter.SceneViewed);
        UpdateButton();
    }

    public void TriggerChapter()
    {
        if (Monitor.PlayerLevel <= _nextChapter.LevelRequirement)
        {
            SceneManager.Instance.TriggerChapter(_nextChapter.Number);
        }
    }

    private void UpdateButton()
    {
        if (_nextChapter == null)
        {
            Debug.LogWarning("We couldn't find the next chapter to show");
            return;
        }
        ChapterNumberText.text = "Chapter " + _nextChapter.Number;
        ChapterNameText.text = _nextChapter.Name;

        if (Monitor.PlayerLevel > _nextChapter.LevelRequirement)
        {
            gameObject.GetComponent<Image>().sprite = _disabledImage;
            Avatar.sprite = _lockedImage;
        }
        else
        { 
            gameObject.GetComponent<Image>().sprite = _activeImage;
            Avatar.sprite = _portalImage;
        }
    }
}
