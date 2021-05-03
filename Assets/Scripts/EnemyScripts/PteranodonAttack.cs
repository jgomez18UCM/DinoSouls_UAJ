using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PteranodonAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Stun stun = other.GetComponent<Stun>();

        print("el trigger va");
        if (stun != null)
        {
            stun.enabled = true;
        }
    }
}
