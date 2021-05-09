using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PteranodonAttack : MonoBehaviour
{
    [SerializeField]
    private float stunTime = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        print("el trigger va");
        if (playerController != null)
        {
            playerController.ActivaStunt(stunTime);
        }
    }
}
