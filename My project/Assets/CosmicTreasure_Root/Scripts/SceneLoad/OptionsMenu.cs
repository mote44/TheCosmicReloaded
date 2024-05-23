using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel; 

    void Update()
    {
        // Verifica si se presiona la tecla "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionsPanel.SetActive(true);
        }
    }
}
