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
    GameObject drop;
    [SerializeField]
    GameObject poison;
    [SerializeField]
    Perception perception;
    [SerializeField]
    CircleCollider2D rangoAcercar, percepCol;
    bool visto;
    float timerVisto;
    [SerializeField]
    GameObject dilophosaurio;
    [SerializeField]
    GameObject direction;

    // Start is called before the first frame update
    void Start()
    {
        timerVisto = 0;
        
        // rbEnemigo = GetComponent<Rigidbody2D>();
       
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
        timerVisto += Time.deltaTime;
        if(timerVisto >= 1)
        {
            visto = true;
        }
     
    }
   
    private void Movimiento()
    {
        // Desactiva percepción (Trigger y Script)
        if (percepCol) percepCol.enabled = false;
        if (perception) perception.enabled = false;

        // Calcula la distancia y lo mueve
        distancia = transform.position - jugador.transform.position;
        distancia.Normalize();
        direction.transform.up = -distancia;        
        rbEnemigo.velocity = distancia *velocity;
            
    }

    //Activa la percepcíon con 1 seg de retraso al salir de rangoAlejar
    private void OnTriggerExit2D(Collider2D collision)
    {
        Invoke("ActivatePerception", 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        visto = true;
        CancelInvoke();
        Movimiento();
      
    }
     private void OnTriggerStay2D(Collider2D collider)
     {
        if (visto)
        {
            visto = false;
            Movimiento();
            timerVisto = 0;
        }

     }
    private void ActivatePerception()
    {
        if (percepCol) percepCol.enabled = true;
        if (perception) perception.enabled = true;
        PoisonInstance();
    }
    
    void PoisonInstance()
    {
        Instantiate(poison, this.transform.position, direction.transform.rotation);
    }
    
}



