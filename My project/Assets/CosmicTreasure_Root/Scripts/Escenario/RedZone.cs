using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZone : MonoBehaviour
{
    private int i;
    [SerializeField] private float speedMovement;
    private float initialSpeed;
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private int startingPoint;
    [SerializeField] Transform playerPos;
    [SerializeField] bool isDetected;
    EnemyTest enem;

    public float radiusAlert; 

    private void Start()
    {
        initialSpeed=speedMovement;
        isDetected = false; 
        
    }

    private void Update()
    {
        if (!isDetected)
        {
            if (Vector2.Distance(transform.position, movementPoints[i].position) < 0.02f)
            {
                i++; //Aumenta el índice, cambia de objetivo hacia el que moverse.
                Debug.Log("i " + i);
                //  ESPERA 3 SEGUNDOS ANTES DE IR AL SIGUIENTE PUNTO
                if (i == movementPoints.Length)
                {

                    i = 0;
                    //transform.position = movementPoints[startingPoint].position;


                }
            }

            transform.position = Vector2.MoveTowards(transform.position, movementPoints[i].position, speedMovement * Time.deltaTime);
        }

        else if (isDetected && playerPos.position.x > movementPoints[0].position.x && playerPos.position.x < movementPoints[1].position.x)
        {
            transform.position = playerPos.position;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && (collision.GetComponent<PlayerController>().direction.x != 0 || collision.GetComponent<PlayerController>().direction.y != 0))
        {
            isDetected = true;
            AlertEnemies();
            Debug.Log("PlayerDetectedByLaser");
            //speedMovement = 0;
           

            //transform.position = playerPos.position;
           
        }
    }

    private void AlertEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radiusAlert);
        foreach (Collider2D col in hitColliders)
        {

            if (col.gameObject.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyTest>().isChasing = true;
                enem = col.GetComponent<EnemyTest>();
                Invoke("ToPatrol", 6);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDetected = false;
        speedMovement = initialSpeed;
    }

    void OnDrawGizmosSelected()      //Gizmo del radio del enemigo
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAlert);
    }

    void ToPatrol()
    {
        enem.isPatroling = true;
    }
}

