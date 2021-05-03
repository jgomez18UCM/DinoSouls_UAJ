using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinoAnimationHelper : MonoBehaviour
{
    [SerializeField]
    private SpinoEnemy spinoEnemy;

    public void PlayRollingAnimation()
    {
        spinoEnemy.PlayRollingAnimation();
    }
}
