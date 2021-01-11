using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidbodymanager : MonoBehaviour
{
    // This script manages the rigidbody of the food items. So that certain food options can't be taken first we have to disable the rigidbody
    public GameObject cheese;
    Collider cheeseCol;
    public GameObject hotdog;
    Collider hotdogCol;
    public GameObject beer;
    Collider beerCol;


    public bool pillsdestroyed;
    public bool bottledestroyed;
    public bool cheesedestroyed;
    public bool hotdogdestroyed;

    // Start is called before the first frame update
    void Start()
    {
        cheeseCol = cheese.GetComponent<Collider>();
        hotdogCol = hotdog.GetComponent<Collider>();
        beerCol = beer.GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        pillsdestroyed = DestroyThis.activePills;
        bottledestroyed = DestroyBottle.activeBottle;
        hotdogdestroyed = DestroyHotdog.activeHotdog;
        cheesedestroyed = DestroyCheese.activeCheese;

        if (pillsdestroyed == false)
        {
            cheeseCol.enabled = true;
            hotdogCol.enabled = true;           
        }

        if (hotdogdestroyed == false && cheesedestroyed == false)
        {
            beerCol.enabled = true;
        }
    }
}
