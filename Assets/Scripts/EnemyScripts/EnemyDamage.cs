using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private GameObject direction;

    [SerializeField]
    private GameObject animationDir;

    [SerializeField]
    private int enemyLives;

    [SerializeField]
    private GameObject fallingEnemy;

    private GameManager gm;

    private void Start()
    {
        gm = GameManager.GetInstance();
    }
    private void OnCollisionEnter2D(Collision2D attack)
    {
        //Si lo que colisiona es el jugador llama al método Respawn
        if (attack.gameObject.GetComponent<PlayerController>() != null)
        {
            gm.TakeDamage(damage, false);
        }

    }

    private void Update()
    {
        float angle = Vector2.SignedAngle(direction.transform.up, Vector2.up);

        //Derecha
        if (angle > 0) animationDir.transform.rotation = Quaternion.Euler(0, 180, 0);
        //Izquierda
        else if (angle < 0) animationDir.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void TakeDamage(int damage)
    {
        enemyLives -= damage;

        if (enemyLives <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void CliffFall() 
    {
        GameObject instance = Instantiate(fallingEnemy, transform.position, transform.rotation);

        Destroy(this.gameObject);

        Destroy(instance, 2);
    }
}

