using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DilophoMove : MonoBehaviour
{
    [SerializeField]
    GameObject jugador = null;
    [SerializeField]
    private float velocity, velocAux;
    [SerializeField]
    private float standByTime;
    Rigidbody2D rbEnemigo;
    private Vector2 distancia;
    [SerializeField]
    public GameObject drop;
    Perception perception;

    // Start is called before the first frame update
    void Start()
    {
        rbEnemigo = GetComponent<Rigidbody2D>();
        Instantiate(drop, this.transform.position, this.transform.rotation);
        perception = GetComponentInChildren<Perception>();
        velocity = velocAux;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (transform.position != jugador.transform.position)
        {
            distancia = jugador.transform.position - transform.position;
           
        }
        Invoke(nameof(Mueve), standByTime);
    }

    private void Mueve()
    {
        transform.up = distancia.normalized;
        if (!perception.GetSee())
        {
            rbEnemigo.velocity = distancia.normalized * (velocity);            
        }
        else
        {
            rbEnemigo.velocity = distancia.normalized * (-velocity);
        }
    }
}

