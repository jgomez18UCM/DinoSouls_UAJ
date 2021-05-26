using UnityEngine;

public class TRexAttackDMG : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float knockbackTime = 0.5f;
    [SerializeField]
    private float knockbackForce = 3;

    [SerializeField]
    private AudioClip sfx;

    //Array de enemigos golpeados
    private GameObject[] enemies;
    int contEnem;

    private EnemyDamage enemyDamage;

    private void OnEnable()
    {
        //Resetea el array
        enemies = new GameObject[10];
        contEnem = 0;
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyDamage = collision.GetComponent<EnemyDamage>();

        //Comprueba que el enemigo no está en el array de los que ya han sido atacados
        int i = 0;
        while (i < contEnem && collision.gameObject != enemies[i]) i++;

        if (enemyDamage != null && i == contEnem)
        {
            //Añade el enemigo al array de enemigos golpeados
            enemies[contEnem] = collision.gameObject;
            contEnem++;

            enemyDamage.TakeDamage(damage);
            SoundManager.Instance.Play(sfx);

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
