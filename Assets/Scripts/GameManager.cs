using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int currentBuildIndex;
    bool isTimeOver = false;

    public string[] dialogues;
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private int dialogueStage;
    void Start()
    {
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
        
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
        Debug.Log(dialogues[dialogueStage]);
        audioSource.clip = audioClips[dialogueStage];
        audioSource.Play();
        
        yield return new WaitForSeconds(audioSource.clip.length + 0.3f);
        dialogueStage++;
        if (dialogueStage < maxStage)
        {
            StartCoroutine(ReadText(maxStage));
        }
    }
    
    public IEnumerator CountTime()
    {
        yield return new WaitForSeconds(60);
        
    }
}
