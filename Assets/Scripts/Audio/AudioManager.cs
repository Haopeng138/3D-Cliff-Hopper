using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake() {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    private void Start() {
        PlayMusic("Background");
    }

    public void PlayMusic(string name){
        Sound s = Array.Find(musicSounds, sound => sound.name == name);

        if (s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
           
        }else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name){
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);

        if (s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
          
        }else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic(){
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX(){
        sfxSource.mute = !sfxSource.mute;
    }

    public void ToggleMusicVolume(float volume){
        musicSource.volume = volume;
    }

    public void ToggleSFXVolume(float volume){
        sfxSource.volume = volume;
    }


}
