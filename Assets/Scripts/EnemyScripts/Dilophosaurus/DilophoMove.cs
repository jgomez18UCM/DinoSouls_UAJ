using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DilophoMove : MonoBehaviour
{
    [SerializeField]
    GameObject jugador = null;
    [SerializeField]
    GameObject poison;
    [SerializeField]
    GameObject direction;
    [SerializeField]
    GameObject animationDir;

    [SerializeField]
    private float velocity;

    [SerializeField]
    Rigidbody2D rbEnemigo;
    
    [SerializeField]
    Perception perception;

    [SerializeField]
    CircleCollider2D percepCol;

    [SerializeField]
    private Animator animator;

    private Vector2 distancia;
    bool visto;
    float timerVisto;

    // Start is called before the first frame update
    void Start()
    {
        timerVisto = 0;              
    }

    // Update is called once per frame
    private void Update()
    {
        timerVisto += Time.deltaTime;
        if(timerVisto >= 1)
        {
            visto = true;
        }

        float angle = Vector2.SignedAngle(direction.transform.up, Vector2.up);

        //Derecha
        if (angle > 0) animationDir.transform.rotation = Quaternion.Euler(0, 180, 0);
        //Izquierda
        else if (angle < 0) animationDir.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        Attack();
    }

    void Attack() 
    {
        animator.Play("DilophoAttack");
    }

    public void PoisonInstance()
    {
        Instantiate(poison, this.transform.position, direction.transform.rotation);
    }

    public void PlayWalkingAnim() 
    {
        animator.Play("DilophoWalkingLeft");
    }
}



