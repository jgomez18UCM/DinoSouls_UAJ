using UnityEngine;

public class LancerMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject attackPrefab;

    [SerializeField]
    private Transform attackRoot;
    private GameObject attackInstance;

    [SerializeField]
    private float attackCooldown = 2f;
    [SerializeField]
    private float attackDuration = 1f;

    private bool attack;
    private float distance;
    private LayerMask mask;
    private Vector2 direction;
    private Color color = Color.red;

    [SerializeField]
    private GameObject father = null;

    [SerializeField]
    private Perception per;

    [SerializeField]
    private Collider2D perColl;

    private Patrol p;
    private EnemyFollow f;
    private Rigidbody2D rb;
    

    void Start()
    {
        p = father.GetComponent<Patrol>();
        f = father.GetComponent<EnemyFollow>();
        rb = father.GetComponent<Rigidbody2D>();
        attack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 dir = -transform.position + player.transform.position;
        if (Vector2.Angle(transform.up, dir) <= 45)
            DoAttack();
        Debug.Log("ángulo: " + Vector2.Angle(transform.up, dir));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector2 dir = -transform.position + player.transform.position;
        if (Vector2.Angle(transform.up, dir) <= 45)
            DoAttack();
        Debug.Log("ángulo: " + Vector2.Angle(transform.up, dir));
    }

    public void DoAttack()
    {
        if (attack)
        {
            if (per) per.enabled = false;
            if (perColl) perColl.enabled = false;         
            if (p) p.enabled = false;
            if (f)
            {
                f.CancelInvoke();
                f.enabled = false;
            }  
            rb.velocity = new Vector2(0,0);
            attackInstance = Instantiate(attackPrefab, attackRoot);
            attack = false;
            Invoke(nameof(ResetCooldown), attackCooldown);
            Destroy(attackInstance, attackDuration);
            Invoke(nameof(MoveAgain), attackDuration);
        }        
    }

    private void ResetCooldown()
    {
        attack = true;
    }
    private void MoveAgain()
    {
        perColl.enabled = true;
        if(per) per.enabled = true;  
    }

}
