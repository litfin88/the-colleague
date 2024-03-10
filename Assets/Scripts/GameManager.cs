using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentBuildIndex;
    bool isTimeOver = false;

    public string[] dialogues;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private TMP_Text tipText;
    
    private Animator mainCharAnim;
    private Animator womanCharAnim;
    private Animator kazimAnim;
    private GameObject player;
    private CharacterMovement charMove;
    
    private TMP_Text subtitleText;

    private int dialogueStage;
    bool is3SOver = false;
    bool firstTime = true;
    private CharacterMovement _characterMovement;
    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        charMove = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
        mainCharAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        womanCharAnim = GameObject.FindWithTag("Girlfriend").GetComponent<Animator>();
        kazimAnim = GameObject.FindWithTag("Kazim").GetComponent<Animator>();
        subtitleText = GameObject.Find("Subtitle").GetComponent<TMP_Text>();
        player = GameObject.FindWithTag("Player");
        _navMeshAgent = player.GetComponent<NavMeshAgent>();
        _characterMovement = player.GetComponent<CharacterMovement>();
        tipText = GameObject.Find("TipText").GetComponent<TMP_Text>();
        
        switch (currentBuildIndex)
        {
            case 0:
                
                StartCoroutine(ReadText(3));
                _navMeshAgent.enabled = false;
                _characterMovement.enabled = false;
                if (isTimeOver)
                {
                    
                }
                break;
            
            case 1:
                StartCoroutine(SecondScene());
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentBuildIndex)
        {
            case 0:
                break;
            
            case 1:
                break;
        }
    }

    public IEnumerator SecondScene()
    {
        player.GetComponent<CharacterMovement>().enabled = false;
        player.GetComponent<NavMeshAgent>().enabled = false;
        
        StartCoroutine(ReadText(5));
        yield return new WaitForSeconds(1);
        player.GetComponent<CharacterMovement>().enabled = true;
        player.GetComponent<NavMeshAgent>().enabled = true;
    }

    public IEnumerator ReadText(int maxStage)
    {
        subtitleText.enabled = true;
        subtitleText.text = dialogues[dialogueStage];
        audioSource.clip = audioClips[dialogueStage];
        audioSource.Play();
        
        if(dialogues[dialogueStage].Split(":")[0] == "Kazım")
        {
            kazimAnim.SetBool("isTalking", true);
        }
        
        if(charMove.situation == currentSituation.Sitting)
        {
            if(dialogues[dialogueStage].Split(":")[0] == "Utku")
            {
                mainCharAnim.SetBool("isTalking", true);
            }
            if(dialogues[dialogueStage].Split(":")[0] == "Mehtap")
            {
                womanCharAnim.SetBool("isTalking", true);
            }
        }
        
        yield return new WaitForSeconds(audioSource.clip.length + 0.3f);

        if(dialogues[dialogueStage].Split(":")[0] == "Kazım")
        {
            kazimAnim.SetBool("isTalking", false);
        }
        
        if (charMove.situation == currentSituation.Sitting)
        {
            if (dialogues[dialogueStage].Split(":")[0] == "Utku")
            {
                mainCharAnim.SetBool("isTalking", false);
            }

            if (dialogues[dialogueStage].Split(":")[0] == "Mehtap")
            {
                womanCharAnim.SetBool("isTalking", false);
            }
        }

        dialogueStage++;
        if (dialogueStage < maxStage)
        {
            StartCoroutine(ReadText(maxStage));
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            subtitleText.enabled = false;

            if (is3SOver == false)
            {
                switch (currentBuildIndex)
                {
                    case 0:
                        StartCoroutine(After3S(6));
                        break;
            
                    case 1:
                        break;
                }
            }
            else if(firstTime)
            {
                tipText.text = "Keşif yapmak için tıklayarak hareket edin.";
                tipText.enabled = true;
                _navMeshAgent.enabled = true;
                _characterMovement.enabled = true;
                firstTime = false;
            }
        }
    }
    
    public IEnumerator After3S(int i)
    {
        yield return new WaitForSeconds(2);
        is3SOver = true;
        StartCoroutine(ReadText(i));
    }
}
