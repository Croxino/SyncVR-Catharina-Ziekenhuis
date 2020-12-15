using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startQuiz : MonoBehaviour
{
    AudioSource introAudio;

    //public GameObject pills;
    public AudioClip intro;
    private bool stopcor = true;
    
    public GameObject quizboard;


    public GameObject fader;
    private GameObject anim;
    private Animator fadeout;



    // Start is called before the first frame update
    void Start()
    {
        introAudio = GetComponent<AudioSource>();

        anim = GameObject.Find("fader");
        fadeout = anim.GetComponent<Animator>();
  
        fadeout.Play("FadeIn");



    }

    private void Update()
    {
 

        StartCoroutine("playStart");
      

    }



    IEnumerator playStart()
    {
        yield return new WaitForSeconds(2.0f);
        if (stopcor == true)
        {

            introAudio.PlayOneShot(intro);
            stopcor = false;

        }
        if (!introAudio.isPlaying)
        {
            yield return new WaitForSeconds(2.0f);
            quizboard.SetActive(true);
        }

        yield return null;

    }






}
