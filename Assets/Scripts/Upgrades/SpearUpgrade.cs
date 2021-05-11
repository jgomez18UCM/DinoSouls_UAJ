using UnityEngine;

public class SpearUpgrade : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player)
        {
            player.ActivarVenenoLanza();
        }
        else Debug.LogWarning("Mejora de la lanza colisionando con un objeto no jugador");
    }
}
