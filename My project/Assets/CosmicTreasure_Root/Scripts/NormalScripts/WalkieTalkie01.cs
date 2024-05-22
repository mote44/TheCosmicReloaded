using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WalkieTalkie01 : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sprite;
    Light2D walkieLight;
    private int audioDuration;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        walkieLight = GetComponentInChildren<Light2D>();
        sprite.color = new Color(1, 1, 1, 0);
        walkieLight.intensity = 0;
        StartCoroutine("WalkieRutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WalkieRutine()
    {
        AudioManager.instance.PlaySFX(42);
        sprite.color = new Color(1, 1, 1, 1);
        anim.Play("WalkieAnim");
        walkieLight.intensity = 1;
        yield return new WaitForSeconds(10);
        sprite.color = new Color(1, 1, 1, 0);
        walkieLight.intensity = 0;
    }
}
