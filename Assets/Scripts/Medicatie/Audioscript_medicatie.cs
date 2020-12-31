using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioscript_medicatie : MonoBehaviour
{

    AudioSource introAudio;

    public bool calltodestroy;

    private bool clonedestroyed = true;
    private bool ibutrue = true;
    private bool quiz = true;
    private bool startibu = false;
    private bool isCreated = false;

    private bool stopcor = true;
    private bool stopcor2 = true;
    private bool stopcor3 = true;
    private bool stopcor4 = true;


    private bool audioecho = true;
    private bool audioecho2 = true;
    public static bool audioecho3 = true;

    public bool answerA = false;
    public bool answerB = false;
    public bool answerC = false;

    public AudioClip vitaminegenomen;
    public AudioClip naaldpijn;
    public AudioClip pijnstillers;
    public AudioClip VitamineQuiz;
    public AudioClip naaldpijnQuiz;
    public AudioClip pijnstillersQuiz;
    public AudioClip audienceClap;
    public AudioClip paracetamol;
    public AudioClip eindemodule;
    public AudioClip foutantwoord;
    public AudioClip welkom;


    public int receiveAnswer;
    public int currentQuestion = 1;

    public Renderer quizBoard;
    public Texture vitamineQuiz;
    public Texture bloedsuikerQuiz;
    public Texture pijnstillerQuiz;
    public Renderer baxter;
    public Material glucosegevaar;
    public Texture textures;
    public Texture texturemain;

    public GameObject fader;
    public GameObject tube;
    public GameObject ibuprofen;
    public GameObject quizboard;
    public GameObject headhit;
    private GameObject anim;
    private GameObject animD;
    private GameObject clone;
    public GameObject Confetti;


    private Animator fadeout;
    private Animator animations;



    // Start is called before the first frame update
    void Start()
    {
        introAudio = GetComponent<AudioSource>();

        anim = GameObject.Find("fader");
        animD = GameObject.Find("Baxter");
        animations = animD.GetComponent<Animator>();
        fadeout = anim.GetComponent<Animator>();

        baxter.GetComponent<Renderer>();
        fadeout.Play("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("PlayWelkom");
        StartCoroutine("PlayAudio");
        receiveAnswer = Pvr_ControllerDemo.answerholder;

        if (!introAudio.isPlaying)
        {
            animations.Play("Idle");
            Pvr_ControllerDemo.answerholder = 0;
        }


        Debug.Log("Holding answer " + receiveAnswer);

        StartCoroutine("Quiz");


    }

    private void WrongAnswer()
    {
        introAudio.PlayOneShot(foutantwoord);
        animations.Play("Wrong");
        Pvr_ControllerDemo.answerholder = 0;

    }


    IEnumerator PlayWelkom()
    {

        if (stopcor == true)
        {
            animations.Play("talking");
            introAudio.PlayOneShot(welkom);
            stopcor = false;
        }

        yield return null;

    }

    IEnumerator PlayAudio()
    {
        calltodestroy = DestroyThis.activePills;
        if (calltodestroy == false && !introAudio.isPlaying && stopcor2 == true)
        {
            introAudio.PlayOneShot(vitaminegenomen);
            stopcor2 = false;
            animations.Play("Happy");
            yield return new WaitForSeconds(1.5f);
            animations.Play("talking");


        }

        if (calltodestroy == false && !introAudio.isPlaying && stopcor3 == true)
        {
            stopcor3 = false;
            yield return new WaitForSeconds(3.0f);
            introAudio.PlayOneShot(naaldpijn);
            animations.Play("talking");



            headhit.gameObject.SetActive(true);
            baxter.GetComponent<MeshRenderer>().materials[4].mainTexture = textures;
            tube.transform.position = new Vector3(-1f, -0.3f, 0.4987f);


        }

        if (!introAudio.isPlaying && headhit.activeSelf == true)
        {
            headhit.gameObject.SetActive(false);
            baxter.GetComponent<MeshRenderer>().materials[4].mainTexture = texturemain;
            tube.transform.position = new Vector3(-0.95f, -0.3f, 0.4987f);
            startibu = true;
        }

        if (ibutrue == true && startibu == true)
        {
            yield return new WaitForSeconds(3.0f);
            ibutrue = false;

            if (isCreated == false)
            {
                clone = Instantiate(ibuprofen, ibuprofen.transform.position, ibuprofen.transform.rotation);
                isCreated = true;

            }


            yield return new WaitForSeconds(22.0f);
            Destroy(clone);
            clonedestroyed = false;

            yield return new WaitForSeconds(4.0f);
            if (clonedestroyed == false && quiz == true)
            {
                quizboard.SetActive(true);
                introAudio.PlayOneShot(VitamineQuiz);
                animations.Play("talking");
                quiz = false;
            }

        }

        if (ibutrue == false && stopcor4 == true)
        {
            introAudio.PlayOneShot(pijnstillers);
            animations.Play("talking");
            stopcor4 = false;
        }

        yield return null;

    }

    IEnumerator Quiz()
    {


        if (!introAudio.isPlaying)
        {
            if (currentQuestion == 1)
            {
                if (quizBoard.GetComponent<Renderer>().materials[1].mainTexture = vitamineQuiz)
                {
                    if (receiveAnswer == 8)
                    {
                        quizBoard.GetComponent<Renderer>().materials[1].mainTexture = bloedsuikerQuiz;
                        answerA = true;
                        answerB = true;
                        introAudio.PlayOneShot(naaldpijnQuiz);
                        animations.Play("Happy");
                        yield return new WaitForSeconds(1.5f);
                        animations.Play("talking");
                        currentQuestion++;



                    }
                    if (receiveAnswer == 10 || receiveAnswer == 9)
                    {
                        WrongAnswer();
                    }





                }
            }
        }

        if (!introAudio.isPlaying)
        {
            if (currentQuestion == 2)
            {

                if (quizBoard.GetComponent<Renderer>().materials[1].mainTexture = bloedsuikerQuiz)
                {

                    if (receiveAnswer == 9 && audioecho == true)
                    {
                        quizBoard.GetComponent<Renderer>().materials[1].mainTexture = pijnstillerQuiz;
                        answerA = true;
                        answerB = true;
                        answerC = true;
                        introAudio.PlayOneShot(pijnstillersQuiz);
                        audioecho = false;
                        animations.Play("Happy");
                        yield return new WaitForSeconds(1.5f);
                        animations.Play("talking");
                        currentQuestion++;


                    }

                    if (receiveAnswer == 10 || receiveAnswer == 8)
                    {
                        WrongAnswer();
                    }


                }
            }
        }

        if (!introAudio.isPlaying)
        {
            if (currentQuestion == 3)
            {

                if (quizBoard.GetComponent<Renderer>().materials[1].mainTexture = pijnstillerQuiz)
                {

                    if (receiveAnswer == 8 && audioecho2 == true)
                    {


                        introAudio.PlayOneShot(paracetamol);
                        audioecho2 = false;

                        animations.Play("Happy");
                        yield return new WaitForSeconds(1.5f);
                        animations.Play("talking");
                        Pvr_ControllerDemo.answerholder = 8;

                    }
                    if (receiveAnswer == 8 && !introAudio.isPlaying && audioecho3 == true)
                    {
                        introAudio.PlayOneShot(audienceClap, 0.7F);
                        introAudio.PlayOneShot(eindemodule);
                        Confetti.SetActive(true);
                        audioecho3 = false;
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
