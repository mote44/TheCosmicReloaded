using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDoorScript : MonoBehaviour
{
    SpriteRenderer sprite;
    [SerializeField] Sprite[] spriteList; 
    
    // Start is called before the first frame update
    void Start()
    {
       sprite = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.toOpenLaserDoor == 0)
        {
            sprite.sprite = spriteList[0];
        }

        if (GameManager.Instance.toOpenLaserDoor == 1)
        {
            sprite.sprite = spriteList[1];
        }
        if (GameManager.Instance.toOpenLaserDoor == 2)
        {
            sprite.sprite = spriteList[2];
        }
    }
}
