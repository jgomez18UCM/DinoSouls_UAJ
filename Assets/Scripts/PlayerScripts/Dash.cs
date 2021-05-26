using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]
    private float dashSpeed;

    [SerializeField]
    private float dashDistance;

    [SerializeField]
    private float uptime;

    //SFX del dash
    [SerializeField]
    private AudioClip dashSound;

    [SerializeField]
    [Tooltip("GO hijo del player que indica la dirección del dash")]
    private GameObject dashDirection;

    [SerializeField]
    private GameObject[] trailsDash;

    //Variable que dice cuál de los dos trails se usa para el dash, dependiendo de si hay o no power up
    private int dashTrailID = 0;

    //Script del movimiento de la cámara
    private CameraShake cameraShake;
    private RandomCameraShake randomShake;
    private Camera cam;

    //Tiempo del dash
    private float dashTime;

    private Vector3 direction;

    private Rigidbody2D rb;

    PlayerController playerController;

    //Instancia del Game Manager
    private GameManager gameManager;

    private void Awake()
    {
        //Recogemos la cámara antes de nada
        cam = Camera.main;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraShake = cam.GetComponent<CameraShake>();
        playerController = GetComponent<PlayerController>();
        randomShake = cam.GetComponent<RandomCameraShake>();

        gameManager = GameManager.GetInstance();

        //Conversión a tiempo
        dashTime = dashDistance / dashSpeed;
    }

    //Función pública que realiza el dash
    public void ExecuteDash(ref float cooldown) 
    {
        playerController.Stun(dashTime);

        direction = dashDirection.transform.up;

        rb.velocity = direction * dashSpeed;

        cooldown = uptime;

        //Se vuelve invencible al jugador mientras dure el dash
        gameManager.CancelDamage(dashTime);

        //Movimiento de cámara
        cameraShake.Shake(-direction, 0.1f, 0.1f);

        //Se activa el trail
        trailsDash[dashTrailID].SetActive(true);
        trailsDash[dashTrailID].GetComponentInChildren<ParticleSystem>().Play();
        CancelInvoke();
        Invoke("DeactivateTrail", dashTime + 1);

        //Reproducción del sfx
        SoundManager.Instance.Play(dashSound);
    }

    public void Upgrade()
    { 
        dashDistance = dashDistance * 2;
        dashSpeed = dashSpeed * 2;

        dashTrailID = 1;
    }

    void DeactivateTrail() 
    {
        trailsDash[dashTrailID].SetActive(false);
    }
}
