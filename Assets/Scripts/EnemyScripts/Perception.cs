using UnityEngine;

public class Perception : MonoBehaviour
{
    [SerializeField]
    private GameObject father;

    private bool seeing = false;

    Patrol p;
    EnemyFollow s;
    Rigidbody2D rb;

    private void Start()
    {
        p = father.GetComponent<Patrol>();
        s = father.GetComponent<EnemyFollow>();
        rb = father.GetComponent<Rigidbody2D>();
        seeing = false;
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        Debug.Log("Te vi");
        seeing = true;
        See();
   
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        seeing = true;
        //See();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

     seeing = false;
      See();
   
    }

    private void See()
    {
        if (seeing)
        {
            if (p) p.enabled = false;
            if (s) s.enabled = true;
            Debug.Log("Seguimiento activado");
        }
        else
        {
            if (p) p.enabled = true;
            if (s) 
            { 
                s.enabled = false;
                s.CancelInvoke();
            }
            rb.velocity = Vector2.zero;
        }
        
            
        
    }

    public bool GetSee()
    {
        return seeing;
    }
}
