using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MoveSpear>() != null)
        {
            Destroy(other.gameObject);  
        }
    }
}
