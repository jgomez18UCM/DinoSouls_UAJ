using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Telemetria;

public class PickUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Lanza cogida");
            Tracker.Instance.TrackEvent(new GetItemEvent("Lanza", other.transform.position.x, other.transform.position.y));
            other.GetComponent<PlayerController>().DarLanza();
            Destroy(this.gameObject);
        }
    }
}
