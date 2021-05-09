using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField]
    private Transform tJug;
    
    Vector3 distance;

    Vector3 iniDistance;

    bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        // Distancia que hay entre la cámara y el jugador
        distance = (transform.position - tJug.position);
        iniDistance = distance;
    }

    void LateUpdate()
    {
        // Cambia la posición de la cámara a la posición del jugador más la distancia
        if (active) transform.position = tJug.position + distance;
    }

    //Aumenta la distancia entre la cámara y el jugador un tiempo
    public void ExtendDistance(Vector3 addDistance, float time) 
    {
        distance += addDistance;

        Invoke("RestoreDistance", time);
    }

    //Restaura la distancia inicial
    public void RestoreDistance() 
    {
        distance = iniDistance;
        CancelInvoke();
    }

    //Cambia la distancia permanentemente
    public void ChangeDistance(Vector3 addDistance) 
    {
        distance += addDistance;
    }

    /*
    //Desactiva el movimiento de la cámara un tiempo dado
    public void DeactivateMovement(float activationTime) 
    {
        active = false;

        Invoke("ActivateMovement", activationTime);
    }

    private void ActivateMovement() 
    {
        active = true;
        CancelInvoke();
    }
    */
}
