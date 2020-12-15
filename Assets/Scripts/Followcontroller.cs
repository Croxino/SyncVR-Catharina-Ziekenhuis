using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pvr_UnitySDKAPI;

public class Followcontroller : MonoBehaviour
{
    public Transform Destination;
    public Vector3 offset;
    public float speed = 1;
    private Vector3 movement;
    private bool alwaysfalse = false;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    IEnumerator Follow()
    {
        while (this.transform.position != Destination.transform.position)
        {
                this.transform.position = Vector3.Lerp(this.transform.position, Destination.transform.position + offset, Time.deltaTime * speed);
                yield return null;          
           }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Destination.position - transform.position;
        //direction.Normalize();
        movement = direction;
        Debug.Log(direction);

        //StartCoroutine("Follow");

    }

    void moveCharacter(Vector3 direction) 
    {
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }
}
