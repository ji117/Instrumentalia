using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private int trustOfMember1;
    private int trustOfMember2;
    [SerializeField] private int reputation;

    [SerializeField] private float bgmVolume = 0.1f;
    [SerializeField] private float sfxVolume = 0.1f;

    void Start()
    {
        DontDestroyOnLoad(this);
        instance = this;
        reputation = 0; 
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
}
