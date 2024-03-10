using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Interaction : MonoBehaviour
{
    private LayerMask objectLayer;
    private GameObject chairPivot;
    private GameObject player;
    private CharacterMovement charMove;
    
    private GameObject objToTake;

    private NavMeshAgent firstNav;
    private NavMeshAgent specialNav;
    private NavMeshAgent bathNav;
    private NavMeshAgent kitchenNav;
    
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

                if (hit.transform.gameObject.tag == "Door")
                {
                    firstNav = GameObject.Find("FirstDoorNav").GetComponent<NavMeshAgent>();
                    specialNav = GameObject.Find("SpecialNav").GetComponent<NavMeshAgent>();
                    bathNav = GameObject.Find("BathNav").GetComponent<NavMeshAgent>();
                    kitchenNav = GameObject.Find("KitchenNav").GetComponent<NavMeshAgent>();
                    
                    if (hit.transform.gameObject.name == "Ozel_Kapi" && PlayerPrefs.GetString("Keys").Split(",").Contains("Ozel"))
                    {
                        hit.transform.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
                        hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                        player.GetComponent<NavMeshAgent>().areaMask = specialNav.areaMask;
                    }
                    else if(hit.transform.gameObject.name == "Yatak_Odasi_1_Kapi" && PlayerPrefs.GetString("Keys").Split(",").Contains("General"))
                    {
                        hit.transform.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
                        hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                        player.GetComponent<NavMeshAgent>().areaMask = firstNav.areaMask;
                    }
                    else if(hit.transform.gameObject.name == "Banyo_Kapi" && PlayerPrefs.GetString("Keys").Split(",").Contains("General"))
                    {
                        hit.transform.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
                        hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                        player.GetComponent<NavMeshAgent>().areaMask = bathNav.areaMask;
                    }
                    else if(hit.transform.gameObject.name == "Mutfak_Kapi" && PlayerPrefs.GetString("Keys").Split(",").Contains("General"))
                    {
                        hit.transform.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
                        hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                        player.GetComponent<NavMeshAgent>().areaMask = kitchenNav.areaMask;
                    }
                }

                if (hit.transform.name == "Ozel_Kapi_Key")
                {
                    objToTake = hit.transform.gameObject;
                    charMove.itemToTake = objToTake;
                    PlayerPrefs.SetString("Keys", PlayerPrefs.GetString("Keys") + ",Ozel");
                }
            }
        }
    }
}
