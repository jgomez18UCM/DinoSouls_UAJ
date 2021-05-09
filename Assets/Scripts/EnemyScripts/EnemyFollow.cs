using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField]
    GameObject jugador = null;
    [SerializeField]
    private float velocity;
    [SerializeField]
    private float standByTime;
    [SerializeField]
    private Perception perception;

    [SerializeField]
    private GameObject direction;

    Rigidbody2D rbEnemigo;
    private Vector2 distancia;

    bool stunned = false;
    bool knockback = false;
    //Tiempo que se queda el enemigo stuneado tras un knockback
    float knockbackRecoverTime = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        rbEnemigo = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (transform.position != jugador.transform.position)
        {
            distancia = jugador.transform.position - transform.position;
        }
        Mueve();
    }

    void Update()
    {
        if (perception.GetSee())
        {
            if (transform.position != jugador.transform.position)
            {
                distancia = jugador.transform.position - transform.position;
            }
            Invoke(nameof(Mueve), standByTime);
        }
    }

    private void Mueve()
    {
        if (!stunned)
        {
            rbEnemigo.velocity = distancia.normalized * (velocity);
            direction.transform.up = distancia;
            direction.transform.up.Normalize();
        }
    }

    public void Stun(float time)
    {
        stunned = true;
        rbEnemigo.velocity = Vector2.zero;

        if (time >= 0) //Los valores negativos actúan como comodín para un stun infinito
        Invoke("DeactivateStun", time);
    }

    public void DeactivateStun()
    {
        stunned = false;
        if (knockback) 
        {
            Stun(knockbackRecoverTime);
            knockback = false;
        }
    }

    public void Knockback(Vector2 dir, float time) 
    {
        knockback = true;
        Stun(time);
        rbEnemigo.velocity = dir;
    }
}
