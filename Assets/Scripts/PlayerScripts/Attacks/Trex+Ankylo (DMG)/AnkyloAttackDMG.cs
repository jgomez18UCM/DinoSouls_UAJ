using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyloAttackDMG : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float knockBackForce;

    [SerializeField]
    [Tooltip("Ángulo del área de ataque")]
    private float attackAngle = 90;

    private EnemyDamage enemyDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyDamage = collision.GetComponent<EnemyDamage>();

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Vector de la dirección hacia el enemigo
            Vector2 dir = collision.transform.position - transform.position;

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (rb != null) rb.AddForce(dir.normalized * knockBackForce, ForceMode2D.Impulse);

            //Si el ángulo está dentro del área del ataque hace daño
            if (Vector2.Angle(transform.up, dir) <= attackAngle/2 && enemyDamage != null) 
            {
                enemyDamage.TakeDamage(damage);
            }
        }
    }
}
