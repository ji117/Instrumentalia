using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public Slider sfxSlider;
    public Slider bgmSlider;

    public StudioEventEmitter eventEmitter;
    public StudioEventEmitter eventEmitterButton;
    public StudioEventEmitter eventEmitterBGM;

    public GameObject press; //press image


    private float sfxVolume = 0.1f;
    private float bgmVolume = 0.1f;

    private void Start()
    {
        eventEmitterBGM.Play();
        eventEmitterBGM.EventInstance.setVolume(bgmVolume);

        press.SetActive(false);

    }

    void Update()
    {
        if (Input.anyKeyDown && titleScreen.activeSelf)
        {
            titleScreen.SetActive(false);
            mainMenu.SetActive(true);
            eventEmitter.Play();
            eventEmitter.EventInstance.setVolume(sfxVolume);
            eventEmitter.TriggerOnce = true; 
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back(); //ESC mainmenu
        }

    }

    
    
    
    public void Play()
    {
        eventEmitterButton.Play();
        eventEmitterButton.EventInstance.setVolume(sfxVolume);

        press.SetActive(true);
    }

    public void Settings()
    {
        eventEmitterButton.Play();
        eventEmitterButton.EventInstance.setVolume(sfxVolume);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        bgmSlider.value = Player.instance.GetBGMVolume();
        sfxSlider.value = Player.instance.GetSFXVolume();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        eventEmitterButton.Play();
        eventEmitterButton.EventInstance.setVolume(sfxVolume);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);

        press.SetActive(false);
    }

    public void SFXChange()
    {
        sfxVolume = sfxSlider.value;
        eventEmitterButton.EventInstance.setVolume(sfxVolume);
        Player.instance.SetSFXVolume(sfxVolume);
        if (!eventEmitterButton.IsPlaying())
        {
            eventEmitterButton.Play();
            eventEmitterButton.EventInstance.setVolume(sfxVolume);
        }
    }

    public void BGMChange()
    {
        bgmVolume = bgmSlider.value;
        eventEmitterBGM.EventInstance.setVolume(bgmVolume);
        Player.instance.SetBGMVolume(bgmVolume);
    }

    private void OnDestroy()
    {
        
    }
}
