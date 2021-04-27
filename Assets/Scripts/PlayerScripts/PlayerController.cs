using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float speed = 1f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Transform rotate;
    public GameObject lanza;
    public float dashCooldown;
    [SerializeField]
    private bool tengolanza = true;


    [SerializeField]
    [Tooltip("AttackRoot, hijos del jugador, a meter")]
    private MonoBehaviour[] attackRoot;

    [SerializeField]
    [Tooltip("AttackRoot del triceratops, hijo del jugador, a meter")]
    private MonoBehaviour attackRootTriceratops;

    [SerializeField]
    [Tooltip("GameObject animación de caída")]
    private GameObject fallingPlayer;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    [Tooltip("GO cuyo transform.up es usado por el dash para calcular la dirección")]
    private GameObject direction;


    private Dash dashScript;

    //Índice del ataque seleccionado
    private int activeAttack;

    //Índice del ataque seleccionado anteriormente
    private int prevAttack;

    private GameManager gm;
    private Stun stun;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        rotate = this.transform;

        dashScript = GetComponent<Dash>();

        gm = GameManager.GetInstance();
        stun = GetComponent<Stun>();
    }


    void Update()
    {
        //Movimiento
        
        float movX = 0, movY = 0;
        
        if (Input.GetAxis("Horizontal") >= 0.2) movX = 1;
        else if(Input.GetAxis("Horizontal") <= -0.2)  movX = -1;
        if (Input.GetAxis("Vertical") >= 0.2) movY = 1;
        else if (Input.GetAxis("Vertical") <= -0.2) movY = -1;
      
        movement = new Vector2(movX, movY);

        //Lanza
        if (Input.GetButtonDown("Fire2") && tengolanza)
        {
            tengolanza = false;

            Instantiate(lanza, direction.transform.position, direction.transform.rotation);

        }
        else if (Input.GetButtonDown("Fire3"))
        {
            GameManager.GetInstance().Heal();
        }

        //Dash
        else if (Input.GetButtonDown("Jump") && dashCooldown <= 0)
        {
            dashScript.ExecuteDash(ref dashCooldown);
        }

        if (dashCooldown > 0) dashCooldown -= Time.deltaTime;

        //Ataque

        if (Input.GetButtonDown("Fire1"))
        {
            Attack attack = (Attack) attackRoot[activeAttack];

            attack.DoAttack();
        }

        else if (Input.GetButtonDown("EscudoTriceratops")) 
        {
            Attack attackTriceratops = (Attack) attackRootTriceratops;

            attackTriceratops.DoAttack();
        }

        if (Input.GetButtonDown("ChangeAttackF")) ChangeAttack(1);
        else if (Input.GetButtonDown("ChangeAttackB")) ChangeAttack(-1);

        //MOVIMIENTO

        //izquierda
        if (movement.x < 0 && movement.y == 0)
        {
            playerAnimator.Play("PlayerLateralLeftWalking");

            direction.transform.up = Vector2.left;
        }

        //abajo
        else if (movement.x == 0 && movement.y < 0)
        {
            playerAnimator.Play("PlayerFrontWalking");

            direction.transform.up = Vector2.down;
        }

        //arriba
        else if (movement.x == 0 && movement.y > 0)
        {
            playerAnimator.Play("PlayerBackWalking");

            direction.transform.up = Vector2.up;
        }

        //derecha
        else if (movement.x > 0 && movement.y == 0)
        {
            playerAnimator.Play("PlayerLateralRightWalking");

            direction.transform.up = Vector2.right;
        }

        //diagonal abajo izquierda
        else if (movement.x < 0 && movement.y < 0)
        {
            playerAnimator.Play("PlayerDiagonalLeftWalking");

            direction.transform.up = Vector2.down + Vector2.left;
        }

        //diagonal arriba izquierda
        else if (movement.x < 0 && movement.y > 0)
        {
            playerAnimator.Play("PlayerDiagonalBackLeftWalking");

            direction.transform.up = Vector2.up + Vector2.left;
        }

        //diagonal abajo derecha
        else if (movement.x > 0 && movement.y < 0)
        {
            playerAnimator.Play("PlayerDiagonalRightWalking");

            direction.transform.up = Vector2.down + Vector2.right;
        }

        //diagonal arriba derecha
        else if (movement.x > 0 && movement.y > 0)
        {
            playerAnimator.Play("PlayerDiagonalBackRightWalking");

            direction.transform.up = Vector2.up + Vector2.right;
        }

        //parado
        else if (movement.x == 0 && movement.y == 0)
        {
            playerAnimator.Play("PlayerStopped");

            direction.transform.up = Vector2.down;
        }
    }
    void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    public void DarLanza()
    {
        tengolanza = true;
    }

    private void ChangeAttack(int dir)
    {
        prevAttack = activeAttack;
        attackRoot[prevAttack].enabled = false;
        if (dir == -1 && activeAttack == 0) activeAttack = attackRoot.Length - 1;
        else if (dir == 1 && activeAttack == attackRoot.Length - 1) activeAttack = 0;
        else activeAttack += dir;

        gm.ChangeAttackUI(activeAttack);
    }
    public void ActivaStunt()//publico porque lo llama el pteranodon
    {
        stun.enabled=true;
    }

    public void ActivateFallingAnimation() 
    {
        Instantiate(fallingPlayer, transform.position, transform.rotation);
    }
}
