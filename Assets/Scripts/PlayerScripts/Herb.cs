using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Telemetria;

public class Herb : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Hierba Verde cogida");
            GameManager.GetInstance().AddHerb();
            Tracker.Instance.TrackEvent(new GetItemEvent("PlantaVerde", other.transform.position.x, other.transform.position.y));
            Destroy(this.gameObject);   
        }
    }
}
