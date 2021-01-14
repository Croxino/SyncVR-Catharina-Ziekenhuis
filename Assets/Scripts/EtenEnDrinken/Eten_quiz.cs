using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eten_quiz : MonoBehaviour
{
    public Renderer quizBoard;
    public Texture fosfaat;
    public Texture alcohol;
    public bool question1 = false;
    public bool question2 = false;
    private bool echo = true;
    private bool echo1 = true;
    private bool audioecho1 = true;
    private bool audioecho2 = true;
    private bool audioecho3 = true;
    public bool mainaudioecho;

    public int receiveAnswer;
    public int currentQuestion = 1;

    AudioSource mainAudio;
    public AudioClip fosfaatQuiz;
    public AudioClip fosfaatGoed;
    public AudioClip alcoholQuiz;
    public AudioClip alcoholGoed;
    public AudioClip audienceClap;
    public AudioClip eindemodule;
    public GameObject Confetti;
    public GameObject quizboard;
    public GameObject fader;
    private GameObject anim;
    private GameObject animD;
    private Animator fadeout;
    private Animator animations;
    public AudioClip foutantwoord;


    // Start is called before the first frame update
    void Start()
    {
        quizboard.SetActive(false);
        Confetti = GameObject.Find("Confetti");
        Confetti.SetActive(false);
        mainAudio = GetComponent<AudioSource>();
        anim = GameObject.Find("fader");
        fadeout = anim.GetComponent<Animator>();
        fadeout.Play("FadeIn");
        animD = GameObject.Find("Baxter");
        animations = animD.GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {

        receiveAnswer = Pvr_ControllerDemo.answerholder;
        Debug.Log("Holding answer " + receiveAnswer);


        StartCoroutine("Quiz");
        StartCoroutine("Playquestions");

        if (!mainAudio.isPlaying)
        {
            Pvr_ControllerDemo.answerholder = 0;

        }

        Debug.Log(currentQuestion);
    }

    private void WrongAnswer()
    {
        mainAudio.PlayOneShot(foutantwoord);
        animations.Play("Wrong");
        Pvr_ControllerDemo.answerholder = 0;

    }


    IEnumerator Playquestions()
    {
        if (!mainAudio.isPlaying)
        {
            animations.Play("Idle");
        }


        if (echo == true)
        {
            mainAudio.PlayOneShot(fosfaatQuiz);
            animations.Play("talking");
            echo = false;
            yield return new WaitForSeconds(9.0f);
            quizboard.SetActive(true);

        }
        if (question2 == true && echo1 == true)
        {
            mainAudio.PlayOneShot(alcoholQuiz);
            echo1 = false;
        }
        yield return null;
    }

    IEnumerator Quiz()
    {
        if (!mainAudio.isPlaying)
        {
            if (currentQuestion == 1)
            {
                if (quizBoard.GetComponent<Renderer>().materials[1].mainTexture = fosfaat)
                {
                    if (receiveAnswer == 9 && audioecho1 == true)
                    {
                        mainAudio.PlayOneShot(fosfaatGoed);
                        audioecho1 = false;
                        animations.Play("Happy");
                        yield return new WaitForSeconds(1.5f);
                        animations.Play("talking");



                        yield return new WaitForSeconds(9.0f);
                        question2 = true;
                        quizBoard.GetComponent<Renderer>().materials[1].mainTexture = alcohol;
                        currentQuestion++;
}

                    if (receiveAnswer == 8 || receiveAnswer == 10)
                    {
                        WrongAnswer();
                    }

                }
            }
        }


        if (!mainAudio.isPlaying)
        {
            if (currentQuestion == 2)
            {

                if (quizBoard.GetComponent<Renderer>().materials[1].mainTexture = alcohol)
                {

                    if (receiveAnswer == 8 && audioecho2 == true)
                    {


                        audioecho2 = false;


                        mainAudio.PlayOneShot(alcoholGoed);
                        animations.Play("Happy");
                        yield return new WaitForSeconds(1.5f);
                        animations.Play("talking");

                    }
                    if (receiveAnswer == 8 && !mainAudio.isPlaying && audioecho3 == true)
                    {

                        audioecho3 = false;

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
                        yield return new WaitForSeconds(4.5f);
                        animations.Play("talking");
                    }

                    if (receiveAnswer == 10 || receiveAnswer == 9)
                    {
                        WrongAnswer();
                    }
                }
            }
        }
        
        yield return null;

    }
}
