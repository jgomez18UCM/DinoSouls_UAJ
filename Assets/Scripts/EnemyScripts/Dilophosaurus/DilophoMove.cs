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
    [SerializeField]
    Rigidbody2D rbEnemigo;
    private Vector2 distancia, vEnemigo;
    [SerializeField]
    public GameObject drop;
    [SerializeField]  
    float distanciaMod;
    [SerializeField]
    Perception perception;
    [SerializeField]
    CircleCollider2D rangoAcercar, percepCol;
    



    // Start is called before the first frame update
    void Start()
    {
       // rbEnemigo = GetComponent<Rigidbody2D>();
        Instantiate(drop, this.transform.position, this.transform.rotation);
        //perception = GetComponentInChildren<Perception>();
        velocAux = velocity;
    }
    void FixedUpdate()
    {
       // rbEnemigo.velocity = vEnemigo;
    }

    // Update is called once per frame
    private void Update()
    {
        //distancia = jugador.transform.position - transform.position;
        //Invoke(nameof(Movimiento), standByTime);
    }
   
    private void Movimiento()
    {
        
        distancia = jugador.transform.position - transform.position;
        rbEnemigo.velocity = distancia.normalized *velocity;
        Debug.Log(rbEnemigo.velocity);

        if (percepCol) percepCol.enabled = false;
        if (perception) perception.enabled = false;
        /*if (!perception.GetSee())
        {
            vEnemigo = distancia.normalized * (velocity);
            
        }
        else
        {
            vEnemigo = distancia.normalized * (-velocity);
          
        }*/

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (percepCol) percepCol.enabled = true;
        if (perception) perception.enabled = true;
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Movimiento();
        
        /* percepCol.enabled = false;
        if (perception.GetSee())
        {
            velocAux = 0;
        }
        velocAux = velocity;*/

    }
    /*private void OnTriggerStay2D(Collider2D collider)
    {
        transform.up = distancia.normalized;
        if (perception.GetSee())
        {
            vEnemigo = distancia.normalized * (-velocAux);
        }
        else if (rangoAcercar)
        {
            vEnemigo = distancia.normalized * (velocAux);
        }
       

    }*/
}


