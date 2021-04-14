using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration;

    private float shakeCooldown;
    private Vector3 iniPos;

    void Start()
    {
        iniPos = transform.position;
    }

    void Update()
    {
        if (shakeCooldown <= 0) transform.position = iniPos;

        else shakeCooldown -= Time.deltaTime;
    }

    public void Shake(Vector3 dir) 
    {
        transform.position -= 0.1f * dir;

        shakeCooldown = shakeDuration;
    }
}
