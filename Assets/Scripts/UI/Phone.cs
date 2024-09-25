using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using TMPro;

public class Phone : MonoBehaviour
{
    public GameObject contacts;
    public GameObject messages;
    public GameObject acceptenceMessage;
    public GameObject confirmWindow;

    public TextMeshProUGUI personBeingMessaged;
    public TextMeshProUGUI bandInvitation;
    public TextMeshProUGUI bandAcceptence;

    public StudioEventEmitter eventEmitter;

    private int members = 0;
    private bool isPlayerNull;
    private float delay = 5.0f;
    private string member;

    private void Start()
    {
        if (Player.instance == null)
        {
            isPlayerNull = true;
        }
        else
            isPlayerNull = false; 
    }

    private void Update()
    {
        if (messages.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                messages.SetActive(false);
                contacts.SetActive(true);
            }
        }

        if(members == 2)
        {
            //go to chapter 2 
        }

        if (delay <= 0 || Input.GetMouseButtonDown(0))
            acceptenceMessage.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (messages.activeSelf)
            delay = delay - 0.1f;
    }

    public void PickBandMember(string bandMember)
    {
        confirmWindow.SetActive(true);
        member = bandMember;
    }

    public void NoToBandMember()
    {
        confirmWindow.SetActive(false);
    }

    public void MessageBandMember()
    {
        confirmWindow.SetActive(false);
        delay = 8.0f;
        acceptenceMessage.SetActive(false);
        contacts.SetActive(false);
        messages.SetActive(true);

        eventEmitter.Play();
        if (isPlayerNull)
            eventEmitter.EventInstance.setVolume(0.1f);
        else
            eventEmitter.EventInstance.setVolume(Player.instance.GetSFXVolume());

        switch (member)
        {
            case "Lyra":
                personBeingMessaged.text = "-Lyra";
                bandInvitation.text = "Lyra, last night's experience was eye-opening.I'm assembling a band with a vision to break new ground, and I'd love for you to be a part of it.If you're ready to be part of something ambitious and groundbreaking, let's discuss.";
                bandAcceptence.text = "Now that's the kind of energy I'm into! I'm all about pushing limits and creating something unique.If you're serious about this band going places, then I'm definitely interested. Let's meet up and start brainstorming'I'm excited to see where this can go!";
                if (!isPlayerNull)
                    Player.instance.lyra = true;
                members++;
                break;

            case "Echo":
                personBeingMessaged.text = "-Echo";
                bandInvitation.text = "Hey Echo! Last night was a blast at the Vault. Let's turn that energy into something fun and permanent. I'm starting a band, and I think your vibes would be the perfect addition. Ready to make some cool music and have a great time ?";
                bandAcceptence.text = "Hey! I'm so glad you reached out'honestly, I've been itching to get into something fun like this. Your band idea sounds like exactly what I need right now! I'm totally in. When's our first jam session? Let's make some noise!";
                if (!isPlayerNull)
                    Player.instance.echo = true;
                members++;
                break;

            case "Vex":
                personBeingMessaged.text = "-Vex";
                bandInvitation.text = "Vex! I had such a great time at the Vault last night. I'm starting a band and would love for you to be a part of it. If you're down for some fun, experimental sessions, let's make it happen!";
                bandAcceptence.text = "Hey, I'm totally down! That sounds like exactly the kind of laid-back fun I've been craving. Music's meant to be enjoyed, right? Let's get together and see what kind of magic we can make. I'm ready when you are!";
                if (!isPlayerNull)
                    Player.instance.vex = true;
                members++;
                break;

            case "Sion":
                personBeingMessaged.text = "-Sion";
                bandInvitation.text = "Sion, the vibe at Vibe Vault got me thinking big. I'm looking to build a band with real potential, and I see you as a key player. If you're ready to push boundaries and make a statement, let's talk about joining forces.";
                bandAcceptence.text = "Now this is what I'm talking about! I'm all in for making some serious waves. If you're aiming high, I'm with you. Let's build something that stands out'ready to start planning whenever you are. Let's do this!";
                if (!isPlayerNull)
                    Player.instance.sion = true;
                members++;
                break;

            case "Melodi":
                personBeingMessaged.text = "-Melodi";
                bandInvitation.text = "Melodi, last night was great. I'm thinking of starting a band that's more about chilling and making music for the love of it.If you're down for a relaxed, creative space, I'd love for you to be part of it.";
                bandAcceptence.text = "Hey! That sounds perfect for me. I love the idea of just enjoying the music without all the stress. Count me in I'm excited to be part of something relaxed and creative. Let's set up a time to jam and see where it takes us!";
                if (!isPlayerNull)
                    Player.instance.melodi = true;
                members++; 
                break;
        }
    }
}
