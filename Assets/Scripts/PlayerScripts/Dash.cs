using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    //Variables públicas
    public float dashSpeed;
    public float dashDistance;
    public float uptime;

    //SFX del dash
    public AudioSource dashSound;

    //Script del movimiento de la cámara
    private CameraShake cameraShake;
    private Camera cam;

    //Tiempo del dash y contador
    private float timer;
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

        gameManager = GameManager.GetInstance();

        //Conversión a tiempo
        dashTime = dashDistance / dashSpeed;
    }

    void Update()
    {
        //Tiempo de dash
        if (timer <= 0)
        {
            rb.velocity = Vector2.zero;

            playerController.enabled = true;
        }

        else timer -= Time.deltaTime;
    }

    //Función pública que realiza el dash
    public void ExecuteDash(ref float cooldown) 
    {
        playerController.enabled = false;

        direction = transform.up;

        rb.velocity = direction * dashSpeed;

        cooldown = uptime;

        timer = dashTime;

        //Se vuelve invencible al jugador mientras dure el dash
        gameManager.CancelDamage(dashTime);

        //Movimiento de cámara
        cameraShake.Shake(direction);

        //Reproducción del sfx
        dashSound.Play();
    }
}
