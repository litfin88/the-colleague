using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum currentSituation
{
    Sitting,
    Idle,
    Walking,
    Running,
    Jumping,
    Attacking
}

public class CharacterMovement : MonoBehaviour
{
    private LayerMask groundLayer;
    private NavMeshAgent agent;
    public currentSituation situation;
    private currentSituation lastSituation;
    private Animator anim;
    
    bool isMoving = false;
    bool canMove = false;
    
    // Start is called before the first frame update
    void Start()
    {
        groundLayer = LayerMask.GetMask("Walkable");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == true && agent.remainingDistance < 0.5f)
        {
            StartCoroutine(changingSituation(currentSituation.Idle));
            anim.speed -= 0.1f;
            isMoving = false;
        }
        
        if(isMoving == false && canMove == true){
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, groundLayer))
            {
                Vector3 target = hit.point;
                agent.SetDestination(target);
                isMoving = true;
                canMove = false;
            }
        }
        
        // check if player is moving
        
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(changingSituation(currentSituation.Walking));
        }
    }
    
    public IEnumerator changingSituation(currentSituation newSituation)
    {
        switch (newSituation)
        {
            case currentSituation.Idle:
                if(situation == currentSituation.Walking)
                {
                    anim.SetBool("isWalking", false);
                }
                lastSituation = currentSituation.Idle;
                situation = currentSituation.Idle;
                break;
            
            case currentSituation.Walking:
                if(situation == currentSituation.Sitting)
                {
                    anim.SetTrigger("StandUp");
                    anim.SetBool("isWalking", true);
                    yield return new WaitForSeconds(2f);
                    canMove = true;
                    situation = currentSituation.Walking;
                }
                
                if(situation == currentSituation.Idle)
                {
                    anim.SetBool("isWalking", true);
                    canMove = true;
                    situation = currentSituation.Walking;
                }
                break;
            
        }
    }
}
