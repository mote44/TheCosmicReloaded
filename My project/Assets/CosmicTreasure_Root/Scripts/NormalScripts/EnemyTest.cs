using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class EnemyTest : MonoBehaviour
{
    //CODEAR: ARREGLAR EL RANDOM PATROL, ANIMACION MUERTE PLAYER, QUE USE EL PATHFINDING PARA EL PATROL NORMAL
    // QUE ESPERE 3 SEG ENTRE PUNTO Y PUNTO EN EL PATROL, ARREGLAR LOS ESTADOS CUANDO HAY MÁS DE UN ENEMIGO

    //Variables para los sonidos
    private bool isPlayed = false;
    private bool isPlayedPatrol = false;
    private bool isPlayedShot = false;

    EnemyDetectionSystem detection;
     public EnemyTest[] enem;
    public GameObject lose;

    PlayerController playerController;

    private Vector2 moveDirection;
    public Rigidbody2D rb;

    public NavMeshAgent agent;
    public enum EnemyState { patroling, randomPatroling, chasing, attacking, hearing}
    Vector2 dir;
    public Vector2 direction;
    public Animator anim;

    [Header("Alert Enemies")]          //Que el enemigo alerte a sus compañeros
    public float radiusAlert;

    [Header("Enemy Attributes")]
    public float fireRate = 1f;  //PARA QUE NO TE TIRE UNA RAFAGA DE TIROS
    private float atkCD;  //CoolDown
    public GameObject bullet;
    [SerializeField] GameObject bulletParent;
    [SerializeField] float attackDistance;

    [Header("Rotation Fov")]
    public float speedRotation;
    public float rotationModifier;

    [Header("Fov Point")]
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range = 8f;
    public LayerMask playerLayer;
    Light2D lightFov;

    [Header("Patrol")]
    private int i;
    [SerializeField] private float speedMovement;
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private int startingPoint;
    /*
    [SerializeField] private float minimumDistance;
    private int randomNumber;
    private SpriteRenderer spriteRenderer;
    */

    [Header("Random Patrol")]
    public float radius;
    public Transform centrePoint;
    public bool isDetected;         //Define cuando te ha detectado el enemigo

    [Header("States Enemy")]
    [SerializeField] EnemyState currentState;
    public bool isChasing;
    public bool isShooting;
    public bool isPatroling;
    public bool isHearing;

    public GameObject player;

    public Transform target;

    

    [SerializeField] PlayerController controller;

    private void Start()
    {
        anim = GetComponent<Animator>();
        currentState = EnemyState.patroling;
        isDetected = false;
        detection = player.GetComponentInChildren<EnemyDetectionSystem>();
        lightFov = GetComponentInChildren<Light2D>();

        //PATHFINDING
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //PATROL
        transform.position = movementPoints[startingPoint].position;
        //randomNumber = Random.Range(0, movementPoints.Length);
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //direction = target.transform.position - transform.position;
        //HandleSpriteFlip();             //Flipear Sprites
        //GetSpriteDirection();           //Cambia de Sprites segun la direccion

        /*
        if (!isChasing && !isShooting && !isHearing) { currentState = EnemyState.patroling; }
        if (!isShooting && isHearing) { currentState = EnemyState.hearing; }
        // if (!isChasing && !isShooting && !isPatroling) { currentState = EnemyState.randomPatroling; }
        if (isChasing && !isShooting) { currentState = EnemyState.chasing; }
        if (isChasing && isShooting) { currentState = EnemyState.attacking; }
        */

        //anim.SetFloat("MoveX", moveDirection.x);
        //anim.SetFloat("MoveY", moveDirection.y);

        if (!isChasing && !isShooting && !isHearing) { currentState = EnemyState.patroling; }   //Normal
        if(isChasing && !isShooting) { currentState = EnemyState.chasing; }                     //Si esta persiguiendo, no en distanciaShoot
        if(!isChasing && !isShooting && isHearing) { currentState = EnemyState.hearing; }
        if(isChasing && isShooting) { currentState = EnemyState.attacking; }
        if(isChasing && isHearing && !isShooting) { currentState = EnemyState.chasing; }
        if (isChasing && isHearing && isShooting) { currentState = EnemyState.attacking; }


        

        float distanceFromPlayer = Vector2.Distance(target.position, transform.position);  //Distancia con el Player

        //RAYCAST
        dir = target.position - transform.position;
        float angle = Vector3.Angle(dir, fovPoint.up);
        RaycastHit2D r = Physics2D.Raycast(fovPoint.position, dir, range, playerLayer);

        if (angle < fovAngle / 2)
        {
            if(r.collider != null)
            {
                if (r.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("TE HE VISTO CABRÓN");


                    if (distanceFromPlayer > attackDistance)
                    {

                        isChasing = true;
                        //AudioManager.instance.PlaySFX(18);
                        Debug.Log("Detecto al player");

                        if (!isPlayed)
                        {
                            //AudioManager.instance.PlaySFX(18);
                            AudioManager.instance.PlaySFX(24);
                            isPlayedPatrol = false;
                            isPlayed = true;
                        }
                    }

                    else
                    {
                        isHearing = false;
                        isShooting = true;
                        lose.SetActive(true);
                        Debug.Log("Estoy atacando");

                        if (!isPlayedShot)
                        {
                            AudioManager.instance.PlaySFX(16);
                            AudioManager.instance.PlaySFX(47);
                            isPlayedShot = true;
                        }
                    }

                    // Todos los enemigos con el tag "enemy" && que hagan trigger con el área de la habitación, pasan a modo chasing (isChasing = true)
                    // Si el raycast del enemigo ya no detecta al player (on trigger exit) todos pasan a Random Patrol

                }
                else
                {
                    isChasing = false;
                    isShooting = false;
                    isPlayed = false;
                    Debug.Log("We dont seen");
                    if (!isPlayedPatrol)     //No lo hace
                    {
                        AudioManager.instance.PlaySFX(13);
                        isPlayedPatrol = true;
                    }
                }
            }
            

            
        }
        /*
        if (isHearing)
        {
            detection.lastPosition = player.transform.position;
        }
        */
        EnemyStateManagement();
    }

    private void AlertEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radiusAlert);
        foreach (Collider2D col in hitColliders)
        {
            
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (col.gameObject.CompareTag("Enemy"))
                {
                    col.GetComponent<EnemyTest>().isChasing = true; //Entra pero se atrampa
                    //col.GetComponent<EnemyTest>().isPatroling = false;
                   enem[i] = col.gameObject.GetComponent<EnemyTest>();
                }
                Invoke("ToPatrol", 6);
            }
           
        }
    }

    void ToPatrol()
    {
        for (int i = 0; i < enem.Length;i++)
        {
            enem[i].isChasing = false;
            enem[i].isPatroling = true;
        }
    }

    void OnDrawGizmosSelected()      //Gizmo del radio del enemigo
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAlert);
    }


    private void RandomPatrol()
    { 
            if (agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
                Vector3 point;

                if (RandomPoint(centrePoint.position, radius, out point)) //pass in our centre point and radius of area
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    agent.SetDestination(point);
                }
            }
    }
    

    void EnemyStateManagement()
    {
        switch (currentState)
        {
            case EnemyState.patroling:
                Patrol();
                    break;
                
            case EnemyState.chasing:
                ChasePlayer(player.transform.position);
                break;
                
            case EnemyState.attacking:
                EnemyAttack();
                break;

            case EnemyState.randomPatroling:
                RandomPatrol();
                break;

            case EnemyState.hearing:
                ChasePlayerSound(detection.lastPosition);
                break;

        }
            
    }

    private void Patrol()
    {
        //agent.SetDestination(movementPoints[0]);
        agent.SetDestination(transform.position);
        LookAt(movementPoints[i].transform);
        lightFov.color = Color.green;
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

        agent.SetDestination(movementPoints[i].position);
        //transform.position = Vector2.MoveTowards(transform.position, movementPoints[i].position, speedMovement * Time.deltaTime);
    }

    public void ChasePlayer(Vector2 target)
    {
        isPatroling = false;
        AlertEnemies();
        //Aquí NO puede ir isHearing = false
        LookAt(player.transform);
        agent.SetDestination(target);
        Debug.Log("SEEN PLAYER!");
        lightFov.color = Color.red;
        //Debug.DrawRay(fovPoint.position, dir, Color.red);
    }

    public void ChasePlayerSound(Vector2 target)
    {
        isPatroling = false;
        LookAt(player.transform);
        agent.SetDestination(target);
        Debug.Log("HEAR PLAYER!");
        lightFov.color = Color.yellow;
        //Debug.DrawRay(fovPoint.position, dir, Color.red);
    }

    void EnemyAttack()
    {
        Debug.Log("ENTRA TIRO");
        isChasing = false;
        isPatroling = false;
        isHearing = false;
        agent.SetDestination(transform.position);
        LookAt(player.transform);
        controller.walkSpeed = 0f;
        controller.walkSpeedStealth = 0f;
        //Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;          //QUE EL PLAYER PASE A ESTATICO
        //atkCD = fireRate;
        //GameObject newBullet = Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
        //Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
    }

    /*
    private void Rotate()
    {
        if (transform.position.x < movementPoints[randomNumber].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    */

    public void LookAt(Transform target)
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float anglee = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(anglee, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speedRotation);
    }

    bool RandomPoint(Vector3 center, float radius, out Vector3 result)            //RANDOM PATROL
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * radius; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void FixedUpdate()
    {
        if (atkCD > 0)
        {
            atkCD -= Time.deltaTime;

        }

    }

    /*
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
    */

   /* void GetSpriteDirection()
    {


        if (direction.y > 0) //North
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                anim.SetBool("Mov_NE", true);
                anim.SetBool("Mov_N", false);
                anim.SetBool("Mov_SE", false);
                anim.SetBool("Mov_S", false);
                anim.SetBool("Mov_E", false);

            }
            else //neutral X
            {
                anim.SetBool("Mov_NE", false);
                anim.SetBool("Mov_N", true);
                anim.SetBool("Mov_SE", false);
                anim.SetBool("Mov_S", false);
                anim.SetBool("Mov_E", false);
            }
        }
        else if (direction.y < 0) //South
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                anim.SetBool("Mov_NE", false);
                anim.SetBool("Mov_N", false);
                anim.SetBool("Mov_SE", true);
                anim.SetBool("Mov_S", false);
                anim.SetBool("Mov_E", false);
            }
            else
            {
                anim.SetBool("Mov_NE", false);
                anim.SetBool("Mov_N", false);
                anim.SetBool("Mov_SE", false);
                anim.SetBool("Mov_S", true);
                anim.SetBool("Mov_E", false);
            }
        }
        else //neutral
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                anim.SetBool("Mov_NE", false);
                anim.SetBool("Mov_N", false);
                anim.SetBool("Mov_SE", false);
                anim.SetBool("Mov_S", false);
                anim.SetBool("Mov_E", true);
            }
        }

    }*/
 }
