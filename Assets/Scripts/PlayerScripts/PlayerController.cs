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


    private Dash dashScript;

    //Índice del ataque seleccionado
    private int activeAttack;

    //Índice del ataque seleccionado anteriormente
    private int prevAttack;

    private GameManager gm;

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

            Instantiate(lanza, this.transform.position, transform.rotation);

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

        bool triceratops = attackRoot[activeAttack] is AttackTriceratops;


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

    }
    void FixedUpdate()
    {
        rb.velocity = movement * speed;

        if (movement.x < 0 && movement.y == 0)  transform.up = Vector2.left;
        
        else if (movement.x == 0 && movement.y < 0)  transform.up = Vector2.down;
        
        else if (movement.x == 0 && movement.y > 0)  transform.up = Vector2.up;
        
        else if (movement.x > 0 && movement.y == 0) transform.up = Vector2.right;
        
        else if (movement.x < 0 && movement.y < 0)  transform.up = Vector2.left + Vector2.down;
        
        else if (movement.x < 0 && movement.y > 0)  transform.up = Vector2.left + Vector2.up;
        
        else if (movement.x > 0 && movement.y < 0)  transform.up = Vector2.right + Vector2.down;
        
        else if (movement.x > 0 && movement.y > 0)  transform.up = Vector2.right + Vector2.up;
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
        GetComponent<Stunt>().enabled=true;

    
    
    }
}
