using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

public class QuizScript : MonoBehaviour
{
    public Renderer quizBoard;
    public Texture vitamineQuiz;
    public Texture bloedsuikerQuiz;
    public Texture pijnstillerQuiz;
    public bool answerA = false;
    public bool answerB =  false;
    public bool answerC = false;
    private bool audioecho = true;
    private bool audioecho2 = true;
    public static bool audioecho3 = true;
    public int receiveAnswer;
    AudioSource mainAudio;
    public AudioClip naaldpijnQuiz;
    public AudioClip pijnstillersQuiz;
    public AudioClip audienceClap;
    public AudioClip paracetamol;
    public AudioClip eindemodule;
    public AudioClip foutantwoord;
    public GameObject Confetti;
    public GameObject quizboard;

    // Start is called before the first frame update
    void Start()
    {
        quizboard.SetActive(false);
        Confetti = GameObject.Find("Confetti");
        Confetti.SetActive(false);
        mainAudio = GetComponent<AudioSource>();

        // quizBoard.GetComponent<Renderer>().materials[1].mainTexture = vitamineQuiz;

    }

    // Update is called once per frame
    void Update()
    {
        
        receiveAnswer = Pvr_ControllerDemo.answerholder;
        Debug.Log("Holding answer " + receiveAnswer);

        Quiz();

    }



    private void Quiz()
    {

        if (answerA == false)
        {
            if (quizBoard.GetComponent<Renderer>().materials[1].mainTexture = vitamineQuiz)
            {
                if (receiveAnswer == 8)
                {
                    quizBoard.GetComponent<Renderer>().materials[1].mainTexture = bloedsuikerQuiz;
                    answerA = true;
                    answerB = true;
                    mainAudio.PlayOneShot(naaldpijnQuiz);

                }

               


            }
        }

        if (answerB == true)
        {

            if (quizBoard.GetComponent<Renderer>().materials[1].mainTexture = bloedsuikerQuiz)
            {

                if (receiveAnswer == 9 && audioecho == true)
                {
                    quizBoard.GetComponent<Renderer>().materials[1].mainTexture = pijnstillerQuiz;
                    answerA = true;
                    answerB = true;
                    answerC = true;
                    mainAudio.PlayOneShot(pijnstillersQuiz);
                    audioecho = false;
  

                }

               
            }
        }

        if (answerC == true)
        {

            if (quizBoard.GetComponent<Renderer>().materials[1].mainTexture = pijnstillerQuiz)
            {

                if (receiveAnswer == 8 && audioecho2 == true)
                {


                    mainAudio.PlayOneShot(paracetamol);
                    audioecho2 = false;
                    Debug.Log("Klaar met module");
                }
                if (receiveAnswer == 8 && !mainAudio.isPlaying && audioecho3 == true)
                {
                    mainAudio.PlayOneShot(audienceClap,0.7F);
                    mainAudio.PlayOneShot(eindemodule);
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

                }
              
            }
        }
    }
}
