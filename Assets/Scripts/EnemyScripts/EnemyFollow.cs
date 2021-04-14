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
    Rigidbody2D rbEnemigo;
    private Vector2 distancia;

    // Start is called before the first frame update
    void Start()
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
    // Update is called once per frame
    void FixedUpdate()
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
        transform.up = distancia;
        transform.up.Normalize();
    }
}
