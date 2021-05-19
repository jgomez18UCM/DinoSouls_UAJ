using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinoEnemy : MonoBehaviour
{
    Vector2 mov = new Vector2(0, 0);
    Vector2 dir = new Vector2(0, 0);
    Rigidbody2D rg;
    bool detectado = false;
    bool stun = false;
    bool attacking = false;
      
    [SerializeField]
    Transform jugador = null;
    [SerializeField]
    float tiempoPlacaje = 1;
    [SerializeField]
    float tiempoStun = 1;
    [SerializeField]
    float velocidadPlacaje = 0;

    [SerializeField]
    [Tooltip("GO hijo del enemigo que indica la dirección de los ataques")]
    private GameObject direction;
    [SerializeField]
    private Animator animator;


    Perception perceptionComponent;
    Patrol patrol;
    EnemyFollow enemyFollow;
    [SerializeField]
    Collider2D perceptionCollider;

    bool knockback = false;
    //Tiempo que se queda el enemigo stuneado tras un knockback
    float knockbackRecoverTime = 0.5f;

    void Start()
    {
        rg= GetComponent<Rigidbody2D>();
        perceptionComponent = GetComponentInChildren<Perception>();
        patrol = GetComponent<Patrol>();
        enemyFollow = GetComponent<EnemyFollow>();
        
    }

    void Update()
    {
        if (!stun)
        {
            if (perceptionComponent) detectado = perceptionComponent.GetSee();
            if(detectado && !attacking)
            {
                Placaje();
            }
        }
    }
    void Placaje()
    {
        animator.Play("SpinoPrepareRoll");
        attacking = true;
        dir = jugador.position - transform.position;
        dir.Normalize();
        direction.transform.up = dir;

        if (perceptionCollider) perceptionCollider.enabled = false;
        if (patrol)
        {
            patrol.enabled = false;
            patrol.CancelInvoke();
        }

        mov = dir * velocidadPlacaje;
        rg.AddForce(mov, ForceMode2D.Impulse);
        //Debug.Log("vector de mov (spino): " + mov);
        Invoke(nameof(GetStunned),tiempoPlacaje);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (attacking && collision.gameObject.layer == jugador.gameObject.layer)
        {
            CancelInvoke();
            GetStunned();
        }
    }

    void GetStunned() 
    {
        Stun(tiempoStun);
        animator.Play("SpinoStopped");
        attacking = false;
    }

    public void Stun(float time)
    {
        stun = true;
        rg.velocity = Vector2.zero;

        if (time >= 0) //Los valores negativos actúan como comodín para un stun infinito
            Invoke("DeactivateStun", time);
    }

    public void DeactivateStun()
    {
        stun = false;
        if (perceptionCollider) perceptionCollider.enabled = true;
        if (knockback)
        {
            Stun(knockbackRecoverTime);
            knockback = false;
        }
    }

    public void Knockback(Vector2 dir, float time)
    {
        knockback = true;
        Stun(time);
        rg.velocity = dir;
    }

    public void PlayRollingAnimation() 
    {
        animator.Play("SpinoRoll");
    }

}
