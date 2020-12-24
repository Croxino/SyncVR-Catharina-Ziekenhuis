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

    public int currentQuestion = 0;


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
    public AudioClip foutantwoord;

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

        if (!mainAudio.isPlaying)
        {
            Pvr_ControllerDemo.answerholder = 0;

        }
    }

    private void WrongAnswer()
    {
        mainAudio.PlayOneShot(foutantwoord);
        animations.Play("Wrong");
        Pvr_ControllerDemo.answerholder = 0;

    }

    private void StartNextQuestion()
    {
        currentQuestion++;
        switch (currentQuestion)
        {
            case 1:
                mainAudio.PlayOneShot(tablettenQuiz);
                break;
            case 2:
                mainAudio.PlayOneShot(laxeerQuiz);
                break;
            case 3:
                mainAudio.PlayOneShot(ontlastingQuiz);
                break;
            case 4:
                mainAudio.PlayOneShot(misselijkQuiz);
                break;
            default:
                break;
        }
        animations.Play("talking");
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
            StartNextQuestion();

        }
        yield return null;

    }

    IEnumerator Quiz()
    {



        if (!mainAudio.isPlaying)
        {
            if (currentQuestion == 1)
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
                        StartNextQuestion();

                        quizBoard.GetComponent<Renderer>().materials[1].mainTexture = laxeermiddelen;
                    }
                    
                    if (receiveAnswer == 10 || receiveAnswer == 9)
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
                        StartNextQuestion();


                        quizBoard.GetComponent<Renderer>().materials[1].mainTexture = ontlasting;


                    }

                    if (receiveAnswer == 8 || receiveAnswer == 9)
                    {
                        WrongAnswer();
                    }
                }
            }
        }

        if (!mainAudio.isPlaying)
        {
            if (currentQuestion == 3)
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
                        StartNextQuestion();
                        quizBoard.GetComponent<Renderer>().materials[1].mainTexture = misselijk;

                    }

                    if (receiveAnswer == 10 || receiveAnswer == 9)
                    {
                        WrongAnswer();
                    }
                }
            }
        }

        if (!mainAudio.isPlaying)
        {
            if (currentQuestion == 4)
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
                    if (receiveAnswer == 8 || receiveAnswer == 10)
                    {
                        WrongAnswer();
                    }

                }
            }
            yield return null;

        }
    }
       
}
