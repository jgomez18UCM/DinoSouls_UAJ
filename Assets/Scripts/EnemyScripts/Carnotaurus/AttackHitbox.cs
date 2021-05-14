using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField]
    private GameObject hitbox;

    //Activa el GO hitbox, llamado desde el animation
    public void ActivateHitbox() 
    {
        hitbox.SetActive(true);
    }
}
