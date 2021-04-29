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
        rbEnemigo.velocity = distancia.normalized * (velocity);
        direction.transform.up = distancia;
        direction.transform.up.Normalize();
    }
}
