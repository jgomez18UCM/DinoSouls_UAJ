using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piedras : MonoBehaviour
{
    [SerializeField]
    Image piedra=null;

    [SerializeField]
    float time = 3;
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            piedra.enabled = true;
            Invoke("PasaPiedra", time);
            player.ActivaStunt(time);
        }
    }
    void PasaPiedra()
    {
        piedra.enabled = false;
    }
}
