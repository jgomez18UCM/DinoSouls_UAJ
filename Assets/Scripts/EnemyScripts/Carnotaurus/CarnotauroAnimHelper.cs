using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnotauroAnimHelper : MonoBehaviour
{
    [SerializeField]
    private CarnotaurusCarga carnotaurusCarga;

    public void PlayRollingAnimation()
    {
        carnotaurusCarga.PlayRollingAnimation();
    }
}
