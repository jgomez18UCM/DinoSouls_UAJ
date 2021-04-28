using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePoison : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    float speed = 2f, tiempoEnvenenado, cooldown;
    [SerializeField]
    float distance = 6f;
    [SerializeField]
    GameObject drop;
    float tiempolanzado, tiempotop;
    Rigidbody2D rg;

    
    void Start()
    {
        rg = this.GetComponent<Rigidbody2D>();
        tiempolanzado = Time.time;
        tiempotop = distance / speed;//siguiendo la formula v=d/t        
    }
   
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
   
    
    private void OnCollisionEnter2D(Collision2D collision)
    {       
        GameManager.GetInstance().PoisonDamage();
        Destroy(this.gameObject);
    }
   
}
