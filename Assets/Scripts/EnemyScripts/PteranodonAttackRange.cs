using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PteranodonAttackRange : MonoBehaviour
{
    [SerializeField]
    float attackDuration = 1;
    [SerializeField]
    Pteranodon pteranodonScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pteranodonScript.Attack(attackDuration);
    }
}
