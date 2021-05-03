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
    float mod;
    Rigidbody2D rg;
    Transform posjug;

    [SerializeField]
    private GameObject direction;
    [SerializeField]
    private GameObject attackPrefab;
    [SerializeField]
    private GameObject animationDir;
    
    void Start()
    {
        percepcion = GetComponent<Perception>();
        posjug = jugador.GetComponent<Transform>();
        //playercont = jugador.GetComponent<PlayerController>();
        rg = GetComponent<Rigidbody2D>();
        speedAuxiliar = pteranodonSpeed;
        

    }

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

            direction.transform.up = mov;

            //Dirección de la animación
            float angle = Vector2.SignedAngle(direction.transform.up, Vector2.up);

            //Derecha
            if (angle > 0) animationDir.transform.rotation = Quaternion.Euler(0, 180, 0);
            //Izquierda
            else if (angle < 0) animationDir.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (!percepcion.GetSee())
        {
            CancelInvoke();
            pteranodonSpeed = 0;

            //mov = mov * 0;
        }
      
    }

    public void Attack(float attackTime) 
    {
        GameObject instance = Instantiate(attackPrefab, direction.transform);

        Destroy(instance, attackTime);
    }

    void FixedUpdate()
    {
        if (!percepcion.GetSee())
            rg.velocity = mov * 0;
        else
        rg.velocity = mov * pteranodonSpeed;
    }

    
}
