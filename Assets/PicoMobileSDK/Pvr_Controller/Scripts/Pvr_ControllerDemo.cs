// Copyright  2015-2020 Pico Technology Co., Ltd. All Rights Reserved.


#if !UNITY_EDITOR && UNITY_ANDROID 
#define ANDROID_DEVICE
#endif

using System;
using UnityEngine;
using System.Collections;
using Pvr_UnitySDKAPI;
using UnityEngine.SceneManagement;


public class Pvr_ControllerDemo : MonoBehaviour
{
    public GameObject HeadSetController;
    public GameObject controller0;
    public GameObject controller1;
    public GameObject pauseMenu;

    public Transform parent;
    public GameObject cube;
    public float speed;
    public GameObject pills;
    public bool pillsdestroyed;
    public bool bottledestroyed;
    public bool cheesedestroyed;
    public bool hotdogdestroyed;

    private Ray ray;
    private GameObject currentController;

    private Transform lastHit;
    private Transform currentHit;
    public Vector3 offset;
    public bool whilemoving;


    [SerializeField]
    private Material normat;
    [SerializeField]
    private Material gazemat;
    [SerializeField]
    private Material clickmat;
    [SerializeField]
    private Material highlight;
    [SerializeField]
    private Material invisible;
    [SerializeField]
    private Material hervatM;
    [SerializeField]
    private Material modulesM;
    [SerializeField]
    private Material hervatsel;
    [SerializeField]
    private Material modulessel;
    [SerializeField]
    private Material medicatie;
    [SerializeField]
    private Material medicatieSel;
    [SerializeField]
    private Material klachten;
    [SerializeField]
    private Material klachtenSel;
    [SerializeField]
    private Material eten;
    [SerializeField]
    private Material etenSel;
    private bool noClick;
    GameObject referenceObj;
    public float rayDefaultLength = 4;
    private bool isHasController = false;
    private bool headcontrolmode = false;
    private RaycastHit hit;
    private GameObject rayLine;
    private GameObject dot;
    public static int answerholder;


    void Start()
    {
     
  
        //pauseMenu.SetActive(false);


        ray = new Ray();
        hit = new RaycastHit();
        if (Pvr_UnitySDKManager.SDK.isHasController)
        {
            Pvr_ControllerManager.PvrServiceStartSuccessEvent += ServiceStartSuccess;
            Pvr_ControllerManager.SetControllerStateChangedEvent += ControllerStateListener;
            Pvr_ControllerManager.ControllerStatusChangeEvent += CheckControllerStateForGoblin;
            isHasController = true;
#if UNITY_EDITOR
            HeadSetController.SetActive(false);
            currentController = controller1;
            dot = controller1.transform.Find("dot").gameObject;
            dot.SetActive(true);
            rayLine = controller1.transform.Find("ray_LengthAdaptive").gameObject;
            rayLine.SetActive(true);
#endif
        }
        referenceObj = new GameObject("ReferenceObj");
    }

    void OnDestroy()
    {
        if (isHasController)
        {
            Pvr_ControllerManager.PvrServiceStartSuccessEvent -= ServiceStartSuccess;
            Pvr_ControllerManager.SetControllerStateChangedEvent -= ControllerStateListener;
            Pvr_ControllerManager.ControllerStatusChangeEvent -= CheckControllerStateForGoblin;
        }
    }

    
    IEnumerator SmoothMove()
    {
        while (dragObj.position != controller1.transform.position)
        {
            
            dragObj.position = Vector3.Lerp(dragObj.transform.position, controller1.transform.position, Time.deltaTime * speed);
            yield return null;

        }
    }

    IEnumerator SendAnswer()
    {
        answerholder = hit.transform.gameObject.layer;
        hit.transform.gameObject.layer = answerholder;
        yield return null;
    }


