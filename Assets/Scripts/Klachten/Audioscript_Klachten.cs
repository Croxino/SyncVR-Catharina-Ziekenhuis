using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audioscript_Klachten : MonoBehaviour
{
    AudioSource introAudio;


    public bool calltodestroy;
    public AudioClip meds;
    public AudioClip intro;
    private bool stopcor = true;
    private bool stopcor2 = true;
    public int receiveAnswer;
    public GameObject fader;
    private GameObject anim;
    private Animator fadeout;
    public GameObject baxter;
    private Vector3 des;
    private Quaternion desrot;
    public float speed = 0.5f;
    public float smooth = 0.5f;

    private Animator animations;
    private GameObject animD;


    // Start is called before the first frame update
    void Start()
    {
        introAudio = GetComponent<AudioSource>();

        //Debug.Log(baxter.GetComponent<MeshRenderer>().sharedMaterials[4]);


        des = new Vector3(-2f, 0.4f, 2.9f);
        desrot = Quaternion.Euler(0, -40, 0);

        animD = GameObject.Find("Baxter");
        animations = animD.GetComponent<Animator>();

        anim = GameObject.Find("fader");
        fadeout = anim.GetComponent<Animator>();
        fadeout.Play("FadeIn");


    }

    private void Update()
    {
        receiveAnswer = Pvr_ControllerDemo.answerholder;


        Debug.Log("received " + receiveAnswer);



        StartCoroutine("PlayWelcome");
        StartCoroutine("SmoothMove");
        StartCoroutine("Medicatie");

        if (!introAudio.isPlaying)
        {
            animations.Play("Idle");
        }

    }

    IEnumerator PlayWelcome()
    {
        if (stopcor == true)
        {
            introAudio.PlayOneShot(intro);
            stopcor = false;
            animations.Play("talking");
        }
        
        if (!introAudio.isPlaying && stopcor2 == true)
        {

            introAudio.PlayOneShot(meds);
            stopcor2 = false;
            animations.Play("talking");

        }

            yield return null;
            
    }

    IEnumerator Medicatie()
    {

        calltodestroy = DestroyThis.activePills;
        if (calltodestroy == false && !introAudio.isPlaying )
        {
         
            fadeout.Play("FadeOut");


            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadScene(2);
        }
    }



    IEnumerator SmoothMove()
    {
        //yield return new WaitForSeconds(15.0f);
        baxter.transform.position = Vector3.Lerp(baxter.transform.position, des, Time.deltaTime * speed);
        baxter.transform.rotation = Quaternion.Slerp(baxter.transform.rotation, desrot, Time.deltaTime * smooth);
        animations.Play("talking");

        yield return null;
    }

    

}
