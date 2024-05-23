using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.Universal;
using UnityEngine.Scripting.APIUpdating;
using static EnemyTest;

public class PlayerController : MonoBehaviour
{

    LineRenderer lineRend;
    ParticleSystem soundAreaParticle;
    public FOVPoint fovCollisionCam;
    public Collider2D coliderFovCam;
    
    AudioSource soundSource;
    

    public Rigidbody2D body;

    public SpriteRenderer spriteRenderer;

    [SerializeField] Collider2D[] enemCol;
    public bool enemyDetection;
    [SerializeField]LayerMask enemyLayer;

    public enum PlayerState { normal, stealth }

    [Header("States Player")]
    [SerializeField] PlayerState currentState;
    public bool isNormal;
    public bool isStealth;

    [Header("Normal Animation")]
    public List<Sprite> nSprites;
    public List<Sprite> neSprites;
    public List<Sprite> eSprites;
    public List<Sprite> seSprites;
    public List<Sprite> sSprites;

    [Header("Stealth Animation")]
    public List<Sprite> nCSprites;
    public List<Sprite> neCSprites;
    public List<Sprite> eCSprites;
    public List<Sprite> seCSprites;
    public List<Sprite> sCSprites;

    [Header("Player Stats")]
    public float speedX = 20f;
    public float speedY = 3f;

    public float walkSpeed;

    public float walkSpeedStealth;

    public float frameRate;

    public float frameRateStealth;

    float idleTime;

    [Header("Camera Disable")]
    public float radiusDesactivation;
    public bool isInRange;
    public bool isClicked;
    private Light2D fovLight;

    //Variable para la mecanica del sonido
    private Transform enemy;
    public float lineOfSite;
    public EnemyTest chasing;


    public Vector2 direction;
   

