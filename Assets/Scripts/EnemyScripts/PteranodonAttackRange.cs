using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PteranodonAttackRange : MonoBehaviour
{
    [SerializeField]
    float attackDuration = 1;
    [SerializeField]
    float attackCooldown = 1;
    [SerializeField]
    Pteranodon pteranodonScript;

    [SerializeField]
    private GameObject attackInstance;

    float cooldown;

    private void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si no está en cooldown ataca
        if (cooldown <= 0)
        {
            Attack();
            cooldown = attackCooldown;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Si no está en cooldown ataca
        if (cooldown <= 0)
        {
            Attack();
            cooldown = attackCooldown;
        }
    }

    void Attack()
    {
        pteranodonScript.Stun(attackDuration);
        attackInstance.SetActive(true);
        Invoke("DeactivateAttack", attackDuration);
    }

    void DeactivateAttack() 
    {
        attackInstance.SetActive(false);
    }
}
