using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Lanza cogida");
            other.GetComponent<PlayerController>().DarLanza();
            Destroy(this.gameObject);
        }
    }
}
