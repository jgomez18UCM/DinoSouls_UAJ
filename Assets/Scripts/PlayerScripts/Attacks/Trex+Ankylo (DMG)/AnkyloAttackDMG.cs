using System.Collections;
using System.Collections.Generic;
using Telemetria;
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

    [SerializeField]
    private AudioClip sfx;

    //Array de enemigos golpeados
    private GameObject[] enemies;
    int contEnem;

    private EnemyDamage enemyDamage;

    private void OnEnable()
    {
        //Resetea el array
        enemies = new GameObject[5];
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

            //Transform del padre que indica la dirección del ataque
            Transform directionTransform = transform.parent.parent;
            //Vector de la dirección hacia el enemigo
            Vector2 dir = collision.transform.position - directionTransform.position;

            //Si el ángulo está dentro del área del ataque hace daño
            if (Vector2.Angle(transform.up, dir) <= attackAngle/2 && enemyDamage != null) 
            {
                enemyDamage.TakeDamage(damage);
                SoundManager.Instance.Play(sfx);

                //Knockback
                Vector2 dirKnockback = dir.normalized * knockbackForce;
                print(dirKnockback);

                enemyDamage.Knockback(dirKnockback, knockbackTime);
            }
        }
    }

    public void ActivateAttack() 
    {
        Debug.Log("Totem Ankylo usado");
        Tracker.Instance.TrackEvent(new UseItemEvent("TotemAnkylo", transform.position.x, transform.position.y));
        colliderAnk.enabled = true;
    }
}
