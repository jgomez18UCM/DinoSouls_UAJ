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
    private GameObject stunAnimation;


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

    bool stunned = false;

    private bool canChange = true;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        rotate = this.transform;

        dashScript = GetComponent<Dash>();

        gm = GameManager.GetInstance();
    }


    void Update()
    {
        //Movimiento

        if (!stunned)
        {
            float movX = 0, movY = 0;

            if (Input.GetAxis("Horizontal") >= 0.2) movX = 1;
            else if (Input.GetAxis("Horizontal") <= -0.2) movX = -1;
            if (Input.GetAxis("Vertical") >= 0.2) movY = 1;
            else if (Input.GetAxis("Vertical") <= -0.2) movY = -1;

            movement = new Vector2(movX, movY);
        }

        //Lanza
        if (Input.GetButtonDown("Fire2") && tengolanza)
        {
            tengolanza = false;

            gm.changeSpearStateUI();

            Instantiate(lanza, direction.transform.position, direction.transform.rotation);

        }
        else if (Input.GetButtonDown("Fire3"))
        {
            GameManager.GetInstance().Heal();
        }

        //Dash
        else if (Input.GetButtonDown("Jump") && dashCooldown <= 0 && !stunned)
        {
            dashScript.ExecuteDash(ref dashCooldown);
        }

        if (dashCooldown > 0) dashCooldown -= Time.deltaTime;

        //Ataque

        if (Input.GetButtonDown("Fire1") && !stunned)
        {
            Attack attack = (Attack) attackRoot[activeAttack];

            attack.DoAttack();
        }

        else if (Input.GetButtonDown("EscudoTriceratops") && !stunned) 
        {
            Attack attackTriceratops = (Attack) attackRootTriceratops;

            attackTriceratops.DoAttack();
        }

        if (canChange &&  Input.GetAxis("ChangeAttackF") > 0.1) ChangeAttack(1);
        else if (!canChange && Input.GetAxis("ChangeAttackF") < 0.1) canChange = true;
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
        else if ((movement.x == 0 && movement.y == 0))
        {
            playerAnimator.Play("PlayerStopped");

            //direction.transform.up = Vector2.down;
        }
    }
    void FixedUpdate()
    {
        if(!stunned)
        rb.velocity = movement * speed;
    }

    public void DarLanza()
    {
        tengolanza = true;
        gm.changeSpearStateUI();
    }

    private void ChangeAttack(int dir)
    {
        prevAttack = activeAttack;
        attackRoot[prevAttack].enabled = false;
        canChange = false;
        if (dir == -1 && activeAttack == 0) activeAttack = attackRoot.Length - 1;
        else if (dir == 1 && activeAttack == attackRoot.Length - 1) activeAttack = 0;
        else activeAttack += dir;

        gm.ChangeAttackUI(activeAttack);
    }

    //Activa el stun con animación
    public void ActivaStunt(float time)//publico porque lo llama el pteranodon
    {
        Stun(time);
        movement = Vector2.zero;
        stunAnimation.SetActive(true);
    }

    //Stun sin animación
    public void Stun(float time) 
    {
        stunned = true;
        rb.velocity = Vector2.zero;

        Invoke("DeactivateStun", time);
    }

    private void DeactivateStun() 
    {
        stunned = false;

        //si hay animación activa la desactiva
        if(stunAnimation.activeSelf) stunAnimation.SetActive(false);
    }

    //Knockback para el jugador en dirección contraria a su dir, para colisiones con enemigos
    public void KnockbackNoDir(float knockbackForce, float knockbackTime) 
    {
        Stun(knockbackTime);

        Vector2 dir = -direction.transform.up;
        dir.Normalize();
        dir *= knockbackForce;

        rb.velocity = dir;
    }
    //Para colisiones con ataques
    public void Knockback(Vector2 dir, float knockbackTime) 
    {
        Stun(knockbackTime);

        rb.velocity = dir;
    }

    public void ActivateFallingAnimation() 
    {
        Instantiate(fallingPlayer, transform.position, transform.rotation);
    }
}
