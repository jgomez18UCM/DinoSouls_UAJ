using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinoEnemy : MonoBehaviour
{
    Vector2 mov = new Vector2(0, 0);
    Vector2 dir = new Vector2(0, 0);
    Rigidbody2D rg;
    float tim = 0f;
    bool detectado =true;//se implementara bien cuando tengamos la percepcion lista
    bool stunt = false;
      
    [SerializeField]
    Transform jugador = null;
    [SerializeField]
    int tiempoplacaje = 1;
    [SerializeField]
    int tiempostunt = 1;
    [SerializeField]
   float velocidadplacaje=0f;


    Perception per;
    Patrol p;
    EnemyFollow e;
    Collider2D perCol;

    // Start is called before the first frame update
    void Start()
    {
        rg= GetComponent<Rigidbody2D>();
        Placaje();
        per = GetComponentInChildren<Perception>();
        p = GetComponent<Patrol>();
        e = GetComponent<EnemyFollow>();
        perCol = GetComponentInChildren<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        detectado = per.GetSee();

        if (detectado && !stunt)
        {

            if (Time.time - tim > tiempoplacaje)
            {
                
                dir= dir*0;

                detectado = false;
                
                stunt = true;

                tim = Time.time;
            }


        }
        else if (Time.time - tim > tiempostunt)
        {
            stunt = false;
            if (perCol) perCol.enabled = true;
            if (p) p.enabled = true;
        }
    }
    void Placaje()
    {
        if (detectado)
        {
            dir = jugador.position - transform.position;
            dir.Normalize();
            transform.up = dir;
            tim = Time.time;
        }

    }
    
    void FixedUpdate()
    { 
        if (detectado)
        {
            if (p)
            {
                p.enabled = false;
                p.CancelInvoke();
            }
            if (e)
            {
                e.enabled = false;
                e.CancelInvoke();
            }
            if (perCol) perCol.enabled = false;
            if (p)
            {
                p.enabled = false;
                p.CancelInvoke();
            }
            rg.velocity = Vector2.zero;
            mov = dir * velocidadplacaje;
            rg.AddForce(mov);
            //rg.velocity = mov ;
        }
        else if (stunt)
        {
            //rg.AddForce(-mov);
            rg.velocity = Vector2.zero;

        }
    
    
    }

}
