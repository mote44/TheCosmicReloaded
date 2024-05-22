using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RedOpenSystem : MonoBehaviour
{
    Animator anim;
    Collider2D col;
    //[SerializeField] GameObject roof;
    //SpriteRenderer shapeRenderer;
    [SerializeField] GameObject closeCol;
    [SerializeField] int keyToOpen;
    [SerializeField] Light2D doorLight;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInParent<Animator>();
        col = gameObject.GetComponent<Collider2D>();
        //shapeRenderer = roof.GetComponent<SpriteRenderer>();
        doorLight.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.keyChain[keyToOpen] == true) { doorLight.intensity = 1; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.keyChain[keyToOpen] == true)
        {
            anim.Play("DoorRedOpenAnim");
            closeCol.GetComponent<Collider2D>().enabled = false;
            AudioManager.instance.PlaySFX(28);
            //shapeRenderer.color = new Color(1f, 1f, 1f, 0f);         //Apaga el techo


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.keyChain[keyToOpen] == true)
        {
            anim.Play("DoorRedCloseAnim");
            AudioManager.instance.PlaySFX(28);
        }
    }
}
