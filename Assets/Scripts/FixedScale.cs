using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedScale : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform objectToFollow;
    public Vector3 offset;
    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.position + offset;
    }
}
