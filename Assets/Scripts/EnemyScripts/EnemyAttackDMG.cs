using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDMG : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float knockbackTime = 0.5f;
    [SerializeField]
    private float knockbackForce = 3;

    private PlayerController playerController;
    private GameManager gm;

    private void Start()
    {
        gm = GameManager.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController = collision.GetComponent<PlayerController>();

        if (playerController != null)
        {
            gm.TakeDamage(damage, false);

            Vector2 dir = transform.up;
            dir.Normalize();
            dir *= knockbackForce;

            playerController.Knockback(dir, knockbackTime);
        }
    }
}
