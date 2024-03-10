using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartScript : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;

    private GameObject dartTrigger;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10000);
        player = GameObject.FindWithTag("Player");
        dartTrigger = GameObject.Find("DartTrigger");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Win")
        {
            Debug.Log("Win");
            rb.isKinematic = true;
            CamTracker.isDartTriggered = false;
            player.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
            PlayerPrefs.SetString("Keys", ",General");
            Destroy(dartTrigger.gameObject);
            
        }
        else if (other.gameObject.name == "Lose")
        {
            Debug.Log("Lose");
            rb.isKinematic = true;
        }
    }
}
