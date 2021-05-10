using UnityEngine;

public class TRexAttackDMG : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float knockbackTime = 0.5f;
    [SerializeField]
    private float knockbackForce = 3;

    private EnemyDamage enemyDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyDamage = collision.GetComponent<EnemyDamage>();

        if (enemyDamage != null)
        {
            enemyDamage.TakeDamage(damage);

            //Transform del padre que indica la dirección del ataque
            Transform directionTransform = transform.parent.parent;
            //Vector de la dirección hacia el enemigo
            Vector2 dir = collision.transform.position - directionTransform.position;

            //Knockback
            Vector2 dirKnockback = dir.normalized * knockbackForce;

            enemyDamage.Knockback(dirKnockback, knockbackTime);
        }
    }
}
