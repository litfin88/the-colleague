using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CamTracker : MonoBehaviour
{
    private GameObject pivot;
    private GameObject player;

    public static bool isDartTriggered;
    private GameObject dartPivot;
    
    private int currentBuildIndex;
    public GameObject dart;
    
    // Start is called before the first frame update
    void Start()
    {
        pivot = GameObject.Find("CamPivot");
        player = GameObject.FindWithTag("Player");
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        
        if(currentBuildIndex == 1)
        {
            dartPivot = GameObject.Find("DartPivot");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            pivot.transform.position = Vector3.Lerp(
                pivot.transform.position, 
                player.transform.position + new Vector3(0, 1f, 0), 
                0.03f);
        
            this.gameObject.transform.LookAt(pivot.transform);
        }
        else
        {
            if (isDartTriggered)
            {
                this.gameObject.transform.position = dartPivot.transform.position;
                player.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
                
                float x = Input.GetAxis("Mouse X") * Time.deltaTime * 150.0f;
                float y = Input.GetAxis("Mouse Y") * Time.deltaTime * 150.0f;

                if (PlayerPrefs.GetInt("Shot") == 1)
                {
                     transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - y + (Random.Range(-0.2f, 0.2f)), transform.rotation.eulerAngles.y + x + + (Random.Range(-0.2f, 0.2f)), 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - y, transform.rotation.eulerAngles.y + x, 0);
                }
                
                if (Input.GetButtonDown("Fire1"))
                {
                    Instantiate(dart, dartPivot.transform.position, transform.rotation);
                }
                
            }
            else
            {
                pivot.transform.position = Vector3.Lerp(
                    pivot.transform.position, 
                    player.transform.position + new Vector3(0, 1f, 0), 
                    0.03f);
                this.gameObject.transform.position = pivot.transform.position + new Vector3(0, 9, 0);
            }
        }

    }
}
