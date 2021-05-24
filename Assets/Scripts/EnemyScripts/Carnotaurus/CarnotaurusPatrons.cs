using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnotaurusPatrons : MonoBehaviour
{
    enum States {Sleep, First, Close, Far};

    States state;
    [SerializeField]
    GameObject player;
    [SerializeField]
    CarnotaurusBite bite;
    [SerializeField]
    float[] firstPatronUptime = new float[2];
    [SerializeField]
    float bitePatronDistance;
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
                state = States.Sleep;
                FirstPatron();
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

    void FirstPatron()
    {
        Charge();
        Invoke(nameof(Mordisco), firstPatronUptime[0]);
        Invoke(nameof(SearchPlayer), firstPatronUptime[0] + firstPatronUptime[1]);
       
    }

    void Charge()
    {
        Debug.Log("Carga");
        charge.Placaje();
    }

    void Mordisco()
    {
        Debug.Log("Mordisco");
        bite.DoAttack();
    }

    void SearchPlayer()
    {
        Debug.Log("Buscando");
    }

    private void OnDestroy()
    {
        CancelInvoke();
        GameManager.GetInstance();
    }

}
