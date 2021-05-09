using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriceTotem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ActivateTrice();
            Destroy(gameObject);
        }
        else Debug.LogWarning("Jugador sin PlayerController");
    }
}
