using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySFX(48);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke ("NextSceneAction", 51);
    }

    void NextSceneAction()
    {
        SceneManager.LoadScene(2);
    }
}
