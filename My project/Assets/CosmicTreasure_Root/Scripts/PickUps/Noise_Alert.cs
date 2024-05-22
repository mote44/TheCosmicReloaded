using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise_Alert : MonoBehaviour
{
    public Noise noise;
    public EnemyTest enem;
    public Transform target;
    [SerializeField] private float radius;

    private void Start()
    {
        noise = GetComponent<Noise>();
        ComeEnemy();
    }

    public void ComeEnemy()
    {
        if (noise.isDropped)
        {
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
        }
    }

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

    void OnDrawGizmosSelected()      //Gizmo del radio del enemigo
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
