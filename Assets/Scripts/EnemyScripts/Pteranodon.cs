using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pteranodon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject jugador;
    [SerializeField] int attackDistance;
    [SerializeField] int pteranodonSpeed;
    Perception percepcion;
    Vector2 mov = new Vector2(0, 0);
    PlayerController playercont;

   float mod;
    Rigidbody2D rg;
    Transform posjug;
    
    void Start()
    {
        percepcion = GetComponent<Perception>();
        posjug = jugador.GetComponent<Transform>();
        playercont = jugador.GetComponent<PlayerController>();
        rg = GetComponent<Rigidbody2D>();

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

            if (mod < attackDistance)
            {
                Invoke("Attack", 0);
                mov = -mov;

                
               
               //si está muy cerca que salga en direccion contraria y ataca
               

            }
  else if (mod==attackDistance)
            {
                Invoke("Attack", 0);
                mov = mov * 0;

            }
            else if (mod > attackDistance)//si esta en un rango de distancia con respecto del jugador ,ataca
            {

                mov = mov;

            }
             
        }
        else if (!percepcion.GetSee())
        {
            CancelInvoke();
            mov = mov * 0;
        }
    }
    void FixedUpdate()
    {
        rg.velocity = mov * pteranodonSpeed;
    
    
    }
    void Attack()
    {

        //estunea al jugador activando el componente stunt que el jugador tiene 
        playercont.ActivaStunt();
        print("active el stunt");
        
    }

}
