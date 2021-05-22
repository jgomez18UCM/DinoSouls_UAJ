using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePoison : MonoBehaviour
{  
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    float distance = 6f;
    [SerializeField]
    float tiempoEnvenenado;
    [SerializeField]
    float cooldown;

    [SerializeField]
    GameObject poisonCharco;
    [SerializeField]
    GameObject poison;
    
    Rigidbody2D rb;
    float timerVeneno, timeMax;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();      
        timeMax = distance / speed;
        timerVeneno = 0;
    }
    private void Update()
    {
        timerVeneno += Time.deltaTime;
        if(timerVeneno < timeMax)
        {
            rb.velocity = (transform.up * speed);           
        }
        else
        {
            timerVeneno = 0;
            Instantiate(poisonCharco, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
    
    // Llava a ActivatePoison del GM y destruye el veneno
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            GameManager.GetInstance().ActivatePoison();
        }

        Destroy(this.gameObject);
    }
   
}
