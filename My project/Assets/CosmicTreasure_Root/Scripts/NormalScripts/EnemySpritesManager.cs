using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpritesManager : MonoBehaviour
{

    EnemyTest enemyTest;
    Animator anim;
    Vector3 direction;
    [SerializeField] Transform target;
    Transform father;
    float fatherRotZ;
    private SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = rb.velocity;
        Debug.Log("direction " + direction);
        
        father = GetComponentInParent<Transform>();
        fatherRotZ = father.localRotation.z;
        Debug.Log("FATHERZ " +fatherRotZ);
        GetSpriteDirection();
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,-fatherRotZ);
        HandleSpriteFlip();
    }

    void HandleSpriteFlip()
    {
        if (!spriteRenderer.flipX && direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (spriteRenderer.flipX && direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }

    }

    void GetSpriteDirection()
    {


        if((fatherRotZ > -90 /360)|| (fatherRotZ < 90 / 360))//if (direction.y > 0) //North         //-0.25 / 0.25
        {
            anim.SetFloat("MoveY", 1);
            if ((fatherRotZ > 0.1f / 360) && (fatherRotZ < 180f / 360)) //east or west
            {
                Debug.Log("Entra animacion");
                //anim.SetBool("Mov_NE", true);
                //anim.SetBool("Mov_N", false);
                //anim.SetBool("Mov_SE", false);
                //anim.SetBool("Mov_S", false);
                //anim.SetBool("Mov_E", false);
                anim.SetFloat("MoveX", 1);
                

            }
            else if ((fatherRotZ < -0.1f / 360) && (fatherRotZ > -180f / 360))//neutral X
            {
                Debug.Log("Entra animacion neutral"); //Lo contrario de east es west
                //anim.SetBool("Mov_NE", false);
                //anim.SetBool("Mov_N", true);
                //anim.SetBool("Mov_SE", false);
                //anim.SetBool("Mov_S", false);
                //anim.SetBool("Mov_E", false);
                anim.SetFloat("MoveX", -1);
                

            }
        }
        else if ((fatherRotZ > 90 / 360) || (fatherRotZ < -90 / 360)) //South
        {
            anim.SetFloat("MoveY", -1);
            if ((fatherRotZ > 0.1f / 360) && (fatherRotZ < 180f / 360)) //east or west
            {
                //anim.SetBool("Mov_NE", false);
                //anim.SetBool("Mov_N", false);
                //anim.SetBool("Mov_SE", true);
                //anim.SetBool("Mov_S", false);
                //anim.SetBool("Mov_E", false);
                anim.SetFloat("MoveX", 1);
                

            }
            else if ((fatherRotZ < 0.1f / 360) && (fatherRotZ < -180f / 360))
            {
                //anim.SetBool("Mov_NE", false);
                //anim.SetBool("Mov_N", false);
                //anim.SetBool("Mov_SE", false);
                //anim.SetBool("Mov_S", true);
                //anim.SetBool("Mov_E", false);
                anim.SetFloat("MoveX", -1);
                
            }
        }
        /*else //neutral
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                anim.SetBool("Mov_NE", false);
                anim.SetBool("Mov_N", false);
                anim.SetBool("Mov_SE", false);
                anim.SetBool("Mov_S", false);
                anim.SetBool("Mov_E", true);
            }
        }*/

    }
}