    private void Start()
    {
        isNormal = true;
        //AudioManager.instance.PlaySFX(42);
        soundAreaParticle = GetComponentInChildren<ParticleSystem>();
        soundSource = GetComponent<AudioSource>();
        lineRend = GetComponent<LineRenderer>();
        //currentState = PlayerState.normal;
        //enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    private void Update()
    {
       
        
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")/2).normalized;

        //body.velocity = direction * walkSpeed;

        PlayerStateManagement();

        if (isNormal && !isStealth) { currentState = PlayerState.normal; }
        if (!isNormal && isStealth) { currentState = PlayerState.stealth; }

        if (Input.GetMouseButtonDown(1) && isNormal)//if (Input.GetKeyDown(KeyCode.F) && isNormal)
        {
            Debug.Log("Cambio a stealth");
            AudioManager.instance.PlaySFX(19);
            isNormal = false;
            isStealth = true;
        }
        else if (Input.GetMouseButtonDown(1) && !isNormal)//else if (Input.GetKeyDown(KeyCode.F) && !isNormal)
        {
            Debug.Log("Cambio a normal");
            isNormal = true;
            isStealth = false;
        }

        

        CameraDisable();     //FUNCIONA?????????


        /*
        float distanceFromEnemy = Vector2.Distance(enemy.position, transform.position);    //Cuando el enemigo entra en la zona del player, pasa a chasing
        if (distanceFromEnemy < lineOfSite && isNormal && direction != Vector2.zero)
        {
            chasing.isChasing = true;   // NO LO HACE SI HAY MAS DE UN ENEMIGO
        }
        */


        //HandleSpriteFlip();
        //SetSprite();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusDesactivation);      //Gizmo para la desactivacion de camaras
    }


    void SetSprite()
    {
        List<Sprite> directionSprites = GetSpriteDirection();

        if (directionSprites != null)
        { //holding a direction

            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * frameRate);
            int frame = totalFrames % directionSprites.Count;

            spriteRenderer.sprite = directionSprites[frame];
            //AudioManager.instance.PlaySFX(22);
        }
        else
        {
            idleTime = Time.time;
        }

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

    List<Sprite> GetSpriteDirection()
    {
        List<Sprite> selectedSprites = null;

        if (direction.y > 0) //North
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = neSprites;
            }
            else //neutral X
            {
                selectedSprites = nSprites;
            }
        }
        else if (direction.y < 0) //South
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = seSprites;
            }
            else
            {
                selectedSprites = sSprites;
            }
        }
        else //neutral
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = eSprites;
            }
        }

        return selectedSprites;

    }

    void SetSpriteStealth()
    {
        List<Sprite> directionSprites = GetSpriteDirectionStealth();

        if (directionSprites != null)
        { //holding a direction

            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * frameRateStealth);
            int frame = totalFrames % directionSprites.Count;

            spriteRenderer.sprite = directionSprites[frame];
        }
        else
        {
            idleTime = Time.time;
        }

    }

    void HandleSpriteFlipStealth()
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

    List<Sprite> GetSpriteDirectionStealth()
    {
        List<Sprite> selectedSprites = null;

        if (direction.y > 0) //North
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = neCSprites;
            }
            else //neutral X
            {
                selectedSprites = nCSprites;
            }
        }
        else if (direction.y < 0) //South
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = seCSprites;
            }
            else
            {
                selectedSprites = sCSprites;
            }
        }
        else //neutral
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = eCSprites;
            }
        }

        return selectedSprites;

    }

    private void Normal()
    {
        Debug.Log("Estoy en normal");
        /*
        float horizontalMovement = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalMovement * speedX * Time.deltaTime);

        float verticalMovement = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * verticalMovement * speedY * Time.deltaTime);
        body.velocity = direction * speedX;
        body.velocity = direction * speedY;
        */
        body.velocity = direction * walkSpeed;
        HandleSpriteFlip();
        SetSprite();
        soundAreaParticle.startSize = 4.4f;
        soundAreaParticle.startColor = Color.red;
        if ((body.velocity.x != 0 || body.velocity.y != 0))
        {
            
            soundSource.mute = false;
            if (soundAreaParticle.isStopped) { soundAreaParticle.Play(); }
        }
        else if ((body.velocity.x == 0 || body.velocity.y == 0)) { soundSource.mute = true; }

    }

    private void Stealth()
    {
        Debug.Log("Estoy en sigilo");
        body.velocity = direction * walkSpeedStealth;
        HandleSpriteFlipStealth();
        SetSpriteStealth();
        soundAreaParticle.startSize =2;
        soundAreaParticle.startColor = Color.cyan;
        soundSource.mute = true;
        if ((body.velocity.x != 0 || body.velocity.y != 0))
        {

            
            if (soundAreaParticle.isStopped) { soundAreaParticle.Play(); }
        }
    }

    void PlayerStateManagement()
    {
        switch (currentState)
        {
            case PlayerState.normal:
                Normal();
                break;

            case PlayerState.stealth:
                Stealth();
                break;

        }



    }

    private void CameraDisable()
    {

        lineRend.SetPosition(0, transform.position);
        lineRend.SetPosition(1, transform.position);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radiusDesactivation);
        foreach (Collider2D col in hitColliders)
        {

            if (col.gameObject.CompareTag("Camera"))
            {
                fovCollisionCam= col.gameObject.GetComponent<FOVPoint>();
                coliderFovCam = col.gameObject.GetComponent<Collider2D>();
                fovLight = col.gameObject.GetComponentInChildren<Light2D>();
                isInRange = true;
                lineRend.SetPosition(1, col.transform.position);
                
                if(isClicked)
                {
                    Debug.Log("ISCLICKED");
                    fovCollisionCam.range = .1f;
                    Debug.Log("FOVCAM.RANGE " + fovCollisionCam.range);
                    fovCollisionCam.rotationSpeed = 0;
                    coliderFovCam.enabled = false;
                    fovLight.enabled = false;
                    Invoke("CameraNormal", 7);
                }
                
            }
            
            
        }


    }

    private void CameraNormal()
    {
        isClicked = false;
        fovCollisionCam.range = 80;
        fovCollisionCam.rotationSpeed = 0.15f;
        coliderFovCam.enabled = true;
        fovLight.enabled = true;
    }
}
