using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControlleer : MonoBehaviour
{
    public int currentHealth = 5;
    void Start()
    {
        
    }

   
    void Update()
    {
       
    }

    public void DamagEnemy( int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