    void Update()
    {
        pillsdestroyed = DestroyThis.activePills;
        bottledestroyed = DestroyBottle.activeBottle;
        hotdogdestroyed = DestroyHotdog.activeHotdog;
        cheesedestroyed = DestroyCheese.activeCheese;
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (Controller.UPvr_GetKeyClick(0, Pvr_KeyCode.TOUCHPAD) || Controller.UPvr_GetKeyClick(1, Pvr_KeyCode.TOUCHPAD) || Input.GetKey("1"))
        {
            if (sceneName == "HomeMenu")
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
        }

        if (HeadSetController.activeSelf)
        {
            HeadSetController.transform.parent.localRotation = Quaternion.Euler(Pvr_UnitySDKSensor.Instance.HeadPose.Orientation.eulerAngles.x, Pvr_UnitySDKSensor.Instance.HeadPose.Orientation.eulerAngles.y, 0);

            ray.direction = HeadSetController.transform.position - HeadSetController.transform.parent.parent.Find("Head").position;
            ray.origin = HeadSetController.transform.parent.parent.Find("Head").position;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (HeadSetController.name == "SightPointer")
                {
                    HeadSetController.transform.localScale = new Vector3(0.09f, 0.09f, 0.09f);
                }

                currentHit = hit.transform;

                if (currentHit != null && lastHit != null && currentHit != lastHit)
                {
                    if (lastHit.GetComponent<Pvr_UIGraphicRaycaster>() && lastHit.transform.gameObject.activeInHierarchy && lastHit.GetComponent<Pvr_UIGraphicRaycaster>().enabled)
                    {
                        lastHit.GetComponent<Pvr_UIGraphicRaycaster>().enabled = false;
                    }
                }
                if (currentHit != null && lastHit != null && currentHit == lastHit)
                {
                    if (currentHit.GetComponent<Pvr_UIGraphicRaycaster>() && !currentHit.GetComponent<Pvr_UIGraphicRaycaster>().enabled)
                    {
                        currentHit.GetComponent<Pvr_UIGraphicRaycaster>().enabled = true;
                    }
                }

                if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("Water"))
                {
                    if (!noClick)
                        hit.transform.GetComponent<Renderer>().material = gazemat;
                }
                if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("Answer"))
                {
                    if (!noClick)
                        hit.transform.GetComponent<Renderer>().material = highlight;
                }
                lastHit = hit.transform;
#if UNITY_EDITOR
                Debug.DrawLine(ray.origin, hit.point, Color.red);
#endif
                if (Pvr_ControllerManager.Instance.LengthAdaptiveRay)
                {
                    HeadSetController.transform.position = hit.point;
                    HeadSetController.transform.position -= (hit.point - HeadSetController.transform.parent.parent.Find("Head").position).normalized * 0.02f;
                    float scale = 0.008f * hit.distance / 4f;
                    Mathf.Clamp(scale, 0.002f, 0.008f);
                    HeadSetController.transform.localScale = new Vector3(scale, scale, 1);
                }
            }
            else
            {
                if (HeadSetController.name == "SightPointer")
                {
                    HeadSetController.transform.localScale = Vector3.zero;
                }
                if (lastHit != null && 1 << lastHit.transform.gameObject.layer == LayerMask.GetMask("Water"))
                {
                    lastHit.transform.GetComponent<Renderer>().material = normat;
                }
                currentHit = null;
                noClick = false;
                if (Pvr_ControllerManager.Instance.LengthAdaptiveRay)
                {
                    HeadSetController.transform.position = HeadSetController.transform.parent.parent.Find("Head").position + ray.direction.normalized * (0.5f + rayDefaultLength);
                    HeadSetController.transform.localScale = new Vector3(0.008f, 0.008f, 1);
                }
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetMouseButtonDown(0))
            {
                if (lastHit != null && 1 << lastHit.transform.gameObject.layer == LayerMask.GetMask("Water") && currentHit != null)
                {
                    lastHit.transform.GetComponent<Renderer>().material = clickmat;
                    noClick = true;
                }
            }
        }
        else
        {
            if (currentController != null)
            {
                ray.direction = currentController.transform.forward - currentController.transform.up * 0.25f;
                ray.origin = currentController.transform.Find("start").position;

                if (Physics.Raycast(ray, out hit))
                {
                    currentHit = hit.transform;

                    if (currentHit != null && lastHit != null && currentHit != lastHit)
                    {
                        if (lastHit.GetComponent<Pvr_UIGraphicRaycaster>() && lastHit.transform.gameObject.activeInHierarchy && lastHit.GetComponent<Pvr_UIGraphicRaycaster>().enabled)
                        {
                            lastHit.GetComponent<Pvr_UIGraphicRaycaster>().enabled = false;
                        }
                    }
                    if (currentHit != null && lastHit != null && currentHit == lastHit)
                    {
                        if (currentHit.GetComponent<Pvr_UIGraphicRaycaster>() && !currentHit.GetComponent<Pvr_UIGraphicRaycaster>().enabled)
                        {
                            currentHit.GetComponent<Pvr_UIGraphicRaycaster>().enabled = true;

                        }
                    }
                    if (1 <<hit.transform.gameObject.layer == LayerMask.GetMask("Water"))
                    {
                        if (!noClick)
                        {
                            hit.transform.GetComponent<Renderer>().material = gazemat;
                        }
                        Debug.Log(hit.transform.gameObject.layer);

                        if (Controller.UPvr_GetKeyClick(0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyClick(1, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0))
                        {
                            referenceObj.transform.position = hit.point;

                            disX = hit.transform.position.x - referenceObj.transform.position.x;
                            disY = hit.transform.position.y - referenceObj.transform.position.y;
                            dragObj = hit.transform;

                            referenceObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.transform.position.z);

                            dragObj.position = new Vector3(referenceObj.transform.position.x + disX, referenceObj.transform.position.y + disY, hit.transform.position.z);
                            Debug.Log(dragObj.transform.position.z);

                            StartCoroutine("SmoothMove");

                        }

                    }

                    if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("Food"))
                    {

                        Debug.Log(hit.transform.gameObject.layer);

                        if (Controller.UPvr_GetKeyClick(0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyClick(1, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0))
                        {
                            referenceObj.transform.position = hit.point;

                            disX = hit.transform.position.x - referenceObj.transform.position.x;
                            disY = hit.transform.position.y - referenceObj.transform.position.y;
                            dragObj = hit.transform;

                            referenceObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.transform.position.z);

                            dragObj.position = new Vector3(referenceObj.transform.position.x + disX, referenceObj.transform.position.y + disY, hit.transform.position.z);
                            Debug.Log(dragObj.transform.position.z);

                            StartCoroutine("SmoothMove");

                        }

                    }

                    if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("AnswerA"))
                    {
                        if (!noClick)
                        {
                            hit.transform.GetComponent<Renderer>().material = highlight;
                        }

                        var AnswerA= GameObject.Find("AnswerA");
                        var AnswerB= GameObject.Find("AnswerB");
                        var AnswerC= GameObject.Find("AnswerC");
                        AnswerB.transform.GetComponent<Renderer>().material = invisible;
                        AnswerC.transform.GetComponent<Renderer>().material = invisible;

                                               Debug.Log("I am hitting" + hit.transform.gameObject.layer);

                        Debug.Log("holding" + answerholder);



                        if (Controller.UPvr_GetKeyClick(0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyClick(1, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0))
                        {
                            referenceObj.transform.position = hit.point;

                            disX = hit.transform.position.x - referenceObj.transform.position.x;
                            disY = hit.transform.position.y - referenceObj.transform.position.y;
                            dragObj = hit.transform;

                            referenceObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.transform.position.z);

                            dragObj.position = new Vector3(referenceObj.transform.position.x + disX, referenceObj.transform.position.y + disY, hit.transform.position.z);
                            Debug.Log(dragObj.transform.position.z);
                            StartCoroutine("SendAnswer");
                        }

                    }


                    if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("AnswerB"))
                    {
                        if (!noClick)
                        {
                            hit.transform.GetComponent<Renderer>().material = highlight;
                        }
                        var AnswerA = GameObject.Find("AnswerA");
                        var AnswerB = GameObject.Find("AnswerB");
                        var AnswerC = GameObject.Find("AnswerC");
                        AnswerA.transform.GetComponent<Renderer>().material = invisible;
                        AnswerC.transform.GetComponent<Renderer>().material = invisible;


                        Debug.Log(hit.transform.gameObject.layer);



                        if (Controller.UPvr_GetKeyClick(0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyClick(1, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0))
                        {
                            referenceObj.transform.position = hit.point;

                            disX = hit.transform.position.x - referenceObj.transform.position.x;
                            disY = hit.transform.position.y - referenceObj.transform.position.y;
                            dragObj = hit.transform;

                            referenceObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.transform.position.z);

                            dragObj.position = new Vector3(referenceObj.transform.position.x + disX, referenceObj.transform.position.y + disY, hit.transform.position.z);
                            Debug.Log(dragObj.transform.position.z);
                            StartCoroutine("SendAnswer");
                        }

                    }


                    if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("AnswerC"))
                    {
                        if (!noClick)
                        {
                            hit.transform.GetComponent<Renderer>().material = highlight;
                        }

                        var AnswerA = GameObject.Find("AnswerA");
                        var AnswerB = GameObject.Find("AnswerB");
                        var AnswerC = GameObject.Find("AnswerC");
                        AnswerA.transform.GetComponent<Renderer>().material = invisible;
                        AnswerB.transform.GetComponent<Renderer>().material = invisible;
 

                        Debug.Log(hit.transform.gameObject.layer);



                        if (Controller.UPvr_GetKeyClick(0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyClick(1, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0))
                        {
                            referenceObj.transform.position = hit.point;

                            disX = hit.transform.position.x - referenceObj.transform.position.x;
                            disY = hit.transform.position.y - referenceObj.transform.position.y;
                            dragObj = hit.transform;

                            referenceObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.transform.position.z);

                            dragObj.position = new Vector3(referenceObj.transform.position.x + disX, referenceObj.transform.position.y + disY, hit.transform.position.z);
                            Debug.Log(dragObj.transform.position.z);
                            StartCoroutine("SendAnswer");
                        }

                    }

                    if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("nothing"))
                    {
                        var AnswerA = GameObject.Find("AnswerA");
                        var AnswerB = GameObject.Find("AnswerB");
                        var AnswerC = GameObject.Find("AnswerC");
                        if (!noClick)
                        {
                            AnswerB.transform.GetComponent<Renderer>().material = invisible;
                            AnswerC.transform.GetComponent<Renderer>().material = invisible;
                            AnswerA.transform.GetComponent<Renderer>().material = invisible;

                            Debug.Log(hit.transform.gameObject.layer);
                        }
                        Debug.Log(hit.transform.gameObject.layer);

                    }

                    if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("general"))
                    {


                        var flush = GameObject.Find("flush");

                        if (!noClick)
                        {
                            flush.transform.GetComponent<Renderer>().material = highlight;
                            Debug.Log(hit.transform.gameObject.layer);
                        }


                    }

                    if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("UI"))
                    {
                        var flush = GameObject.Find("flush");
                        if (!noClick)
                        {
                            flush.transform.GetComponent<Renderer>().material = invisible;
                            Debug.Log(hit.transform.gameObject.layer);
                        }
                        Debug.Log(hit.transform.gameObject.layer);

                    }


                    if (Controller.UPvr_GetKeyClick(0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyClick(1, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0) && 1 << hit.transform.gameObject.layer == LayerMask.GetMask("general"))
                    {

                        referenceObj.transform.position = hit.point;

                        disX = hit.transform.position.x - referenceObj.transform.position.x;
                        disY = hit.transform.position.y - referenceObj.transform.position.y;
                        dragObj = hit.transform;

                        referenceObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.transform.position.z);

                        dragObj.position = new Vector3(referenceObj.transform.position.x + disX, referenceObj.transform.position.y + disY, hit.transform.position.z);
                        Debug.Log(dragObj.transform.position.z);
                        Debug.Log("You Flushed");
                        StartCoroutine("SendAnswer");
                    }

                    //if (pillsdestroyed == false)
                    //{
                    //    StopCoroutine("SmoothMove");
                    //}


                    lastHit = hit.transform;

                    if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("resume"))
                    {
                        if (!noClick)
                        {
                            hit.transform.GetComponent<Renderer>().material = hervatsel;
                        }
                        var module = GameObject.Find("modules");

                        module.transform.GetComponent<Renderer>().material = modulesM;

                        Debug.Log(hit.transform.gameObject.layer);



                        if (Controller.UPvr_GetKeyClick(0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyClick(1, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0))
                        {
                            referenceObj.transform.position = hit.point;

                            disX = hit.transform.position.x - referenceObj.transform.position.x;
                            disY = hit.transform.position.y - referenceObj.transform.position.y;
                            dragObj = hit.transform;

                            referenceObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.transform.position.z);

                            dragObj.position = new Vector3(referenceObj.transform.position.x + disX, referenceObj.transform.position.y + disY, hit.transform.position.z);
                            Debug.Log(dragObj.transform.position.z);
                            pauseMenu.SetActive(false);
                        }

                    }

                    if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("home"))
                    {
                        if (!noClick)
                        {
                            hit.transform.GetComponent<Renderer>().material = modulessel;
                        }
                        var hervat = GameObject.Find("hervatten");

                        hervat.transform.GetComponent<Renderer>().material = hervatM;

                        Debug.Log(hit.transform.gameObject.layer);



                        if (Controller.UPvr_GetKeyClick(0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyClick(1, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0))
                        {
                            referenceObj.transform.position = hit.point;

                            disX = hit.transform.position.x - referenceObj.transform.position.x;
                            disY = hit.transform.position.y - referenceObj.transform.position.y;
                            dragObj = hit.transform;

                            referenceObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.transform.position.z);

                            dragObj.position = new Vector3(referenceObj.transform.position.x + disX, referenceObj.transform.position.y + disY, hit.transform.position.z);
                            Debug.Log(dragObj.transform.position.z);
                            SceneManager.LoadScene(0);
                        }

                    }


                    if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("medicatie"))
                    {
                        if (!noClick)
                        {
                            hit.transform.GetComponent<Renderer>().material = medicatieSel;
                        }
                        var meds = GameObject.Find("Klachten");
                        var food = GameObject.Find("Eten en Drinken");

                        meds.transform.GetComponent<Renderer>().material = klachten;
                        food.transform.GetComponent<Renderer>().material = eten;

                        Debug.Log(hit.transform.gameObject.layer);



                        if (Controller.UPvr_GetKeyClick(0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyClick(1, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0))
                        {
                            referenceObj.transform.position = hit.point;

                            disX = hit.transform.position.x - referenceObj.transform.position.x;
                            disY = hit.transform.position.y - referenceObj.transform.position.y;
                            dragObj = hit.transform;

                            referenceObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.transform.position.z);

                            dragObj.position = new Vector3(referenceObj.transform.position.x + disX, referenceObj.transform.position.y + disY, hit.transform.position.z);
                            Debug.Log(dragObj.transform.position.z);
                            SceneManager.LoadScene(7);
                        }

                    }

                    if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("klachten"))
                    {
                        if (!noClick)
                        {
                            hit.transform.GetComponent<Renderer>().material = klachtenSel;
                        }
                        var klacht = GameObject.Find("Medicatie");
                        var food = GameObject.Find("Eten en Drinken");

                        klacht.transform.GetComponent<Renderer>().material = medicatie;
                        food.transform.GetComponent<Renderer>().material = eten;

                        Debug.Log(hit.transform.gameObject.layer);



                        if (Controller.UPvr_GetKeyClick(0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyClick(1, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0))
                        {
                            referenceObj.transform.position = hit.point;

                            disX = hit.transform.position.x - referenceObj.transform.position.x;
                            disY = hit.transform.position.y - referenceObj.transform.position.y;
                            dragObj = hit.transform;

                            referenceObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.transform.position.z);

                            dragObj.position = new Vector3(referenceObj.transform.position.x + disX, referenceObj.transform.position.y + disY, hit.transform.position.z);
                            Debug.Log(dragObj.transform.position.z);
                            SceneManager.LoadScene(1);
                        }

                    }

                    if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("eten"))
                    {
                        if (!noClick)
                        {
                            hit.transform.GetComponent<Renderer>().material = etenSel;
                        }
                        var klacht = GameObject.Find("Medicatie");
                        var meds = GameObject.Find("Klachten");

                        meds.transform.GetComponent<Renderer>().material = klachten;
                        klacht.transform.GetComponent<Renderer>().material = medicatie;

                        Debug.Log(hit.transform.gameObject.layer);



                        if (Controller.UPvr_GetKeyClick(0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyClick(1, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0))
                        {
                            referenceObj.transform.position = hit.point;

                            disX = hit.transform.position.x - referenceObj.transform.position.x;
                            disY = hit.transform.position.y - referenceObj.transform.position.y;
                            dragObj = hit.transform;

                            referenceObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.transform.position.z);

                            dragObj.position = new Vector3(referenceObj.transform.position.x + disX, referenceObj.transform.position.y + disY, hit.transform.position.z);
                            Debug.Log(dragObj.transform.position.z);
                            SceneManager.LoadScene(8);
                        }

                    }




