using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsText : MonoBehaviour
{
    private AudioManager _audioManager;
    
    public void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }
    
    public void DisableActiveState()
    {
        gameObject.SetActive(false);
    }

    public void PlayIntro()
    {
        _audioManager.Play("LevelUp2");
    }

    public void PlayOutro()
    {
        _audioManager.Play("Whoosh");
    }
}
