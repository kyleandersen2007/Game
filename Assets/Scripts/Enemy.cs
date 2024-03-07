using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameEvent EnemyKilled;
    public GameObject deathEffect;
    public float enemyHealth;

    public void TakeDamage(float damageAmount)
    {
        enemyHealth -= damageAmount; 

        if(enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            EnemyKilled.Raise();
            Destroy(this.gameObject);
        }
    }
}
