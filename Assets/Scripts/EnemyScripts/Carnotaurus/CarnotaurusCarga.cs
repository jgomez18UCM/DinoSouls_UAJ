using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnotaurusCarga : MonoBehaviour
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

    [SerializeField]
    private GameObject particulasCarga;


    Perception perceptionComponent;
    Patrol patrol;
    EnemyFollow enemyFollow;
    [SerializeField]
    Collider2D perceptionCollider;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        perceptionComponent = GetComponentInChildren<Perception>();
        patrol = GetComponent<Patrol>();
        enemyFollow = GetComponent<EnemyFollow>();

    }

    public void Placaje()
    {
        animator.Play("CarnotaurusPreparing");
        particulasCarga.SetActive(true);
        attacking = true;
        dir = jugador.position - transform.position;
        dir.Normalize();
        direction.transform.up = dir;

        if (patrol)
        {
            patrol.enabled = false;
            patrol.CancelInvoke();
        }
        if (enemyFollow)
        {
            enemyFollow.enabled = false;
            enemyFollow.CancelInvoke();
        }
        if (perceptionCollider) perceptionCollider.enabled = false;
        if (patrol)
        {
            patrol.enabled = false;
            patrol.CancelInvoke();
        }

        mov = dir * velocidadPlacaje;
        rg.AddForce(mov, ForceMode2D.Impulse);
         
        //Debug.Log("vector de mov (spino): " + mov);
        Invoke(nameof(GetStunned), tiempoPlacaje);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (attacking && collision.gameObject.layer == jugador.gameObject.layer)
        {
            CancelInvoke();
            GetStunned();
        }
    }

    private void GetStunned()
    {
        particulasCarga.SetActive(false);
        attacking = false;
        stun = true;
        rg.velocity = Vector2.zero;
        animator.Play("CarnotaurusStop");
        Invoke(nameof(QuitaStun), tiempoStun);
    }

    private void QuitaStun()
    {
        stun = false;
        animator.Play("CarnotaurusWalking");
        if (perceptionCollider) perceptionCollider.enabled = true;
        if (patrol)
        {
            patrol.enabled = true;
            patrol.CancelInvoke();
        }
    }

    public void PlayChargingAnimation()
    {
        animator.Play("CarnotaurusCharging");
    }

}

