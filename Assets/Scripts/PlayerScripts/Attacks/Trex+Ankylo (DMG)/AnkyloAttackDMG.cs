using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyloAttackDMG : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private Collider2D colliderAnk;

    [SerializeField]
    private float knockbackForce = 10;
    [SerializeField]
    private float knockbackTime = 0.5f;

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

            //Si el ángulo está dentro del área del ataque hace daño
            if (Vector2.Angle(transform.up, dir) <= attackAngle/2 && enemyDamage != null) 
            {
                enemyDamage.TakeDamage(damage);

                //Knockback
                Vector2 dirKnockback = dir.normalized * knockbackForce;
                print(dirKnockback);

                enemyDamage.Knockback(dirKnockback, knockbackTime);
            }
        }
    }

    public void ActivateAttack() 
    {
        colliderAnk.enabled = true;
    }
}
