using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpear : MonoBehaviour
{
    public float lanzaspeed = 2f;
    public float lanzadistance = 6f;
    public GameObject drop;
    float tiempolanzado;
    float tiempotop;
    Rigidbody2D rg;
    // Start is called before the first frame update
    void Start()
    {
        rg = this.GetComponent<Rigidbody2D>();
        tiempolanzado = Time.time;
        tiempotop = lanzadistance / lanzaspeed;//siguiendo la formula v=d/t        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - tiempolanzado < tiempotop)
        {
            if (Time.time - tiempolanzado < lanzadistance / 2)
                rg.velocity = (transform.up * lanzaspeed);
            else
                rg.AddForce(-transform.up * lanzaspeed / 4);


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
