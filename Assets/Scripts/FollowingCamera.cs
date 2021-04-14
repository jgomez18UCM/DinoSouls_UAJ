using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField]
    private Transform tJug;
    Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        // Distancia que hay entre la cámara y el jugador
        distance = (transform.position - tJug.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Cambia la posición de la cámara a la posición del jugador más la distancia
        transform.position = tJug.position + distance;
    }
}