#if UNITY_EDITOR
                    Debug.DrawLine(ray.origin, hit.point, Color.red);
#endif
                    currentController.transform.Find("dot").position = hit.point;
                    if (Pvr_ControllerManager.Instance.LengthAdaptiveRay)
                    {
                        float scale = 0.178f * currentController.transform.Find("dot").localPosition.z / 3.3f;
                        Mathf.Clamp(scale, 0.05f, 0.178f);
                        currentController.transform.Find("dot").localScale = new Vector3(scale, scale, 1);
                    }
                }
                else
                {
                    if (lastHit != null && 1 << lastHit.transform.gameObject.layer == LayerMask.GetMask("Water"))
                    {
                        lastHit.transform.GetComponent<Renderer>().material = normat;
                    }
                    currentHit = null;
                    noClick = false;

                    if(Pvr_ControllerManager.Instance.LengthAdaptiveRay)
                    {
                        currentController.transform.Find("dot").localScale = new Vector3(0.178f, 0.178f, 1);
                        currentController.transform.Find("dot").position = currentController.transform.position + currentController.transform.forward.normalized * (0.5f + rayDefaultLength);
                    }
                }
#if UNITY_EDITOR
                rayLine.GetComponent<LineRenderer>().SetPosition(0,currentController.transform.TransformPoint(0, 0, 0.072f));
                rayLine.GetComponent<LineRenderer>().SetPosition(1, dot.transform.position);
