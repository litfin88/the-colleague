using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartScript : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10000);
    }

}
