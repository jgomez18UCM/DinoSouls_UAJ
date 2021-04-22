using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAttack : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    public float speed = 2f;
    [SerializeField]
    public float distance = 6f;
    [SerializeField]
    public GameObject drop;
    float tiempolanzado;
    float tiempotop;
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
            Destroy(this.gameObject);
        }

    }
    void OnDestroy()
    {
        Instantiate(drop, this.transform.position, this.transform.rotation);
    }
    
}
