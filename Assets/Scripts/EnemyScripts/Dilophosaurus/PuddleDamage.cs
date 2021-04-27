using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleDamage : MonoBehaviour
{
    //Llama a PoisonDamage del GM
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.GetInstance().PoisonDamage();
        Destroy(this.gameObject);
    }
}
