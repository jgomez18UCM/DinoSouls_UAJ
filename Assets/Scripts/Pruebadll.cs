using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Telemetria;
using Telemetria.src;

public class Pruebadll : MonoBehaviour
{
    public Prueba_dll pruebaDllClase = new Prueba_dll();

    // Start is called before the first frame update
    void Start()
    {
        pruebaDllClase.Pruebacion(20, 10);

        Debug.Log(pruebaDllClase.a);
    }
}
