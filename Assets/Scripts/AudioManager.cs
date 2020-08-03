using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    
    void Awake()
    {
        foreach (var sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Play("Theme");
    }
    
    // FindObjectOfType<AudioManager>().Play();
    public void Play(string songName, float? pitch = null)
    {
        var sound = Array.Find(Sounds, s => s.Name == songName);
        if (sound != null)
        {
            if (pitch>0)
            {
                sound.Source.pitch = (float) pitch;
            }
            sound.Source.Play();   
        }
        else
        {
            Debug.LogWarning("We couldn't find this sound to play: " + songName);
        }
    }

    public void PlaySong(string songName)
    {
        Play(songName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
