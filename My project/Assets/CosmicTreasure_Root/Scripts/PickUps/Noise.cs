using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public EnemyTest enem;
    public Transform target;
    public GameObject noisePrefab;
    public GameObject enemy;
    public float speed;
    private float fireNoise = 0;
    public bool isDropped;
    public GameObject droppedNoiseInstancia;
    [SerializeField] private float radius;  //Usar el overlapsphere

    public Transform firePoint;

    EnemyTest change;

    // Start is called before the first frame update
    void Start()
    {
        isDropped = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Alpha1)) //&& equippedNoise)
        {
            NoiseAction();
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            NoiseAction2();
        }
        */
    }

    public void NoiseAction()   //Instancia el dispositivo
    {
        GameObject bullet = Instantiate(noisePrefab, firePoint.position, firePoint.rotation);
        droppedNoiseInstancia = bullet;
        droppedNoiseInstancia.GetComponent<Noise_Alert>().noise = gameObject.GetComponent<Noise>(); //intento pasar este script como referencia(noise) a otro script
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireNoise*2, ForceMode2D.Impulse);
        AudioManager.instance.PlaySFX(36);
        isDropped = true;
        //AlertEnemies();


    }

    /*
    public void NoiseAction2()   //Activa el dispositivo y los enemigos van a �l
    {
        AudioManager.instance.PlaySFX(20);   //QUE SUENE EN LOOP

        GameObject.FindGameObjectsWithTag("Enemy");
        enem.agent.SetDestination(target.transform.position);
        //enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, noise.transform.position, speed * Time.deltaTime);
        Invoke("Return", 10);
        
    }

    private void Return()  //El objeto luego de unos segundos se destruye
    {
        enem.isPatroling = true;
        Destroy(gameObject);
    }


    private void AlertEnemies()
    {
        
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D col in hitColliders)
            {

                if (col.gameObject.CompareTag("Enemy"))
                {
                   NoiseAction2();
                }
            }
        

    }

    void OnDrawGizmosSelected()      //Gizmo del radio del enemigo
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    */
}
