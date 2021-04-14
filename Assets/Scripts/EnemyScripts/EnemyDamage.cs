using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private int enemyLives;

    private void OnCollisionEnter2D(Collision2D attack)
    {
        //Si lo que colisiona es el jugador llama al método Respawn
        if (attack.gameObject.GetComponent<PlayerController>() != null)
        {
            attack.gameObject.GetComponent<PlayerHealth>().Respawn(damage);
        }

    }

    public void TakeDamage(int damage)
    {
        enemyLives -= damage;

        if (enemyLives <= 0)
        {
            Destroy(gameObject);
        }
    }
}

