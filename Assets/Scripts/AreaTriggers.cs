using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject thanksPanel;
    private CanvasGroup bpGroup;
    private TMP_Text thanksText;

    private TMP_Text subtitle;

    public string[] dialogues;
    
    public AudioClip[] audioClips;
    private AudioSource customSource;
    AudioSource bgMusic;

    private bool bitti;
    bool gerceklesti = false;
    private int currentBuildIndex;
    
    bool isItEnd = false;
    
    // [Header("Ikinci Bolum")]
    
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        subtitle = GameObject.Find("Subtitle").GetComponent<TMP_Text>();
        bgMusic = GameObject.Find("BGMusic").GetComponent<AudioSource>();
        
        if(currentBuildIndex == 0)
        {
            blackPanel.SetActive(false);
            bpGroup = blackPanel.GetComponent<CanvasGroup>();
            
            if(PlayerPrefs.GetInt("isStarted") == 0)
            {
                PlayerPrefs.SetInt("isStarted", 1);
                SceneManager.LoadScene(2);
            }
   
        }

        if (currentBuildIndex == 1)
        {
            customSource = gameObject.GetComponent<AudioSource>();
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

        if (isItEnd)
        {
            blackPanel.SetActive(true);
            bpGroup.alpha += 0.002f;
            subtitle.enabled = false;
            
            if(bpGroup.alpha >= 1)
            {
                thanksPanel.SetActive(true);
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
                    bgMusic.Stop();
                    bgMusic.clip = audioClips[1];
                    bgMusic.Play();
                    
                    if(gerceklesti == false)
                    {
                        gerceklesti = true;
                        StartCoroutine(waitForSec());
                    }
                    
                    PlayerPrefs.DeleteAll();
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

                if (this.gameObject.name == "GFTrigger")
                {
                    StartCoroutine(TheEnd());
                    player.GetComponent<CharacterMovement>().enabled = false;
                    player.GetComponent<NavMeshAgent>().enabled = false;
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

    public IEnumerator TheEnd()
    {
        bgMusic.Stop();
        bgMusic.clip = audioClips[2];
        bgMusic.Play();
        
        player.GetComponent<Animator>().SetBool("isWalking", false);
        
        subtitle.enabled = true;
        subtitle.text = dialogues[0];
        customSource.clip = audioClips[0];
        customSource.Play();
        
        yield return new WaitForSeconds(audioClips[0].length + 0.3f);
        
        subtitle.text = dialogues[1];
        customSource.clip = audioClips[1];
        customSource.Play();
        
        yield return new WaitForSeconds(10);
        isItEnd = true;
    }
}
