using System.Collections;
using System.Collections.Generic;
using Telemetria;
using UnityEngine;

public class TriceTotem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("Trice Totem cogido");
            Tracker.Instance.TrackEvent(new GetItemEvent("TotemTrice", player.transform.position.x, player.transform.position.y));
            player.ActivateTrice();
            Destroy(gameObject);
        }
        else Debug.LogWarning("Jugador sin PlayerController");
    }
}
