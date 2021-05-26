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

    [SerializeField]
    private AudioClip sfxLanza;

    private bool veneno = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyDamage enemigo = collision.gameObject.GetComponent<EnemyDamage>();

        if (enemigo != null)
        {
            enemigo.TakeDamage(damage);

            SoundManager.Instance.Play(sfxLanza);
            
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
