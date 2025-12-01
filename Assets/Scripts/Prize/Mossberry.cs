using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mossberry : MonoBehaviour
{
 	public AudioClip[] audioClips;
    private AudioSource audioSource;
	private bool playerInside = false;
    public GameObject berry;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
			audioSource.clip = audioClips[0];
        	audioSource.Play();
            gameObject.SetActive(false);
            berry.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
