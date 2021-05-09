using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAnimation : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Velocidad de giro al caer")]
    private float spinSpeed = 1;

    [SerializeField]
    [Tooltip("Velocidad de reducción del tamaño")]
    private float sizeReduceSpeed = 1;

    [SerializeField]
    [Tooltip("Tiempo que dura la animación de la caída")]
    private float fallTime;

    float zRotation;
    float scale = 1;

    private void Start()
    {
        zRotation = transform.rotation.eulerAngles.z;

        Destroy(this.gameObject, fallTime);
    }

    void Update()
    {
        zRotation -= spinSpeed * Time.deltaTime;

        //Rota a la velocidad elegida
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));

        scale -= sizeReduceSpeed * Time.deltaTime;

        //Reduce su tamaño progresivamente
        transform.localScale = new Vector2(scale, scale);
    }
}
