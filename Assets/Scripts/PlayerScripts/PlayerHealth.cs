using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    Transform respawn;

    public void Respawn(int damage)
    {
        if (GameManager.GetInstance().TakeDamage(damage))
        {
            this.gameObject.transform.position = respawn.position;
        }
    }
}
