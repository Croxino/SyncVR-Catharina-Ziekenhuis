using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulateCanvasses : MonoBehaviour
{
    [SerializeField]
    private Camera headCam;
    [SerializeField]
    private GameObject laserPointer;

    public void Start()
    {
        Canvas[] allCanvasses = FindObjectsOfType<Canvas>();

        foreach (Canvas c in allCanvasses)
        {
            c.worldCamera = headCam;
            SyncVRRaycaster raycaster = c.GetComponent<SyncVRRaycaster>();
            if (raycaster != null)
            {
                raycaster.pointer = laserPointer;
            }
        }
    }
}
