using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise_Alert : MonoBehaviour
{
    public Noise noise;
    public EnemyTest enem;
    public Transform target;
    ParticleSystem part;
    [SerializeField] private float radius;

    private void Start()
    {
        
        //noise = GetComponent<Noise>();
        //ComeEnemy();
        part = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        ComeEnemy();
    }

    public void ComeEnemy()
    {
        if (noise.isDropped )
        {
            {

                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
                foreach (Collider2D col in hitColliders)
                {

                    if (col.gameObject.CompareTag("Enemy"))
                    {
                        Debug.Log("TE PILLO EL SCRIPT");
                        enem = col.gameObject.GetComponent<EnemyTest>();
                        if (enem.isChasing == false) { NoiseAction2(); }
                        
                    }
                }


            }
        }
    }

    public void NoiseAction2()   //Activa el dispositivo y los enemigos van a él
    {
        Debug.Log("DestroyMina");
        AudioManager.instance.PlaySFX(20);   //QUE SUENE EN LOOP
        if (part.isStopped) { part.Play(); }

        //GameObject.FindGameObjectsWithTag("Enemy");
        enem.LookAt(transform);
        enem.agent.SetDestination(target.transform.position);
        //enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, noise.transform.position, speed * Time.deltaTime);
        Invoke("Return", 10);

    }

    private void Return()  //El objeto luego de unos segundos se destruye
    {
        enem.isPatroling = true;
        Debug.Log("DestroyMina");
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()      //Gizmo del radio del enemigo
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
