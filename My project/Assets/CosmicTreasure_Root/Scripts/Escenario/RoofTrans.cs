using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofTrans : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private float transparency;
    [SerializeField] int keyNeeded;
    // Start is called before the first frame update
    void Start()
    {
       sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.keyChain[keyNeeded]==true) { sprite.color = new Color(1, 1, 1, transparency); }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { sprite.color = new Color(1, 1, 1, 1); }
    }
}
