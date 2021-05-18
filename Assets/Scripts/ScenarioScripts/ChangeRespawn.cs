using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRespawn : MonoBehaviour
{
    private GameManager gm;
    void Start()
    {
        gm = GameManager.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        gm.changeRespawnPoint(transform.position);
    }
}
