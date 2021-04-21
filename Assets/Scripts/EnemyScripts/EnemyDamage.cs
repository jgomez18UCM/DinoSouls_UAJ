using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private int enemyLives;

    [SerializeField]
    private GameObject fallingEnemy;

    private GameManager gm;

    private void Start()
    {
        gm = GameManager.GetInstance();
    }
    private void OnCollisionEnter2D(Collision2D attack)
    {
        //Si lo que colisiona es el jugador llama al método Respawn
        if (attack.gameObject.GetComponent<PlayerController>() != null)
        {
            gm.TakeDamage(damage);
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

    public void CliffFall() 
    {
        Instantiate(fallingEnemy, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }
}

