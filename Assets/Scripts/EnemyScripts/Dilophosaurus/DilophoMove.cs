using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DilophoMove : MonoBehaviour
{
    [SerializeField]
    GameObject jugador = null;
    [SerializeField]
    private float velocity;
    [SerializeField]
    private float standByTime;
    Rigidbody2D rbEnemigo;
    private Vector2 distancia;
    bool dentroRango = false;
    [SerializeField]
    public GameObject drop;
    Perception perception;

    // Start is called before the first frame update
    void Start()
    {
        rbEnemigo = GetComponent<Rigidbody2D>();
        Instantiate(drop, this.transform.position, this.transform.rotation);
        perception = GetComponentInChildren<Perception>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (transform.position != jugador.transform.position && !perception.GetSee())
        {
            distancia = jugador.transform.position - transform.position;
           
        }
        else if (transform.position != jugador.transform.position && perception.GetSee())
        {
            distancia = jugador.transform.position + transform.position;
        }
        Invoke(nameof(Mueve), standByTime);
    }

    private void Mueve()
    {
        rbEnemigo.velocity = distancia.normalized * (velocity);
        transform.up = distancia.normalized;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        dentroRango = true;
    }

}

