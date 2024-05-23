using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu2 : MonoBehaviour
{
    public GameObject optionsPanel;

    private void Start()
    {
        //MainMenu();
    }

    public void OptionsPanel()
    {
        optionsPanel.SetActive(true);
    }
}
