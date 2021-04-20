using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliff : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            GameManager.GetInstance().CliffFall();
        }

        //Si se cae un enemigo hace una pequeña animación y lo destruye
        else if (collision.GetComponent<EnemyDamage>() != null) 
        {
            collision.GetComponent<EnemyDamage>().CliffFall();
        }
    }
}
