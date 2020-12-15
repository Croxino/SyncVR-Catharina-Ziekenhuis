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
    private bool stopcor3 = true;
    private bool stopcor4 = true;
    private bool test = true;
    private bool stopflash = true;
    private bool clonedestroyed = true;
    public Image _image;
    public int flashscreen = 3;
    public Texture textures;
    public Texture texturemain;
    private bool ibutrue = true;
    private bool quiz = true;
    public GameObject quizboard;
    public GameObject headhit;
    public int receiveAnswer;
    public GameObject fader;
    private GameObject anim;
    private Animator fadeout;
    private Vector3 des;
    private Quaternion desrot;
    public float speed = 0.5f;
    public float smooth = 0.5f;
    [SerializeField] Color myColor;
    [SerializeField] [Range(0f, 255f)] float lerpTime;


    // Start is called before the first frame update
    void Start()
    {
        introAudio = GetComponent<AudioSource>();
        introAudio.PlayDelayed(delay);
        //Debug.Log(baxter.GetComponent<MeshRenderer>().sharedMaterials[4]);

        anim = GameObject.Find("fader");
        fadeout = anim.GetComponent<Animator>();
        des = new Vector3(-2f, 0.4f, 4f);
        desrot = Quaternion.Euler(0, -40, 0);
        fadeout.Play("FadeIn");



    }

    private void Update()
    {
        receiveAnswer = Pvr_ControllerDemo.answerholder;


        Debug.Log("received " + receiveAnswer);


        StartCoroutine("PlayToilet");
        //StartCoroutine("PlayvitamineQuiz");
        //StartCoroutine("PlayWelcome");
        // StartCoroutine("SmoothMove");
        //StartCoroutine("FadeOut");


        //baxter.transform.position = new Vector3 (-1.921f,0.036f, 1.528f);

    }



    IEnumerator PlayToilet()
    {
        if (stopcor == true)
        {
            introAudio.PlayOneShot(intro);
            stopcor = false;
            //fadeout.Play("FadeOut");

        }

        if (receiveAnswer == 12 && stopcor2 == true)
        {
            introAudio.PlayOneShot(flush);
            stopcor2 = false;

            fadeout.Play("FadeOut");

            yield return new WaitForSeconds(5.0f);
            SceneManager.LoadScene(5);
        }
        yield return null;

    }









}
