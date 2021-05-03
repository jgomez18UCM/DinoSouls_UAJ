using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    [SerializeField]
    PlayerController playercont;
    [SerializeField]
    Dash dash;
    Rigidbody2D rg;
    [SerializeField]
    int tiempoStun = 1;
    void OnEnable()
    {
        rg = GetComponent<Rigidbody2D>();

        dash.enabled = false;
        playercont.enabled = false;
        
        rg.velocity = new Vector2(0,0);
        print("desactiuve el player ocntroller");
        Invoke("Desactivar", tiempoStun);
    }
    void Desactivar()
    {
        dash.enabled = true;
        playercont.enabled = true;
        this.enabled = false;
    }
}
