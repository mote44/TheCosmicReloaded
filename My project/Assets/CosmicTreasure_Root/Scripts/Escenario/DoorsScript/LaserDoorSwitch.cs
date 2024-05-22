using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LaserDoorSwitch : MonoBehaviour
{

    Light2D switchLight;
    Animator anim;
    Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        switchLight = GetComponentInChildren<Light2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //Debug.Log("LASERSWITCH");
            anim.Play("LaserDoorSwitchAnim");
            GameManager.Instance.LaserButtonClick();
            AudioManager.instance.PlaySFX(25);
            switchLight.color = Color.green;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
