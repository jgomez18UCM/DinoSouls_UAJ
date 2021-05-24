using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnotaurusBite : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject attackInstance;

    [SerializeField]
    private float attackCooldown = 2f;
    [SerializeField]
    private float attackDuration = 1f;

    [SerializeField]
    private GameObject directionGO;

    private bool attack;
    private float distance;
    private LayerMask mask;
    private Vector2 direction;
    private Color color = Color.red;

    [SerializeField]
    private GameObject father = null;

    private Patrol enemyPatrol;
    private EnemyFollow enemyFollow;


    void Start()
    {
        enemyPatrol = father.GetComponent<Patrol>();
        enemyFollow = father.GetComponent<EnemyFollow>();
        attack = true;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 dir = -transform.position + player.transform.position;
        if (Vector2.Angle(directionGO.transform.up, dir) <= 45)
            DoAttack();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector2 dir = -transform.position + player.transform.position;
        if (Vector2.Angle(directionGO.transform.up, dir) <= 45)
            DoAttack();
    }*/

    public void DoAttack()
    {
        if (attack)
        {          
            enemyFollow.Stun(-1); //Le pasamos el valor -1 para tener un stun con tiempo indeterminado
            attackInstance.SetActive(true);

            attack = false;
            Invoke(nameof(ResetCooldown), attackCooldown);
            Invoke(nameof(MoveAgain), attackDuration);
        }
    }

    private void ResetCooldown()
    {
        attack = true;
    }
    private void MoveAgain()
    {      
        enemyFollow.DeactivateStun();
        attackInstance.SetActive(false);
    }

}

