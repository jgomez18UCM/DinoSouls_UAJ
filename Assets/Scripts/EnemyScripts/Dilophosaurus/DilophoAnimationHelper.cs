using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DilophoAnimationHelper : MonoBehaviour
{
    [SerializeField]
    private DilophoMove dilophoMove;

    public void SpitPoison() 
    {
        dilophoMove.PoisonInstance();
    }

    public void PlayWalkingAnimation() 
    {
        dilophoMove.PlayWalkingAnim();
    }
}
