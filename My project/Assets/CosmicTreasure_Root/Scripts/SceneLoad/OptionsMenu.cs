using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    private bool isOpen;
    private bool isClose;

    private void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && !isOpen)
        {
            optionsPanel.SetActive(true);
            isOpen = true;
            isClose = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            optionsPanel.SetActive(false);
            isOpen = false;
            isClose = true;
        }
    }
}
