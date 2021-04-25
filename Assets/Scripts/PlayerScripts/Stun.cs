using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
   
    PlayerController playercont;
    Dash dash;
    Rigidbody2D rg;
    [SerializeField]
    int tiempostunt;
    void OnEnable()
    {
        dash = GetComponent<Dash>();
        playercont =GetComponent<PlayerController>();

        //playercont.enabled =false;
        dash.enabled = false;
        playercont.enabled = false;
        rg = GetComponent<Rigidbody2D>();
        rg.velocity = new Vector2(0,0);
        print("desactiuve el player ocntroller");
        Invoke("Desactivar", tiempostunt);
       


    }

    /*void OnDisable()
    {
        playercont.enabled = true;

    
    }*/
    void Desactivar()
    {
        dash.enabled = true;
        playercont.enabled = true;
        this.enabled = false;
    
    
    }
}
