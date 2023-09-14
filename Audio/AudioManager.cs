using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private static string during_sound = "Home";
    
    void Awake() {
        foreach(Sound sound in sounds) {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
        }

        
    }

    void Start() {
        PlaySound(during_sound);
    }

    public void PlaySound(string name) {
        during_sound = name;
        Sound snd = Array.Find(sounds, sound => sound.sound_name == name);
        if(snd == null) {
            return;
        }
        snd.audioSource.Play();
    }

    public void StopSound(string name) {
        Sound snd = Array.Find(sounds, sound => sound.sound_name == name);
        snd.audioSource.Stop();
    }

    public void DuringSound(string name) {
        during_sound = name;
    }
}
