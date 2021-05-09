using UnityEngine;

public class RandomCameraShake : MonoBehaviour
{
    [SerializeField]
    private float shakeTime = 0.15f;

    [SerializeField]
    private float minDist = -0.1f;
    [SerializeField]
    private float maxDist = 0.1f;

    FollowingCamera followingCamera;

    private void OnEnable()
    {
        Invoke("DeactivateScript", shakeTime);
    }

    private void Start()
    {
        followingCamera = GetComponent<FollowingCamera>();
    }

    private void Update()
    {
        //Genera un vector aleatorio en el rango elegido
        float x = Random.Range(minDist, maxDist);
        float y = Random.Range(minDist, maxDist);

        followingCamera.ChangeDistance(new Vector3(x, y, 0));
    }

    //Se desactiva tras un tiempo y restaura la distancia inicial
    private void DeactivateScript() 
    {
        followingCamera.RestoreDistance();
        this.enabled = false;
    }
}
