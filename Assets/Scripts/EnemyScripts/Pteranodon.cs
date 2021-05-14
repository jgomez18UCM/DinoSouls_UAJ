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

    float mod;
    Rigidbody2D rb;
    Transform posjug;

    bool stunned = false;
    bool knockback = false;
    float knockbackRecoverTime = 0.5f;

    [SerializeField]
    private GameObject direction;
    [SerializeField]
    private GameObject animationDir;
    
    void Start()
    {
        percepcion = GetComponentInChildren<Perception>();
        posjug = jugador.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        speedAuxiliar = pteranodonSpeed;
    }

    void Update()
    {
        if (percepcion.GetSee())//cuando lo vea lo seguira hasta la distancia de  ataque
        {
            mov = posjug.transform.position - transform.position;
            //no encontre niguna instruccion para calcular modulkos pero si alguien la conoce nos ahorramos esto XD
            //mod = Mathf.Sqrt(mov.x * mov.x + mov.y * mov.y);
            mod = mov.magnitude;
            mov.Normalize();

            if (mod < attackDistance - 1.5f)
            {
                //Invoke("Attack", 0);
                mov = -mov;
                pteranodonSpeed = speedAuxiliar;

                //si está muy cerca que salga en direccion contraria y ataca
            }
            else if ((mod<attackDistance) &&mod>= attackDistance - 1)
            {
                pteranodonSpeed = 0;
                //mov = mov * 0; mov me cambia la direccion tbm solo tengo que tocar la velocidad
            }
            else if (mod > attackDistance)//si esta en un rango de distancia con respecto del jugador ,ataca
            {
                pteranodonSpeed = speedAuxiliar;
            }

            direction.transform.up = jugador.transform.position - transform.position;

            //Dirección de la animación
            float angle = Vector2.SignedAngle(direction.transform.up, Vector2.up);

            //Derecha
            if (angle > 0) animationDir.transform.rotation = Quaternion.Euler(0, 180, 0);
            //Izquierda
            else if (angle < 0) animationDir.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if(!percepcion.GetSee())
        {
            CancelInvoke();
            pteranodonSpeed = 0;
        }
    }

    void FixedUpdate()
    {
        if (!stunned)
        {
            if (!percepcion.GetSee())
                rb.velocity = mov * 0;
            else
                rb.velocity = mov * pteranodonSpeed;
        }
    }

    public void Stun(float time)
    {
        stunned = true;
        rb.velocity = Vector2.zero;

        if (time >= 0) //Los valores negativos actúan como comodín para un stun infinito
            Invoke("DeactivateStun", time);
    }

    public void DeactivateStun()
    {
        stunned = false;
        if (knockback)
        {
            Stun(knockbackRecoverTime);
            knockback = false;
        }
    }

    public void Knockback(Vector2 dir, float time)
    {
        knockback = true;
        Stun(time);
        rb.velocity = dir;
    }
}
