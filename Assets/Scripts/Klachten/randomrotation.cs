using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomrotation : MonoBehaviour
{
    public float minimum = -1.0F;
    public float maximum = 1.0F;

    // starting value for the Lerp
    static float smooth = 1.0f;
    public float speed = 2f;
    public float RotAngleZ = 45;
    public float rotFl = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion rotation = Quaternion.Euler(0, 0, 10);
        //transform.localRotation = Quaternion.identity;
        //transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime * smooth);

        //float rY = Mathf.SmoothStep(0, RotAngleZ, Mathf.PingPong(Time.time * speed, 1));
        //transform.localRotation = Quaternion.Euler(-90, 0, rY);

        //transform.Rotate(0, 0, 1);
        //transform.Rotate(0, 0, -1);

        //transform.localEulerAngles = new Vector3(0, 0, -Mathf.PingPong(Time.time * 50, rotFl));
    }
}
