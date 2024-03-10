using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamTracker : MonoBehaviour
{
    private GameObject pivot;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        pivot = GameObject.Find("CamPivot");
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            pivot.transform.position = Vector3.Lerp(
                pivot.transform.position, 
                player.transform.position + new Vector3(0, 1f, 0), 
                0.03f);
        
            this.gameObject.transform.LookAt(pivot.transform);
        }
        else
        {
            pivot.transform.position = Vector3.Lerp(
                pivot.transform.position, 
                player.transform.position + new Vector3(0, 1f, 0), 
                0.03f);
            this.gameObject.transform.position = pivot.transform.position + new Vector3(0, 9, 0);
        }

    }
}
