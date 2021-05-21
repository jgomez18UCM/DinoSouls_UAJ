using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnotaurusPatrons : MonoBehaviour
{
    enum States {Sleep, First, Close, Far};

    States state;
    [SerializeField]
    CarnotaurusBite bite;
    CarnotaurusCarga charge;

    private void Start()
    {
        state = States.Sleep;
        charge = GetComponent<CarnotaurusCarga>();
    }

    private void Update()
    {
        switch (state)
        {
            case States.First:

                break;
            case States.Far:
                break;
            case States.Close:
                break;
            default:
                break;
        }
    }

    //Este método activa por primera vez al Carnotauro
    public void WakeUp()
    {
        state = States.First;
    }

    private void OnDestroy()
    {
        CancelInvoke();
        GameManager.GetInstance();
    }

}
