using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DilophoMove : MonoBehaviour
{
    [SerializeField]
    GameObject jugador = null;
    [SerializeField]
    private float velocity;
    float velocAux;
    [SerializeField]
    private float standByTime;
    Rigidbody2D rbEnemigo;
    private Vector2 distancia, vEnemigo;
    [SerializeField]
    public GameObject drop;
    [SerializeField]  
    float distanciaMod;
    Perception perception;
    [SerializeField]
    BoxCollider2D rangoAcercar, percepcion;



    // Start is called before the first frame update
    void Start()
    {
        rbEnemigo = GetComponent<Rigidbody2D>();
        Instantiate(drop, this.transform.position, this.transform.rotation);
        perception = GetComponentInChildren<Perception>();
        velocAux = velocity;
    }
    void FixedUpdate()
    {
        rbEnemigo.velocity = vEnemigo;
    }

    // Update is called once per frame
    private void Update()
    {
        distancia = jugador.transform.position - transform.position;
        Invoke(nameof(Movimiento), standByTime);
    }
   
    private void Movimiento()
    {
       
        if (!perception.GetSee() && !percepcion)
        {
            velocAux = 0;
        }
        else
        {
           transform.up = distancia.normalized;
        }
        /*if (!perception.GetSee())
        {
            vEnemigo = distancia.normalized * (velocity);
            
        }
        else
        {
            vEnemigo = distancia.normalized * (-velocity);
          
        }*/

    }
    private void OnTriggerStay2D(Collider2D collider)
    {
       
        if (perception.GetSee())
        {
            vEnemigo = distancia.normalized * (-velocAux);
        }
        else if (rangoAcercar)
        {
            vEnemigo = distancia.normalized * (velocAux);
        }
        

    }
}


