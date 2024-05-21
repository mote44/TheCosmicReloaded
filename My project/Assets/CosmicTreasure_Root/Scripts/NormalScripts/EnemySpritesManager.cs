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
        //Debug.Log("FATHERZ " +fatherRotZ);
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


        if(fatherRotZ >= -45f / 180 && fatherRotZ <= 45f / 180 )//if (direction.y > 0) //NORTH   //-0.25 / 0.25      
        {
            anim.SetFloat("Move_Y", 1f);

            if ((fatherRotZ > 22f / 180) && (fatherRotZ < 45f / 180)) //east         
            {
                Debug.Log("NE");
                
                anim.SetFloat("MoveX", 1);

            }
            else if ((fatherRotZ < -22f / 180) && (fatherRotZ > -45f / 180))//west
            {
                Debug.Log("NW"); //Lo contrario de east es west
               
                anim.SetFloat("MoveX", -1);

            }
            else if ((fatherRotZ >= -22f / 180) && (fatherRotZ <=22 / 180)) { anim.SetFloat("MoveX", 0);  Debug.Log("N"); } //Full North
        }
        


        
        
        else if ((fatherRotZ >= 135 / 180 && fatherRotZ < 180/180) || (fatherRotZ <= -135 / 180 && fatherRotZ > -180/180)) //SOUTH  //0.75 / 1 || -1 /-0.75
        {
            
            anim.SetFloat("Move_Y", -1f);
           
            if ((fatherRotZ > 135f / 180) && (fatherRotZ < 157 / 180)) //east
            {
                Debug.Log("SE");
                anim.SetFloat("MoveX", 1);
                

            }
            else if ((fatherRotZ < -135f / 180) && (fatherRotZ > -157f / 180)) //West
            {

                Debug.Log("SW");
                anim.SetFloat("MoveX", -1);
                
            }
            else if ((fatherRotZ <= -157f / 180) && (fatherRotZ >= -180f / 180) || (fatherRotZ > 157f / 180) && (fatherRotZ <= 180f / 180)) { anim.SetFloat("MoveX", 0); } //Full South
        }








        else if (fatherRotZ > 45f / 180 && fatherRotZ< 135f / 180)//if (direction.y > 0) //EAST         //0.25 / 0.75
        {
            anim.SetFloat("MoveX", 1f);

            if ((fatherRotZ > 45f / 180) && (fatherRotZ <= 67f / 180)) //north       
            {
                //Debug.Log("Entra animacion");
                Debug.Log("EN");
                anim.SetFloat("Move_Y", 1);


            }
            else if ((fatherRotZ < 135f / 180) && (fatherRotZ >= 112f / 180))//south
            {
                //Debug.Log("Entra animacion neutral"); //Lo contrario de east es west
                Debug.Log("ES");
                anim.SetFloat("Move_Y", -1);


            }
            else if ((fatherRotZ < 135f / 180) && (fatherRotZ > 45f / 180)) { anim.SetFloat("Move_Y", 0); } //Full East
        }


       
        else if (fatherRotZ > -135f / 180 && fatherRotZ< -45f / 180)//if (direction.y > 0) //WEST         //-0.75 / -0.25
        {
            anim.SetFloat("MoveX", -1f);

            if ((fatherRotZ < -45f / 180) && (fatherRotZ >= -67f / 180)) //north         
            {
                //Debug.Log("Entra animacion");
                Debug.Log("WN");
                anim.SetFloat("Move_Y", 1);


            }
            else if ((fatherRotZ > -135f / 180) && (fatherRotZ <= -112f / 180))//south
            {
                //Debug.Log("Entra animacion neutral"); //Lo contrario de east es west
                Debug.Log("WS");
                anim.SetFloat("Move_Y", -1);


            }
            else if ((fatherRotZ > -135f / 180) && (fatherRotZ <= -45f / 180)) { anim.SetFloat("Move_Y", 0); }  //Full West
        }

       

    }
}
