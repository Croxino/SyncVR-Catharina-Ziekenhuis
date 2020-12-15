using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audioscript_keuken : MonoBehaviour
{
    AudioSource introAudio;
    public AudioClip intro;
    private bool stopcor = true;
    public GameObject fader;
    private GameObject anim;
    private Animator fadeout;




    // Start is called before the first frame update
    void Start()
    {
        introAudio = GetComponent<AudioSource>();
        //Debug.Log(baxter.GetComponent<MeshRenderer>().sharedMaterials[4]);

        anim = GameObject.Find("fader");
        fadeout = anim.GetComponent<Animator>();

        fadeout.Play("FadeIn");



    }

    private void Update()
    {
        StartCoroutine("PlayKeuken");

    }



    IEnumerator PlayKeuken()
    {

        if (stopcor == true)
        {
            introAudio.PlayOneShot(intro);
            stopcor = false;
            //fadeout.Play("FadeOut");

        }

        if (!introAudio.isPlaying)
        {
            fadeout.Play("FadeOut");
            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadScene(4);
        }
        yield return null;

    }

}
