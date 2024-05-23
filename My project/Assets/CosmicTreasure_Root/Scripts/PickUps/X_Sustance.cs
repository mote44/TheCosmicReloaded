using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X_Sustance : MonoBehaviour
{
    public GameObject win;
    [SerializeField] WalkieTalkie01 walkie;
    [SerializeField] GameObject escapeText;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //AudioManager.instance.PlaySFX(46);
            //win.SetActive(true);
            gameObject.SetActive(false);
            walkie.currentAudio = 1;
            walkie.StartCoroutine("WalkieRoutine");
            escapeText.GetComponent<SpriteRenderer>().color = Color.green;

            //Invoke("MusicEnd", 0.7f);
        }
    }

    private void MusicEnd()
    {
        AudioManager.instance.PlaySFX(50);
    }
}
