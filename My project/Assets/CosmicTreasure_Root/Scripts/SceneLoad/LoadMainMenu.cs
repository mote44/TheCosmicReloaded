using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMainMenu : MonoBehaviour
{
    public GameObject optPanel;
    private void Start()
    {
        //MainMenu();

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ResumePlay()
    {
        optPanel.SetActive(false);
    }
}
