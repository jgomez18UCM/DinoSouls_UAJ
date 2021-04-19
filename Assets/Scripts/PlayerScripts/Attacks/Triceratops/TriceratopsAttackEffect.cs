using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriceratopsAttackEffect : MonoBehaviour
{
    private void OnEnable()
    {
        //Al activarse o crearse activa la invencibilidad
        GameManager.GetInstance().CancelDamage(1);
    }

    private void OnDestroy()
    {
        //Al destruirse desactiva la invencibilidad
        GameManager.GetInstance().DeactivateInvencibility();
    }
}
