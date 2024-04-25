using UnityEngine;
using Telemetria;

public class AnkyloTotem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("Ankylo Totem cogido");
            Tracker.Instance.TrackEvent(new GetItemEvent("TotemAnkylo", player.transform.position.x, player.transform.position.y));
            player.ActivateAnkylo();
            Destroy(gameObject);
        }
        else Debug.LogWarning("El jugador no tiene player controller");
    }
}
