using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public List<String> BackgroundMusic = new List<string>
    {
        "Xals Theme",
        "Barlogs Theme",
        "Mountains",
        "River",
        "Meadow"
    };
    
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
        Play("Xals Theme");
    }
    
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

    public void MuteBackgroundMusic(bool mute)
    {
        foreach (var backgroundMusic in BackgroundMusic)
        {
            var theme = Array.Find(Sounds, sound => sound.Name == backgroundMusic);
            theme.Source.mute = !mute;
        }
    }
    
    public void MuteSoundEffects(bool mute)
    {
        var soundEffects = Sounds.ToList();
        foreach (var backgroundMusic in BackgroundMusic)
        {
            foreach (var sound in soundEffects)
            {
                if (sound.Name == backgroundMusic)
                {
                    soundEffects.Remove(sound);
                }
            }
        }
        foreach (var soundEffectSound in soundEffects)
        {
            soundEffectSound.Source.mute = !mute;
        }
    }
}
