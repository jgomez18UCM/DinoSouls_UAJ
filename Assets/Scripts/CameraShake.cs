using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    FollowingCamera followingCamera;

    void Start()
    {
        followingCamera = GetComponent<FollowingCamera>();
    }

    //Ejecuta un camera shake en la dirección y distancia dados por un tiempo
    public void Shake(Vector3 dir, float distance, float time) 
    {
        dir.Normalize();
        dir *= distance;

        //Si hay followingCamera extiende la distancia lo indicado
        if (followingCamera != null) followingCamera.ExtendDistance(dir, time);
    }

    public void DoubleShake(Vector3 dir, float distance, float time) 
    {
        dir.Normalize();
        dir *= distance;

        //Si hay followingCamera extiende la distancia lo indicado
        if (followingCamera != null)
        {
            followingCamera.ExtendDistance(dir, time);
            

        }
    }

    private void SecondShake(Vector3 dir, float distance, float time) 
    {
        if (followingCamera != null) followingCamera.ExtendDistance(dir, time);
    }
}
