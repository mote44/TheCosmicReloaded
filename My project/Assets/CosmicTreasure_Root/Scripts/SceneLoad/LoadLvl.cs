using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvl : MonoBehaviour
{
    public GameObject retry;

    public AudioSource audioSource;

    private void Start()
    {
        //MainMenu();
        audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    public void Retry()
    {
        SceneManager.LoadScene("Level01");
        audioSource.Stop();
        retry.SetActive(false);
    }
}
