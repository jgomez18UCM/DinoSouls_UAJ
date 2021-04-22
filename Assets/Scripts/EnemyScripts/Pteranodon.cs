using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pteranodon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject jugador;
    [SerializeField] int attackDistance;
    [SerializeField] int pteranodonSpeed;
    int speedAuxiliar;
    Perception percepcion;
    Vector2 mov = new Vector2(0, 0);
    PlayerController playercont;
    Stunt stun;
   float mod;
    Rigidbody2D rg;
    Transform posjug;
    
    void Start()
    {
        percepcion = GetComponent<Perception>();
        posjug = jugador.GetComponent<Transform>();
        //playercont = jugador.GetComponent<PlayerController>();
        stun = jugador.GetComponent<Stunt>();
        stun.enabled = false;
        rg = GetComponent<Rigidbody2D>();
        speedAuxiliar = pteranodonSpeed;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (percepcion.GetSee())//cuando lo vea lo seguira hasta la distancia de  ataque
        {
            mov = posjug.transform.position - transform.position;
            //no encontre niguna instruccion para calcular modulkos pero si alguien la conoce nos ahorramos esto XD
            mod = Mathf.Sqrt(mov.x * mov.x + mov.y * mov.y);
            mov.Normalize();

            if (mod < attackDistance - 2)
            {
                //Invoke("Attack", 0);
                mov = -mov;
                pteranodonSpeed = speedAuxiliar;


                //si está muy cerca que salga en direccion contraria y ataca


            }
            else if ((mod<attackDistance)&&mod>= attackDistance - 2)
            {
                mov = mov;
                pteranodonSpeed = 0;
                //mov = mov * 0; mov me cambia la direccion tbm solo tengo que tocar la velocidad

            }
            else if (mod > attackDistance + 2)//si esta en un rango de distancia con respecto del jugador ,ataca
            {

                mov = mov;
                pteranodonSpeed = speedAuxiliar;

            }
           

            transform.up = mov;
        }
        else if (!percepcion.GetSee())
        {
            CancelInvoke();
            pteranodonSpeed = 0;

            //mov = mov * 0;
        }
      
    }
    void FixedUpdate()
    {
        if (!percepcion.GetSee())
            rg.velocity = mov * 0;
        else
        rg.velocity = mov * pteranodonSpeed;
       
    
    
    }
    void OnTriggerEntered2D(Collider2D other)
    {
        print("el trigger va");
        if (other.GetComponent<PlayerController>() != null)
        {
            //Invoke("Attack", 0);
            //other.GetComponent<Stunt>().enabled = true;
            stun.enabled = true;
        }
    
    
    }
    /*void Attack()
    {

        //estunea al jugador activando el componente stunt que el jugador tiene 
        playercont.ActivaStunt();
        print("active el stunt");
        
    }*/

}
