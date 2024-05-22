using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeCollider : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ESCAPE!!!");
            AudioManager.instance.PlaySFX(46);
            winPanel.SetActive(true);
        }
    }
   
}