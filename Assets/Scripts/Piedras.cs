using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piedras : MonoBehaviour
{
    [SerializeField]
    Image piedra=null;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            piedra.enabled = true;
            Invoke("PasaPiedra", 3);
           
        
        }
    
    }
    void PasaPiedra()
    {
        piedra.enabled = false;
        Destroy(this.gameObject);

    }
}
