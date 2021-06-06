using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliff : MonoBehaviour
{
    EnemyDamage enemyDamage;

    [SerializeField]
    Transform cliffRespawnPoint;

    [SerializeField]
    private float cliffRespawnTime = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            GameManager.GetInstance().CliffFall(cliffRespawnPoint.position, cliffRespawnTime);
        }

        //Si se cae un enemigo hace una pequeña animación y lo destruye
        else
        {
            enemyDamage = collision.GetComponent<EnemyDamage>();

            if (enemyDamage != null) enemyDamage.CliffFall();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            GameManager.GetInstance().CliffFall(cliffRespawnPoint.position, cliffRespawnTime);
        }
    }
}
