using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDMG : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private int poisonTicks;
    [SerializeField]
    private int poisonDmgPerTick;

    private bool veneno = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyDamage enemigo = collision.gameObject.GetComponent<EnemyDamage>();
           
            
                enemigo.TakeDamage(damage);
            
            if(veneno)
            {
                Debug.Log("envenenando");
                
                enemigo.Poisoned(poisonTicks, poisonDmgPerTick);
            }
                
        }

       // Debug.Log("hola buenas tardes");
    }

    public void ActivatePoison()
    {
        veneno = true;

    }
}
