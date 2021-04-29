using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField]
    //contiene los puntos de la patrulla
    Transform [] patrolPositions;

    [SerializeField]
    private GameObject direction;

    [SerializeField]
    //el siguiente punto de patrulla del array es aleatoria si la variable es true
    bool randomNext = false;

    [SerializeField]
    //tiempo que espera el jugador en cada punto antes de moverse al siguiente
    float standBy = 0;

    [SerializeField]
    //velocidad de movimiento del enemigo
    float speed = 3;

    //punto de la patrulla actual
    private int aux = 0;

    private Rigidbody2D rb;

    //vector que contiene las coordenadas del siguiente punto
    private Vector2 nextPosition;

    private float timer = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextPosition = new Vector2(patrolPositions[aux].position.x, patrolPositions[aux].position.y);
    }

   
    private void Update()
    {
        //si la velocidad de patrulla no es 0
        if (speed != 0)
        {
            //si el jugador ha llegado al punto
            if (Vector2.Distance(nextPosition, transform.position) < 0.1)
            {
                //hallamos el siguiente punto de patrulla
                NextPosition();

                timer = standBy;

                //paramos al enemigo
                rb.velocity = Vector2.zero;
            }

            timer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (speed != 0 && timer <= 0)
        {
            //el enemigo patrulla hacia el siguiente punto
            Vector2 dir = nextPosition - new Vector2(transform.position.x, transform.position.y);
            dir.Normalize();
            direction.transform.up = dir;

            rb.velocity = speed * dir;
        }
    }


    //siguiente punto de patrulla
    private void NextPosition()
    {

        if (!randomNext)
        {
            aux = (aux + 1) % patrolPositions.Length;
        }
        else
        {
            aux = Random.Range(0, patrolPositions.Length);
        }

        nextPosition = new Vector2(patrolPositions[aux].position.x, patrolPositions[aux].position.y);
    }
}
