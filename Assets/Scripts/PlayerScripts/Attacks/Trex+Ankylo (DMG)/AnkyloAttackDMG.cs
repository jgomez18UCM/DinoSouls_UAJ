using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyloAttackDMG : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    [Tooltip("Ángulo del área de ataque")]
    private float attackAngle = 90;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Vector de la dirección hacia el enemigo
            Vector2 dir = collision.transform.position - transform.position;

            //Si el ángulo está dentro del área del ataque hace daño
            if (Vector2.Angle(transform.up, dir) <= attackAngle/2) 
            {
                collision.gameObject.GetComponent<EnemyDamage>().TakeDamage(damage);
            }
        }
    }
}
