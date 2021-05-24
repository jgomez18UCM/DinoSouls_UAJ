using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoosFight : MonoBehaviour
{
    [SerializeField]
    private GameObject carnotauro;
    [SerializeField]
    private AudioSource rugido;

    [SerializeField]
    private GameObject piedrasEntrada;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Rugido
        //rugido.Play();

        carnotauro.SetActive(true);
        carnotauro.GetComponent<CarnotaurusPatrons>().WakeUp();
        piedrasEntrada.SetActive(true);
    }
}
