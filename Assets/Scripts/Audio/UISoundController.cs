using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    public void ToggleMusic(){
        AudioManager.instance.ToggleMusic();
    }
    public void ToggleSFX(){
        AudioManager.instance.ToggleSFX();
    }
    public void ToggleMusicVolume(){
        AudioManager.instance.ToggleMusicVolume(musicSlider.value);
    }
    public void ToggleSFXVolume(){
        AudioManager.instance.ToggleSFXVolume(sfxSlider.value);
    }
}
