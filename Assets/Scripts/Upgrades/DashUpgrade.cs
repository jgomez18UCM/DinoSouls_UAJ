using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUpgrade : MonoBehaviour
{
    private Dash dash;
    void  OnTriggerEnter2D(Collider2D other)
    { 
        if (other.GetComponent<Dash>() != null)
        {
            dash = other.GetComponent<Dash>();
            dash.Upgrade();
            Destroy(this.gameObject);


        }       
            
        
    
    }
}
