using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piedras : MonoBehaviour
{
    [SerializeField]
    Image piedra=null;
    [SerializeField]
    GameObject jug;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            piedra.enabled = true;
            
            Time.timeScale = 0;
           
           
            player.ActivaStunt(-1);
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            Invoke("PasaPiedra", 0);
    }
    void PasaPiedra()
    {
        Time.timeScale = 1;
       jug.GetComponent<PlayerController>().DeactivateStun();
        piedra.enabled = false;
    }
}
