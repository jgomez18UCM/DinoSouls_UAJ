using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDMG : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Respawn(damage);
        }
    }
}
