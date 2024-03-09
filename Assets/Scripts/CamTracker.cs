using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        pivot.transform.position = Vector3.Lerp(
            pivot.transform.position, 
            player.transform.position + new Vector3(0, 1.5f, 0), 
            0.03f);
        
        this.gameObject.transform.LookAt(pivot.transform);
    }
}
