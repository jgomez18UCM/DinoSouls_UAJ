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
    GameObject poisonCharco;

    [SerializeField]
    GameObject poison;
    float tiempolanzado, tiempotop;
    Rigidbody2D rg;
    float timerVeneno, timeMax;



    
    void Start()
    {
        rg = this.GetComponent<Rigidbody2D>();
        tiempolanzado = Time.time;
        timeMax = distance / speed;//siguiendo la formula v=d/t 
        timerVeneno = 0;
    }
    private void Update()
    {
        timerVeneno += Time.deltaTime;
        if(timerVeneno < timeMax)
        {
            rg.velocity = (transform.up * speed);
           
        }
        else
        {
            timerVeneno = 0;
            Instantiate(poisonCharco, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
    void FixedUpdate()
    {
        /*if (Time.time - tiempolanzado < tiempotop)
        {
            if (Time.time - tiempolanzado < distance / 2)
                rg.velocity = (transform.up * speed);
            else
                rg.AddForce(-transform.up * speed / 4);

        }
        else
        {
            Instantiate(poisonCharco, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }*/

    }
   
    
    private void OnCollisionEnter2D(Collision2D collision)
    {       
        GameManager.GetInstance().ActivatePoison();
        Destroy(this.gameObject);
    }
   
}
