using UnityEngine;

public class AnkyloTotem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        { 
            player.ActivateAnkylo();
            Destroy(gameObject);
        }
        else Debug.LogWarning("El jugador no tiene player controller");
    }
}
