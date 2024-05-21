using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X_Sustance : MonoBehaviour
{
    public GameObject win;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(46);
            win.SetActive(true);
        }
    }
}
