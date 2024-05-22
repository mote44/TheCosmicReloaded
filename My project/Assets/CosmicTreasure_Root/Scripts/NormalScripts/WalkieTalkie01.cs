using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkieTalkie01 : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("WalkieRutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WalkieRutine()
    {
        AudioManager.instance.PlaySFX(42);
        anim.Play("WalkieAnim");
        yield return new WaitForSeconds(10);
    }
}