#endif
            }

            if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER) ||
                Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0))
            {
                if (lastHit != null && 1 << lastHit.transform.gameObject.layer == LayerMask.GetMask("Water") && currentHit != null)
                {
                    lastHit.transform.GetComponent<Renderer>().material = clickmat;
                    noClick = true;
                }
            }
        }

    }
    private Transform dragObj;
    private Transform parented;
    float disX, disY, disZ;





    private void ServiceStartSuccess()
    {
        if (Controller.UPvr_GetControllerState(0) == ControllerState.Connected ||
            Controller.UPvr_GetControllerState(1) == ControllerState.Connected)
        {
            HeadSetController.SetActive(false);
        }
        else
        {
            HeadSetController.SetActive(true);
        }
        if (Controller.UPvr_GetMainHandNess() == 0)
        {
            currentController = controller0;
        }
        if (Controller.UPvr_GetMainHandNess() == 1)
        {
            currentController = controller1;
        }
    }

    private void ControllerStateListener(string data)
    {

        if (Controller.UPvr_GetControllerState(0) == ControllerState.Connected ||
            Controller.UPvr_GetControllerState(1) == ControllerState.Connected)
        {
            HeadSetController.SetActive(false);
        }
        else
        {
            HeadSetController.SetActive(true);
        }

        if (Controller.UPvr_GetMainHandNess() == 0)
        {
            currentController = controller0;
        }
        if (Controller.UPvr_GetMainHandNess() == 1)
        {
            currentController = controller1;
        }
    }

    private void CheckControllerStateForGoblin(string state)
    {
        HeadSetController.SetActive(Convert.ToInt16(state) != 1);
    }

    public void SwitchControlMode()
    {
#if UNITY_EDITOR
        if (headcontrolmode)
        {
            headcontrolmode = false;
            HeadSetController.SetActive(false);
            controller0.SetActive(true);
            controller1.SetActive(true);
        }
        else
        {
            headcontrolmode = true;
            HeadSetController.SetActive(true);
            controller0.SetActive(false);
            controller1.SetActive(false);
        }
#endif
    }

}
