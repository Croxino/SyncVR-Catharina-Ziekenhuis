using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audioscript : MonoBehaviour
{
    AudioSource introAudio;
    public float delay;
    //public GameObject pills;
    public bool calltodestroy;
    public AudioClip vitaminegenomen;
    public AudioClip naaldpijn;
    public AudioClip pijnstillers;
    public AudioClip VitamineQuiz;
    private bool stopcor = true;
    private bool stopcor2 = true;
    private bool stopcor3 = true;
    private bool stopcor4 = true;
    private bool test = true;
    private bool stopflash = true;
    private bool clonedestroyed = true;
    public int flashscreen = 3;
    public Renderer baxter;
    public Material glucosegevaar;
    public Texture textures;
    public Texture texturemain;
    public GameObject tube;
    public GameObject ibuprofen;
    private bool ibutrue = true;
    private bool quiz = true;
    public GameObject quizboard;
    public GameObject headhit;
    public int receiveAnswer;
    public AudioClip welkom;

    public GameObject fader;
    private GameObject anim;
    private GameObject animD;
    private Animator fadeout;
    private Animator animations;


    // Start is called before the first frame update
    void Start()
    {
        introAudio = GetComponent<AudioSource>();
        introAudio.PlayDelayed(delay);

        anim = GameObject.Find("fader");
        animD = GameObject.Find("Baxter");
        animations = animD.GetComponent<Animator>();
        fadeout = anim.GetComponent<Animator>();

        baxter.GetComponent<Renderer>();
        fadeout.Play("FadeIn");

    }

    private void Update()
    {
        receiveAnswer = Pvr_ControllerDemo.answerholder;
        StartCoroutine("PlayAudio");
        StartCoroutine("FlashScreen");
        StartCoroutine("correctAnswer");

        if (calltodestroy == false && stopflash == true && test == false)
        {
            StartCoroutine("PlayNaald");
        }



        StartCoroutine("PlayPain");

        //if (introAudio.isPlaying)
        //{
        //    animations.Play("talking");
        //}
        //else
        if (!introAudio.isPlaying)
        {
           animations.Play("Idle");
        }
    }

    IEnumerator correctAnswer() 
    {
        if (stopcor == true)
        {
            introAudio.PlayOneShot(welkom);

            stopcor = false;
            animations.Play("talking");
        }

        if (receiveAnswer == 8)
        {
            animations.Play("Happy");
            yield return new WaitForSeconds(1.5f);
            animations.Play("talking");


        }

        yield return null;
    }

    IEnumerator PlayAudio()
    {
        calltodestroy = DestroyThis.activePills;
        if (calltodestroy == false && !introAudio.isPlaying && stopcor == true)
        {
            introAudio.PlayOneShot(vitaminegenomen);
            stopcor = false;
        }
        yield return null;

    }

    IEnumerator PlayNaald()
    {
        calltodestroy = DestroyThis.activePills;
        if (calltodestroy == false && !introAudio.isPlaying && stopcor2 == true)
        {
            introAudio.PlayOneShot(naaldpijn);
            stopcor2 = false;
        }
        yield return null;

    }

    IEnumerator PlayPain()
    {
 
        if (ibutrue == false && stopcor3 == true)
        {
            introAudio.PlayOneShot(pijnstillers);
            stopcor3 = false;

            yield return new WaitForSeconds(5.0f);

        }
        yield return null;

    }

    IEnumerator PlayvitamineQuiz()
    {

        if (ibutrue == true && stopcor4 == true)
        {
            introAudio.PlayOneShot(VitamineQuiz);
            stopcor4 = false;

            yield return new WaitForSeconds(5.0f);
        }
        yield return null;

    }


    IEnumerator FlashScreen()

    {
        if (calltodestroy == false && stopflash && test == true)
        {
            yield return new WaitForSeconds(14.0f);
            test = false;


            headhit.gameObject.SetActive(true);


            baxter.GetComponent<MeshRenderer>().materials[4].mainTexture = textures;
            tube.transform.position = new Vector3(-1f, -0.3f, 0.4987f);


            yield return new WaitForSeconds(15.0f);
            stopflash = false;
            yield return new WaitForSeconds(2.0f);
            headhit.gameObject.SetActive(false);
            baxter.GetComponent<MeshRenderer>().materials[4].mainTexture = texturemain;
            tube.transform.position = new Vector3(-0.95f, -0.3f, 0.4987f);
            yield return new WaitForSeconds(3.0f);
            if (ibutrue == true)
            {
                GameObject clone = (GameObject)Instantiate(ibuprofen, ibuprofen.transform.position, ibuprofen.transform.rotation);
                ibutrue = false;
                yield return new WaitForSeconds(22.0f);
                Destroy(clone);
                clonedestroyed = false;

                yield return new WaitForSeconds(4.0f);
                if (clonedestroyed == false && quiz == true)
                {
                    quizboard.SetActive(true);
                    introAudio.PlayOneShot(VitamineQuiz);
                    quiz = false;
                }

            }

        }
    }


}
