using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int health;


    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        gameObject.GetComponent<Animator>().SetTrigger("hurt");

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        if(gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<MeleeEnemyController>().Died();
        }
        
        gameObject.GetComponent<Animator>().SetBool("isDead", true);
        Destroy(transform.parent.gameObject);
    }
}
