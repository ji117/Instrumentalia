using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu; 
    public GameObject enterPlayerName;
    public GameObject enterPlayerHand;
    public TMP_InputField inputField;
    public Slider sfxSlider;
    public Slider bgmSlider;

    public StudioEventEmitter eventEmitter;
    public StudioEventEmitter eventEmitterButton;
    public StudioEventEmitter eventEmitterBGM;

    public GameObject press; //press image
    public GameObject press1;
    public GameObject press2;

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
        if (!eventEmitterBGM.IsPlaying())
        {
            eventEmitterBGM.Play();
            eventEmitterBGM.EventInstance.setVolume(bgmVolume);
        }

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
        press1.SetActive(false);
        press2.SetActive(false);
    }

    public void Settings()
    {
        eventEmitterButton.Play();
        eventEmitterButton.EventInstance.setVolume(sfxVolume);

        press1.SetActive(true); //shows image press1 befoer menu disappears
        press.SetActive(false);
        press2.SetActive(false);

        mainMenu.SetActive(false); //menu disappears then settings appear 
        settingsMenu.SetActive(true);

        bgmSlider.value = Player.instance.GetBGMVolume();
        sfxSlider.value = Player.instance.GetSFXVolume();

    }

    public void Credits()
    {
        eventEmitterButton.Play();
        eventEmitterButton.EventInstance.setVolume(sfxVolume);

        press1.SetActive(true); //shows image press1 befoer menu disappears
        press.SetActive(false);
        press2.SetActive(false);

        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);

    }

    public void Quit()
    {
        press2.SetActive(true);
        press1.SetActive(false);
        press.SetActive(false);
        Application.Quit();
    }

    public void Back()
    {
        eventEmitterButton.Play();
        eventEmitterButton.EventInstance.setVolume(sfxVolume);

        if (settingsMenu.activeSelf)
            settingsMenu.SetActive(false);
        else
            creditsMenu.SetActive(false);

        mainMenu.SetActive(true);

        press.SetActive(false);
        press1.SetActive(false);
        press2.SetActive(false);
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

    public void EnterPlayerName()
    {
        enterPlayerName.SetActive(true); 
    }

    public void SubmitPlayerName()
    {
        Player.instance.SetName(inputField.text);
        enterPlayerName.SetActive(false);
    }

    public void ShowPlayerHandWindow()
    {
        enterPlayerHand.SetActive(true);
    }

    public void SetPlayerHand(bool rightHanded)
    {
        Player.instance.SetHand(rightHanded);
    }

    private void OnDestroy()
    {
        eventEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventEmitter.EventInstance.release();

        eventEmitterBGM.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventEmitterBGM.EventInstance.release();

        eventEmitterButton.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventEmitterButton.EventInstance.release();
    }
}
