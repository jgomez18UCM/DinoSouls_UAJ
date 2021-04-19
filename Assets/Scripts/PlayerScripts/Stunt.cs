using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunt : MonoBehaviour
{
    [SerializeField] GameObject jugador;
    PlayerController playercont;   
    void Start()
    {
        playercont = jugador.GetComponent<PlayerController>();
        playercont.enabled =false;

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
