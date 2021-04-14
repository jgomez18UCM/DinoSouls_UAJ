using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriceratops : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab del ataque")]
    private GameObject attackPrefab = null;

    GameObject attackInstance;

    //Bool que indica si se está atacando para no poder realizar ataques mientras
    private bool attacking = false;

    public void DoAttack()
    {
        if (!attacking)
        {
            //Realiza el ataque tras el tiempo de casteo
            attackInstance = Instantiate(attackPrefab, transform);
            attacking = true;
        }
    }

    public void CancelAttack()
    {
        Destroy(attackInstance);

        attacking = false;
    }
}
