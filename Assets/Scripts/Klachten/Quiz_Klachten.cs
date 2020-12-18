using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz_Klachten : MonoBehaviour
{

    AudioSource mainAudio;

    public Renderer quizBoard;
    public Texture tabletten;
    public Texture misselijk;
    public Texture laxeermiddelen;
    public Texture ontlasting;

    public bool question1 = false;
    public bool question2 = false;
    public bool question3 = false;
    public bool question4 = false;
    private bool echo = true;
    private bool echo1 = true;
    private bool echo2 = true;
    private bool echo3 = true;
    private bool audioecho = true;
    private bool audioecho1 = true;
    private bool audioecho2 = true;
    private bool audioecho3 = true;
    private bool audioecho4 = true;
    public bool mainaudioecho;
    private bool stopcor = true;

    public AudioClip intro;
    public int receiveAnswer;

    public AudioClip tablettenQuiz;
    public AudioClip goedtabletten;
    public AudioClip laxeerQuiz;
    public AudioClip goedlaxeer;
    public AudioClip ontlastingQuiz;
    public AudioClip goedontlasting;
    public AudioClip misselijkQuiz;
    public AudioClip goedmisselijk;
    public AudioClip audienceClap;
    public AudioClip eindemodule;
    public GameObject Confetti;
    public GameObject quizboard;
    private GameObject animD;



    public GameObject fader;
    private GameObject anim;
    private Animator fadeout;
    private Animator animations;


    // Start is called before the first frame update
    void Start()
    {
        mainAudio = GetComponent<AudioSource>();

        anim = GameObject.Find("fader");
        fadeout = anim.GetComponent<Animator>();

        animD = GameObject.Find("Baxter");
        animations = animD.GetComponent<Animator>();
        fadeout.Play("FadeIn");
    }

    private void Update()
    {

        receiveAnswer = Pvr_ControllerDemo.answerholder;
        StartCoroutine("playStart");
        StartCoroutine("Quiz");
    }



    IEnumerator playStart()
    {
        if (!mainAudio.isPlaying)
        {
            animations.Play("Idle");
        }


        if (stopcor == true)
        {

            mainAudio.PlayOneShot(intro);
            stopcor = false;
            animations.Play("talking");
            yield return new WaitForSeconds(6.0f);
            quizboard.SetActive(true);


        }


        if (quizboard.activeSelf == true && echo == true)
        {
            mainAudio.PlayOneShot(tablettenQuiz);
            echo = false;
            animations.Play("talking");
        }
        if (!mainAudio.isPlaying && question2 == true && echo1 == true)
        {
            mainAudio.PlayOneShot(laxeerQuiz);
            echo1 = false;
            animations.Play("talking");
        }

        if (!mainAudio.isPlaying && question3 == true && echo2 == true)
        {
            mainAudio.PlayOneShot(ontlastingQuiz);
            echo2 = false;
            animations.Play("talking");
        }
        if (!mainAudio.isPlaying && question4 == true && echo3 == true)
        {
            mainAudio.PlayOneShot(misselijkQuiz);
            echo3 = false;
            animations.Play("talking");
        }

        yield return null;

    }

    IEnumerator Quiz()
    {
        if (!mainAudio.isPlaying)
        {
            if (question1 == false)
            {
                if (quizBoard.GetComponent<Renderer>().materials[1].mainTexture = tabletten)
                {
                    if (receiveAnswer == 8 && audioecho1 == true)
                    {
                        mainAudio.PlayOneShot(goedtabletten);
                        audioecho1 = false;
                        animations.Play("Happy");
                        yield return new WaitForSeconds(1.5f);
                        animations.Play("talking");

                        yield return new WaitForSeconds(13.0f);
                        question2 = true;
                        quizBoard.GetComponent<Renderer>().materials[1].mainTexture = laxeermiddelen;




                    }

                }
            }
        }

        if (!mainAudio.isPlaying)
        {
            if (question2 == true)
            {
                if (quizBoard.GetComponent<Renderer>().materials[1].mainTexture = laxeermiddelen)
                {
                    if (receiveAnswer == 10 && audioecho == true)
                    {

                        mainAudio.PlayOneShot(goedlaxeer);
                        audioecho = false;
                        animations.Play("Happy");
                        yield return new WaitForSeconds(1.5f);
                        animations.Play("talking");
                        yield return new WaitForSeconds(8.0f);
                        question3 = true;

                        quizBoard.GetComponent<Renderer>().materials[1].mainTexture = ontlasting;

                    }
                }
            }
        }

        if (!mainAudio.isPlaying)
        {
            if (question3 == true)
            {
                if (quizBoard.GetComponent<Renderer>().materials[1].mainTexture = ontlasting)
                {
                    if (receiveAnswer == 8 && audioecho2 == true)
                    {
                        mainAudio.PlayOneShot(goedontlasting);

                        audioecho2 = false;
                        animations.Play("Happy");
                        yield return new WaitForSeconds(1.5f);
                        animations.Play("talking");
                        yield return new WaitForSeconds(10.0f);
                        question4 = true;
                        quizBoard.GetComponent<Renderer>().materials[1].mainTexture = misselijk;

                    }
                }
            }
        }

        if (!mainAudio.isPlaying)
        {
            if (question4 == true)
            {

                if (quizBoard.GetComponent<Renderer>().materials[1].mainTexture = misselijk)
                {

                    if (receiveAnswer == 9 && audioecho3 == true)
                    {


                        audioecho3 = false;


                        mainAudio.PlayOneShot(goedmisselijk);
                        animations.Play("Happy");
                        yield return new WaitForSeconds(1.5f);
                        animations.Play("talking");
                    }
                    if (receiveAnswer == 9 && !mainAudio.isPlaying && audioecho4 == true)
                    {

                        audioecho4 = false;
                        yield return new WaitForSeconds(2.0f);
                        mainAudio.PlayOneShot(audienceClap, 0.7F);
                        mainAudio.PlayOneShot(eindemodule);
                        Confetti.SetActive(true);
                        quizboard.GetComponent<MeshRenderer>().enabled = false;
                        quizboard.GetComponent<MeshCollider>().enabled = false;
                        var AnswerA = GameObject.Find("AnswerA");
                        var AnswerB = GameObject.Find("AnswerB");
                        var AnswerC = GameObject.Find("AnswerC");
                        AnswerA.GetComponent<MeshRenderer>().enabled = false;
                        AnswerB.GetComponent<MeshRenderer>().enabled = false;
                        AnswerC.GetComponent<MeshRenderer>().enabled = false;
                        AnswerA.GetComponent<MeshCollider>().enabled = false;
                        AnswerB.GetComponent<MeshCollider>().enabled = false;
                        AnswerC.GetComponent<MeshCollider>().enabled = false;
                        animations.Play("Happy");
                        yield return new WaitForSeconds(1.5f);
                        animations.Play("talking");

                    }

                }
            }
            yield return null;

        }
    }
       
}
