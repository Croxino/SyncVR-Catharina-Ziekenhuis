using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audioscript_Klachten : MonoBehaviour
{
    AudioSource introAudio;
    public float delay;

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



    }

    private void Update()
    {
        receiveAnswer = Pvr_ControllerDemo.answerholder;


        Debug.Log("received " + receiveAnswer);


        //StartCoroutine("PlayFlush");
        //StartCoroutine("PlayvitamineQuiz");
        StartCoroutine("PlayWelcome");
        StartCoroutine("SmoothMove");
        StartCoroutine("Medicatie");
        //StartCoroutine("FadeOut");


        //baxter.transform.position = new Vector3 (-1.921f,0.036f, 1.528f);

    }

    IEnumerator PlayWelcome()
    {
        if (stopcor == true)
        {
            introAudio.PlayOneShot(intro);
            stopcor = false;
        }
        
        if (!introAudio.isPlaying && stopcor2 == true)
        {

            introAudio.PlayOneShot(meds);
            stopcor2 = false;
            
        }

            yield return null;
            
    }

    IEnumerator Medicatie()
    {

        calltodestroy = DestroyThis.activePills;
        if (calltodestroy == false && !introAudio.isPlaying )
        {
         
            fadeout.Play("FadeOut");


            yield return new WaitForSeconds(5.0f);
            SceneManager.LoadScene(2);
        }
    }



    IEnumerator SmoothMove()
    {
        yield return new WaitForSeconds(15.0f);
        baxter.transform.position = Vector3.Lerp(baxter.transform.position, des, Time.deltaTime * speed);
        baxter.transform.rotation = Quaternion.Slerp(baxter.transform.rotation, desrot, Time.deltaTime * smooth);

        yield return null;
    }

    

}
