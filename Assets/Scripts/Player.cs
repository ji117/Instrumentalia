using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private string playerName;
    private int trustOfMember1 = 50;
    private int trustOfMember2 = 50;
    private bool rightHanded;

    public bool lyra;
    public bool vex;
    public bool melodi;
    public bool sion;
    public bool echo;

    [SerializeField] private int reputation;

    [SerializeField] private float bgmVolume = 0.1f;
    [SerializeField] private float sfxVolume = 0.1f;

    void Start()
    {
        DontDestroyOnLoad(this);
        instance = this;
        reputation = 0; 
    }

    public string GetName()
    {
        return playerName;
    }

    public void SetName(string name)
    {
        playerName = name; 
    }

    public float GetBGMVolume()
    {
        return bgmVolume;
    }

    public void SetBGMVolume(float newVolume)
    {
        bgmVolume = newVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public void SetSFXVolume(float newVolume)
    {
        sfxVolume = newVolume; 
    }

    public int GetReputation()
    {
        return reputation;
    }

    public void AddReputation(int repToAdd)
    {
        reputation = reputation + repToAdd;
    }

    public bool IsRightHanded()
    {
        return rightHanded; 
    }

    public void SetHand(bool isRightHanded)
    {
        rightHanded = isRightHanded; 
    }
}
