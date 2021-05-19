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

    [SerializeField]
    private float knockbackTime = 0.5f;
    [SerializeField]
    private float knockbackForce = 3;

    private GameManager gm;
    private PlayerController playerController;
    private EnemyFollow enemyFollow;
    private Pteranodon pteranodon;
    private SpinoEnemy spino;
    private int poisonTicks = -1;
    private int poisonedDmgPerTick;

    private void Start()
    {
        gm = GameManager.GetInstance();
        enemyFollow = GetComponent<EnemyFollow>();
        pteranodon = GetComponent<Pteranodon>();
        spino = GetComponent<SpinoEnemy>();
    }
    private void OnCollisionEnter2D(Collision2D attack)
    {
        playerController = attack.gameObject.GetComponent<PlayerController>();

        //Si lo que colisiona es el jugador llama al método Respawn
        if (playerController != null)
        {
            gm.TakeDamage(damage, false);

            Vector2 dir = attack.transform.position - transform.position;
            dir.Normalize();
            dir *= knockbackForce;

            playerController.Knockback(dir, knockbackTime);
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

    public void Knockback(Vector2 dir, float knockbackTime) 
    {
        if (enemyFollow != null)
        {
            enemyFollow.Knockback(dir, knockbackTime);
        }

        else if (pteranodon != null) pteranodon.Knockback(dir, knockbackTime);

        else if (spino != null) spino.Knockback(dir, knockbackTime);
    }

    public void Poisoned(int time,int dmgPerTick)
    {
        if (poisonTicks <= 0)
        {
            poisonTicks = time;
            poisonedDmgPerTick = dmgPerTick;
        }
        Debug.Log("Daño por veneno");
        TakeDamage(dmgPerTick);
        poisonTicks--;
        if (poisonTicks > 0) Invoke("RepeatDamage",1);
    }

    private void RepeatDamage()
    {
        Poisoned(poisonTicks, poisonedDmgPerTick);
    }
}

