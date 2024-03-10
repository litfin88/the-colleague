using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interaction : MonoBehaviour
{
    private LayerMask objectLayer;
    private GameObject chairPivot;
    private GameObject player;
    private CharacterMovement charMove;
    
    private GameObject objToTake;
    
    private void Start()
    {
        objectLayer = LayerMask.GetMask("Interactable");
        player = GameObject.FindWithTag("Player");
        charMove = player.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.allCameras[0].ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, objectLayer))
        {
            Debug.Log(hit.transform.name);
            if (Input.GetButtonDown("Fire1"))
            {
                if (hit.transform.gameObject.tag == "Cali")
                {
                    objToTake = hit.transform.gameObject;
                    charMove.itemToTake = objToTake;
                }

                if (hit.transform.gameObject.tag == "Shot")
                {
                    PlayerPrefs.SetInt("Shot", 1);
                    PlayerPrefs.Save();
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
