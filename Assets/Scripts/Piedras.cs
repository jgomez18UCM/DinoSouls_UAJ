using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piedras : MonoBehaviour
{
    [SerializeField]
    Image piedra=null;
    PlayerController player;


    void OnTriggerEnter2D(Collider2D other)
    {
       player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            piedra.enabled = true;
            player.enabled = false;
            Time.timeScale = 0;
           
           
            //player.ActivaStunt(time);
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Invoke("PasaPiedra", 0);
    }
    void PasaPiedra()
    {
        Time.timeScale = 1;
        player.enabled = true;
        piedra.enabled = false;
    }
}
