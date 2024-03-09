using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int currentBuildIndex;
    bool isTimeOver = false;

    public string[] dialogues;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    
    private Animator mainCharAnim;
    private Animator womanCharAnim;
    
    private TMP_Text subtitleText;

    private int dialogueStage;
    void Start()
    {
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
        mainCharAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        womanCharAnim = GameObject.FindWithTag("Girlfriend").GetComponent<Animator>();
        subtitleText = GameObject.Find("Subtitle").GetComponent<TMP_Text>();
        
        switch (currentBuildIndex)
        {
            case 0:
                StartCoroutine(ReadText(4));
                if (isTimeOver)
                {
                    
                }
                break;
            
            case 1:
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

    public IEnumerator ReadText(int maxStage)
    {
        subtitleText.enabled = true;
        subtitleText.text = dialogues[dialogueStage];
        audioSource.clip = audioClips[dialogueStage];
        audioSource.Play();
        
        if(dialogues[dialogueStage].Split(":")[0] == "Utku")
        {
            mainCharAnim.SetBool("isTalking", true);
        }
        if(dialogues[dialogueStage].Split(":")[0] == "Mehtap")
        {
            womanCharAnim.SetBool("isTalking", true);
        }
        
        yield return new WaitForSeconds(audioSource.clip.length + 0.3f);
        
        if(dialogues[dialogueStage].Split(":")[0] == "Utku")
        {
            mainCharAnim.SetBool("isTalking", false);
        }
        if(dialogues[dialogueStage].Split(":")[0] == "Mehtap")
        {
            womanCharAnim.SetBool("isTalking", false);
        }
        
        dialogueStage++;
        if (dialogueStage < maxStage)
        {
            StartCoroutine(ReadText(maxStage));
        }
        else
        {
            yield return new WaitForSeconds(1f);
            subtitleText.enabled = false;
        }
    }
    
    public IEnumerator CountTime()
    {
        yield return new WaitForSeconds(60);
        
    }
}
