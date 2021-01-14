using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGrabbers : MonoBehaviour
{
    [SerializeField]
    private GameObject grabberPrefab;

    [SerializeField]
    private Transform leftHandAnchor;
    [SerializeField]
    private Transform rightHandAnchor;

    public void Start()
    {
        GameObject leftHandGrabber = Instantiate(grabberPrefab, leftHandAnchor);
        GameObject rightHandGrabber = Instantiate(grabberPrefab, rightHandAnchor);

#if OCULUS
#else
        leftHandGrabber.GetComponent<SyncVRGrabber>().Variety = Pvr_UnitySDKAPI.ControllerVariety.Controller0;
        rightHandGrabber.GetComponent<SyncVRGrabber>().Variety = Pvr_UnitySDKAPI.ControllerVariety.Controller1;
#endif
    }

}
