using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEnemy : MonoBehaviour
{
    EnemyTest enem;

    // Start is called before the first frame update
    void Start()
    {
        if (enem.isChasing)
        {
            AudioManager.instance.PlaySFX(18);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
