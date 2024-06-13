using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;
    public Rigidbody TheRb;
    public GameObject impactEffect;
    public int damege = 1;
    public bool damageEnemy, damagePlayer;

    void Start()
    {
        
    }

   
    void Update()
    {
        TheRb.velocity = transform.forward * moveSpeed;
        
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0 )
        {
            Destroy(gameObject);
        } 
    }
    

    //ban dich
    private void OnTriggerEnter(Collider other)
    {
        //thẻ Enemy và DamageEnemy đều đúng chúng ta được phép xát thương

        if (other.gameObject.tag == "Enemy" && damageEnemy )
        {
            //Destroy (other.gameObject);
            other.gameObject.GetComponent<EnemyHealthControlleer>().DamagEnemy(damege);
        }    

        // đam kẻ thù cho người chơi
        if (other.gameObject.tag == "Player" && damagePlayer )
        {

        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);
    }

}
