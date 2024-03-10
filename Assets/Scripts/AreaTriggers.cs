using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AreaTriggers : MonoBehaviour
{
    [Header("Ilk Bolum")]
    public Camera otherCam;
    private Camera mainCam;
    private GameManager _gameManager;
    private GameObject player;
    public GameObject blackPanel;
    private CanvasGroup bpGroup;

    private bool bitti;
    bool gerceklesti = false;
    private int currentBuildIndex;
    
    // [Header("Ikinci Bolum")]
    
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        
        if(currentBuildIndex == 0)
        {
            blackPanel.SetActive(false);
            bpGroup = blackPanel.GetComponent<CanvasGroup>();
        }
    }

    private void Update()
    {
        if (bitti)
        {
            blackPanel.SetActive(true);
            bpGroup.alpha += 0.01f;
            
            if(bpGroup.alpha >= 1)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (currentBuildIndex == 0)
            {
                Debug.Log(this.gameObject.name);
                if (this.gameObject.name == "DigerAlanTrigger")
                {
                    mainCam.gameObject.SetActive(false);
                    otherCam.gameObject.SetActive(true);
                    
                    if(gerceklesti == false)
                    {
                        gerceklesti = true;
                        StartCoroutine(waitForSec());
                    }
                }

                if (this.gameObject.name == "IlkAlanTrigger")
                {
                    mainCam.gameObject.SetActive(true);
                    otherCam.gameObject.SetActive(false);
                }
            }

            if (currentBuildIndex == 1)
            {
                if (this.gameObject.name == "DartTrigger")
                {
                    CamTracker.isDartTriggered = true;
                    StartCoroutine(_gameManager.ReadText(6));
                    Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
    }

    public IEnumerator waitForSec()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(_gameManager.ReadText(8));
        GameObject.FindWithTag("Girlfriend").SetActive(false);

        yield return new WaitForSeconds(30);
        player.GetComponent<CharacterMovement>().enabled = false;
        player.GetComponent<NavMeshAgent>().enabled = false;
        StartCoroutine(_gameManager.ReadText(9));

        yield return new WaitForSeconds(5f);
        bitti = true;
    }
}
