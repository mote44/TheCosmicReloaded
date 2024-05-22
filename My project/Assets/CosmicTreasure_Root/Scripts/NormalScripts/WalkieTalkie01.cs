using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WalkieTalkie01 : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sprite;
    Light2D walkieLight;
    public int currentAudio;
    [SerializeField] int[] audioList;
    [SerializeField] int[] audioDuration;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        walkieLight = GetComponentInChildren<Light2D>();
        sprite.color = new Color(1, 1, 1, 0);
        walkieLight.intensity = 0;
        StartCoroutine("WalkieRoutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator WalkieRoutine(/*int[] audioList, int[] audioDuration, int currentAudio*/)
    {
        AudioManager.instance.PlaySFX(audioList[currentAudio]);
        sprite.color = new Color(1, 1, 1, 1);
        anim.Play("WalkieAnim");
        walkieLight.intensity = 1;
        yield return new WaitForSeconds(audioDuration[currentAudio]);
        sprite.color = new Color(1, 1, 1, 0);
        walkieLight.intensity = 0;
    }
}
