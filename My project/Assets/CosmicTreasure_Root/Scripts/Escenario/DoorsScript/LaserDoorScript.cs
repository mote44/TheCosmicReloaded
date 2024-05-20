using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LaserDoorScript : MonoBehaviour
{
    SpriteRenderer sprite;
    Collider2D col;
    [SerializeField] Sprite[] spriteList;
    Light2D laserLight;
    
    // Start is called before the first frame update
    void Start()
    {
       sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        laserLight = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.toOpenLaserDoor == 0)
        {
            sprite.sprite = spriteList[0];
            laserLight.intensity = 2;

        }

        if (GameManager.Instance.toOpenLaserDoor == 1)
        {
            sprite.sprite = spriteList[1];
            laserLight.intensity = 1;
        }
        if (GameManager.Instance.toOpenLaserDoor == 2)
        {
            sprite.sprite = spriteList[2];
            col.isTrigger = true;
            laserLight.color = new Color(0, 1, 0, 1);
            
        }
    }
}
