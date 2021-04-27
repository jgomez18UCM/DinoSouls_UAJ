using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoison : MonoBehaviour
{
    [SerializeField]
    GameObject drop;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(drop, this.transform.position, this.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
