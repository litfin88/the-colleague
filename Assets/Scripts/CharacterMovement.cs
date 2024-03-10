using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
    private Animator anim;

    private Vector3 mousePos;
    private int currentBuildIndex;
    
    bool isMoving = false;
    bool canMove = false;

    public GameObject itemToTake;
    private TMP_Text tipText;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (currentBuildIndex == 0)
        {
            anim.SetBool("isFirstScene", true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        groundLayer = LayerMask.GetMask("Walkable");
        agent = GetComponent<NavMeshAgent>();
        tipText = GameObject.Find("TipText").GetComponent<TMP_Text>();

        if (currentBuildIndex == 0)
        {
            StartCoroutine(changingSituation(currentSituation.Sitting));
        }
        else
        {
            StartCoroutine(changingSituation(currentSituation.Idle));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == true && agent.remainingDistance < 0.5f)
        {
            StartCoroutine(changingSituation(currentSituation.Idle));
            isMoving = false;
        }
        
        if(isMoving == false && canMove == true){
            Ray ray = Camera.allCameras[0].ScreenPointToRay(mousePos);
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
            mousePos = Input.mousePosition;
            MoveChar();
        }
    }

    public void MoveChar()
    {
        StartCoroutine(changingSituation(currentSituation.Walking));
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
                if(itemToTake != null)
                {
                    anim.SetBool("isWalking", false);
                    anim.Play("Item Taking");
                    
                    canMove = false;
                    agent.enabled = false;
                    yield return new WaitForSeconds(1.5f);
                    agent.enabled = true;
                    
                    if(itemToTake.tag == "Cali")
                    {
                        if(itemToTake.name == "BicakCali")
                        {
                            StartCoroutine(TipTextChange("Bıçak bulundu!"));
                            PlayerPrefs.SetString("Inventory", PlayerPrefs.GetString("Inventory") + ",Bicak");
                        }
                        else if(itemToTake.name == "IpCali")
                        {
                            StartCoroutine(TipTextChange("İp bulundu!"));
                            PlayerPrefs.SetString("Inventory", PlayerPrefs.GetString("Inventory") + ",Ip");
                        }
                        else
                        {
                            StartCoroutine(TipTextChange("Bir şey bulunamadı."));
                        }

                        itemToTake.layer = LayerMask.NameToLayer("Default");
                    }
                    else
                    {
                        Destroy(itemToTake.gameObject);
                    }
                    
                    itemToTake = null;
                    canMove = true;
                }
                
                situation = currentSituation.Idle;
                break;
            
            case currentSituation.Walking:
                if(situation == currentSituation.Sitting)
                {
                    anim.SetTrigger("StandUp");
                    anim.SetBool("isWalking", true);
                    tipText.enabled = false;
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

    public IEnumerator TipTextChange(string txt)
    {
        tipText.enabled = true;
        tipText.text = txt;
        yield return new WaitForSeconds(3);
        tipText.enabled = false;
    }
}
