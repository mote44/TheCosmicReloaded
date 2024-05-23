using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvl : MonoBehaviour
{
    public GameObject retry;

    private void Start()
    {
        //MainMenu();
    }

    public void Retry()
    {
        SceneManager.LoadScene("Level01");
        retry.SetActive(false);
    }
}
