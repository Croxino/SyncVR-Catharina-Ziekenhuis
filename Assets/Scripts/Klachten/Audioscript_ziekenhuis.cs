﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audioscript_ziekenhuis : MonoBehaviour
{
    AudioSource introAudio;


    public AudioClip dizzy;
    private bool stopcor = true;


    public GameObject fader;
    private GameObject anim;
    private GameObject animD;
    private GameObject animC;
    private Animator fadeout;
    private Animator dizziness;
    private Animator animations;



    // Start is called before the first frame update
    void Start()
    {
        introAudio = GetComponent<AudioSource>();
        //Debug.Log(baxter.GetComponent<MeshRenderer>().sharedMaterials[4]);

        anim = GameObject.Find("fader");
        animD = GameObject.Find("Pvr_UnitySDK");
        fadeout = anim.GetComponent<Animator>();
        dizziness = animD.GetComponent<Animator>();
        fadeout.Play("FadeIn");

        animC = GameObject.Find("Baxter");
        animations = animC.GetComponent<Animator>();


    }

    private void Update()
    {
       
        StartCoroutine("PlayZiekenhuis");
     }

 

    IEnumerator PlayZiekenhuis()
    {

        if (stopcor == true)
        {
            introAudio.PlayOneShot(dizzy);
            stopcor = false;
            animations.Play("talking");

            yield return new WaitForSeconds(7.0f);
            dizziness.Play("dizziness");
            yield return new WaitForSeconds(3.0f);
            dizziness.Play("New State");


        }

        if (!introAudio.isPlaying)
        {
            fadeout.Play("FadeOut");
            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadScene(3);
        }
        yield return null;

    }







    



}