using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
   
    PlayerController playercont;
    Rigidbody2D rg;
    void OnEnable()
    {
        playercont =GetComponent<PlayerController>();
        //playercont.enabled =false;
        playercont.enabled = false;
        rg = GetComponent<Rigidbody2D>();
        rg.velocity = new Vector2(0,0);
        print("desactiuve el player ocntroller");
        Invoke("Desactivar", 3);
       


    }

    /*void OnDisable()
    {
        playercont.enabled = true;

    
    }*/
    void Desactivar()
    {
        playercont.enabled = true;
        this.enabled = false;
    
    
    }
}
