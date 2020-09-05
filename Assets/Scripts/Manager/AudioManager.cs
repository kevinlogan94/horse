using UnityEngine.Audio;
using System;
using System.Linq;
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
        var theme = Array.Find(Sounds, sound => sound.Name == "Theme");
        theme.Source.mute = !mute;
    }

    public void MuteSoundEffects(bool mute)
    {
        var soundEffectSounds = Sounds.Where(x => x.Name != "Theme");
        foreach (var soundEffectSound in soundEffectSounds)
        {
            soundEffectSound.Source.mute = !mute;
        }
    }
}
