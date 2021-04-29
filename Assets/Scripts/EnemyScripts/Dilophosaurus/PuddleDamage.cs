using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleDamage : MonoBehaviour
{
    //Destruye puddlePoison a los 5 segundos
    private void Start()
    {
        Destroy(this.gameObject, 5);
    }

    //Si detecta al jugador llama a ActivatePoison y destruye charco
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.GetInstance().ActivatePoison();
        Destroy(this.gameObject);
    }
}
