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
    private NavMeshAgent agent;
    private CharacterMovement charMove;
    private Animator anim;

    private bool isObjTaking;
    private GameObject objToTake;
    
    private void Start()
    {
        objectLayer = LayerMask.GetMask("Interactable");
        player = GameObject.FindWithTag("Player");
        agent = player.GetComponent<NavMeshAgent>();
        charMove = player.GetComponent<CharacterMovement>();
        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, objectLayer))
        {
            Debug.Log(hit.transform.name);
            if (Input.GetButtonDown("Fire1"))
            {
                if (hit.transform.tag == "Knife")
                {
                    isObjTaking = true;
                    objToTake = hit.transform.gameObject;
                }
            }
        }
        
        if(isObjTaking && agent.remainingDistance < 0.5f)
        {
            StartCoroutine(TakeObject(objToTake.name));
        }
    }

    public IEnumerator TakeObject(string objName)
    {
        Debug.Log(objName + " taken");
        anim.Play("Item Taking");
        yield return new WaitForSeconds(2);
    }
}
