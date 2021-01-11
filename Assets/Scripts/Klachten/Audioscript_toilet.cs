using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audioscript_toilet : MonoBehaviour
{
    AudioSource introAudio;
    public float delay;
    //public GameObject pills;
    public bool calltodestroy;
    public AudioClip flush;
    public AudioClip intro;
    private bool stopcor = true;
    private bool stopcor2 = true;

    public Image _image;
    public int flashscreen = 3;
    public Texture textures;
    public Texture texturemain;

    public GameObject quizboard;
    public GameObject headhit;
    public int receiveAnswer;
    public GameObject fader;
    private GameObject anim;
    private GameObject animD;
    private Animator flushanim;
    private Animator fadeout;

    public float speed = 0.5f;
    public float smooth = 0.5f;
    [SerializeField] Color myColor;
    [SerializeField] [Range(0f, 255f)] float lerpTime;


    // Start is called before the first frame update
    void Start()
    {
        introAudio = GetComponent<AudioSource>();
        introAudio.PlayDelayed(delay);

        anim = GameObject.Find("fader");
        fadeout = anim.GetComponent<Animator>();
        fadeout.Play("FadeIn");


        animD = GameObject.Find("FlushHandle");
        flushanim = animD.GetComponent<Animator>();
    }

    private void Update()
    {
        receiveAnswer = Pvr_ControllerDemo.answerholder;


        Debug.Log("received " + receiveAnswer);


        StartCoroutine("PlayToilet");


    }



    IEnumerator PlayToilet()
    {
        if (stopcor == true)
        {
            introAudio.PlayOneShot(intro);
            stopcor = false;

        }

        //Plays the toilet flushing animation and sound
        if (receiveAnswer == 12 && stopcor2 == true)
        {
            introAudio.PlayOneShot(flush);
            stopcor2 = false;

            fadeout.Play("FadeOut");
            flushanim.Play("flush");

            yield return new WaitForSeconds(5.0f);
            SceneManager.LoadScene(5);
        }
        yield return null;

    }









}
