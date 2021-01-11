using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    //destroys the pills
    public static bool activePills = true;

    // Start is called before the first frame update
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "PvrController1")
        {
            Debug.Log("collision detected!");
            this.gameObject.SetActive(false);
            activePills = false;

            //Debug.Log(activePills); 
        }
    }

}
