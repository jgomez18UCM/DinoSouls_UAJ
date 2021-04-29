using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleDamage : MonoBehaviour
{
    //Llama a PoisonDamage del GM
    private void Start()
    {
        Destroy(this.gameObject, 5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.GetInstance().ActivatePoison();
        Destroy(this.gameObject);
    }
}
