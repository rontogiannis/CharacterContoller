using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    Quaternion initialRotation;

    // Start is called before the first frame update
    void Awake()
    {
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = initialRotation;
    }
}
