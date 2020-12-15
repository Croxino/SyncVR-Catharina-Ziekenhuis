using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBottle : MonoBehaviour
{

    public static bool activeBottle = true;
    
    // Start is called before the first frame update
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "PvrController1")
        {
            Debug.Log("collision detected!");
            this.gameObject.SetActive(false);

            activeBottle = false;
            //Debug.Log(activePills); 
        }
    }

}