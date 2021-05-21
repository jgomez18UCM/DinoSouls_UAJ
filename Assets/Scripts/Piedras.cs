using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piedras : MonoBehaviour
{
    [SerializeField]
    GameObject piedra;

    private PlayerController player;


    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            piedra.SetActive(true);
            
            Time.timeScale = 0;
           
            player.ActivaStunt(-1);
        }
    }
    private void Update()
    {
        if (piedra.activeInHierarchy && Input.GetButtonDown("Jump"))
            Invoke("PasaPiedra", 0);
    }
    void PasaPiedra()
    {
        Time.timeScale = 1;
        player.GetComponent<PlayerController>().DeactivateStun();
        piedra.SetActive(false);
    }
}
