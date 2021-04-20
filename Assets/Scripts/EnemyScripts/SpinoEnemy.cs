using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinoEnemy : MonoBehaviour
{
    Vector2 mov = new Vector2(0, 0);
    Vector2 dir = new Vector2(0, 0);
    Rigidbody2D rg;
    bool detectado = false;
    bool stun = false;
    bool attacking = false;
      
    [SerializeField]
    Transform jugador = null;
    [SerializeField]
    float tiempoPlacaje = 1;
    [SerializeField]
    float tiempoStun = 1;
    [SerializeField]
    float velocidadPlacaje = 0;


    Perception per;
    Patrol p;
    EnemyFollow e;
    Collider2D perCol;

    // Start is called before the first frame update
    void Start()
    {
        rg= GetComponent<Rigidbody2D>();
        per = GetComponentInChildren<Perception>();
        p = GetComponent<Patrol>();
        e = GetComponent<EnemyFollow>();
        perCol = GetComponentInChildren<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stun)
        {
            if (per) detectado = per.GetSee();
            if(detectado && !attacking)
            {
                Placaje();
            }
        }
    }
    void Placaje()
    {
        attacking = true;
        dir = jugador.position - transform.position;
        dir.Normalize();
        transform.up = dir;

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

        mov = dir * velocidadPlacaje;
        rg.AddForce(mov, ForceMode2D.Impulse);
        Debug.Log("vector de mov (spino): " + mov);
        Invoke(nameof(GetStunned),tiempoPlacaje);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (attacking && collision.gameObject.layer == jugador.gameObject.layer)
        {
            CancelInvoke();
            GetStunned();
        }
    }

    private void GetStunned()
    { 
        attacking = false;
        stun = true; 
        rg.velocity = Vector2.zero;
        Invoke(nameof(QuitaStun), tiempoStun);
    }

    private void QuitaStun()
    {
        stun = false;
    }

}
