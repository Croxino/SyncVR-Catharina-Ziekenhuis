﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioscript_eten : MonoBehaviour
{
    AudioSource introAudio;

    public AudioClip welkom;
    public AudioClip fosfaatB;
    public AudioClip fosfaatReden;
    public AudioClip bier;

    private bool stopcor = true;
    private bool stopcor2 = true;
    private bool stopcor3 = true;
    private bool stopcor4 = true;

    public bool pillsdestroyed;
    public bool bottledestroyed;
    public bool cheesedestroyed;
    public bool hotdogdestroyed;


    public GameObject fader;
    private GameObject anim;
    private GameObject animD;
    private Animator fadeout;
    private Animator animations;





    void Start()
    {
        introAudio = GetComponent<AudioSource>();
        //Debug.Log(baxter.GetComponent<MeshRenderer>().sharedMaterials[4]);

        anim = GameObject.Find("fader");
        animD = GameObject.Find("Baxter");
        animations = animD.GetComponent<Animator>();
        fadeout = anim.GetComponent<Animator>();

        fadeout.Play("FadeIn");



    }

    private void Update()
    {
        pillsdestroyed = DestroyThis.activePills;
        bottledestroyed = DestroyBottle.activeBottle;
        hotdogdestroyed = DestroyHotdog.activeHotdog;
        cheesedestroyed = DestroyCheese.activeCheese;

        StartCoroutine("PlayEten");
    }

    IEnumerator PlayEten()
    {
        if (introAudio.isPlaying)
        {
            animations.Play("talking");
        }
        else
        {
            animations.Play("Idle");
        }

        if (stopcor == true)
        {
            introAudio.PlayOneShot(welkom);

            stopcor = false;

        }

        if (!introAudio.isPlaying && stopcor2 == true)
        {
            introAudio.PlayOneShot(fosfaatB);
            stopcor2 = false;
        }

        if (!introAudio.isPlaying && pillsdestroyed == false && stopcor3 == true)
        {
            introAudio.PlayOneShot(fosfaatReden);
            stopcor3 = false;
        }

        if (!introAudio.isPlaying && hotdogdestroyed == false && cheesedestroyed == false && stopcor4 == true)
        {
            introAudio.PlayOneShot(bier);
            stopcor4 = false;
        }

        if (!introAudio.isPlaying && bottledestroyed == false)
        {
            fadeout.Play("FadeOut");
            yield return new WaitForSeconds(3.5f);
            SceneManager.LoadScene(9);
        }

        yield return null;
    }
}

   

