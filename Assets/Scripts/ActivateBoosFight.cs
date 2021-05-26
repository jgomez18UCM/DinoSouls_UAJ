using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoosFight : MonoBehaviour
{
    [SerializeField]
    private GameObject carnotauro;
    [SerializeField]
    private AudioClip rugido;
    [SerializeField]
    private AudioClip musicBoss;

    [SerializeField]
    private GameObject piedrasEntrada;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Rugido
        SoundManager.Instance.Play(rugido);
        Invoke("ActivateCarnotaur", 1);

        piedrasEntrada.SetActive(true);
        gameObject.SetActive(false);
    }

    void ActivateCarnotaur() 
    {
        carnotauro.SetActive(true);
        carnotauro.GetComponent<CarnotaurusPatrons>().WakeUp();
        SoundManager.Instance.PlayMusic(musicBoss);
    }
}
