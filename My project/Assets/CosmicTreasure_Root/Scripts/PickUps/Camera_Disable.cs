using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Disable : MonoBehaviour
{
    public PlayerController controller;

    public FOVPoint fov;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void CameraOff()
    {
        if (controller.isInRange)
        {
            //La camara deja de moverse y no puede detectar al jugador
            //AudioManager.instance.PlaySFX(21);
            //AudioManager.instance.PlaySFX(1);
            fov.range = 0.1f;
            fov.rotationSpeed = 0.0f;

        }
    }
}
