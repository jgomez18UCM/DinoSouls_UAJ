using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunt : MonoBehaviour
{
   
    PlayerController playercont;
    Rigidbody2D rg;
    void OnEnable()
    {
        playercont =GetComponent<PlayerController>();
        playercont.enabled =false;
        rg.velocity = new Vector2(0,0);
        print("desactiuve el player ocntroller");
       


    }

    void OnDisable()
    {
        playercont.enabled = true;

    
    }
    void Desactivar()
    {
        this.enabled = false;
    
    
    }
}
