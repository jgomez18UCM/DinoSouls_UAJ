using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePoison : MonoBehaviour
{
    [SerializeField]
    private int damage, tiempoEnvenenado, cooldown;
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    float distance = 6f;
    [SerializeField]
    GameObject drop;
    float tiempolanzado, tiempotop, venenoIni = 0, cooldownIni = 0;
    Rigidbody2D rg;
    // Start is called before the first frame update
    void Start()
    {
        rg = this.GetComponent<Rigidbody2D>();
        tiempolanzado = Time.time;
        tiempotop = distance / speed;//siguiendo la formula v=d/t        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - tiempolanzado < tiempotop)
        {
            if (Time.time - tiempolanzado < distance / 2)
                rg.velocity = (transform.up * speed);
            else
                rg.AddForce(-transform.up * speed / 4);

        }
        else
        {
            Instantiate(drop, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }

    }
   /* void OnDestroy()
    {
        Instantiate(drop, this.transform.position, this.transform.rotation);
    }*/
    
    //Si detecta al jugador hace daño con TakeDamage y destruye el veneno
    private void OnCollisionEnter2D(Collision2D collision)
    {
        venenoIni = Time.time;
       /* if (tiempoEnvenenado)
        {
            if ()
            {

            }
        }*/
        GameManager.GetInstance().TakeDamage(damage);
        Destroy(this.gameObject);
        
    }
}
