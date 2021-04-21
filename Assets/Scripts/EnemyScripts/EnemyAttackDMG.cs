using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDMG : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private GameManager gm;

    private void Start()
    {
        gm = GameManager.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            gm.TakeDamage(damage);
        }
    }
}
