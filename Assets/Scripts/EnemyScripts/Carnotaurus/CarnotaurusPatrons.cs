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
    float[] firstPatronUptime = new float[3];
    [SerializeField]
    float[] farPatronUptime = new float[2];
    [SerializeField]
    float[] closePatronUptime = new float[2];
    [SerializeField]
    float bitePatronDistance;
    CarnotaurusCarga charge;
    [SerializeField]
    private menuManager menuM;

    private void OnEnable()
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
                state = States.Sleep;
                FarPatron();
                break;
            case States.Close:
                state = States.Sleep;
                ClosePatron();
                break;
        }
    }

    //Este método activa por primera vez al Carnotauro
    public void WakeUp()
    {
        Debug.Log("Activado");
        state = States.First;
    }

    void FirstPatron()
    {

        Invoke(nameof(Charge),firstPatronUptime[0]);
        Invoke(nameof(Charge), firstPatronUptime[0] + firstPatronUptime[1]);
        Invoke(nameof(SearchPlayer), firstPatronUptime[0] + firstPatronUptime[1] + firstPatronUptime[2]);
       
    }

    void FarPatron()
    {
        Charge();
        Invoke(nameof(Mordisco), farPatronUptime[0]);
        Invoke(nameof(SearchPlayer), farPatronUptime[0] + farPatronUptime[1]);
    }

    void ClosePatron()
    {
        Mordisco();
        Invoke(nameof(Mordisco), closePatronUptime[0]);
        Invoke(nameof(SearchPlayer), farPatronUptime[0] + farPatronUptime[1]);
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
        if (Vector2.Distance(player.transform.position, transform.position) > bitePatronDistance) state = States.Far;
        else state = States.Close;
        Debug.Log("Buscando");
    }

    private void OnDestroy()
    {
        CancelInvoke();
        menuM.EndMenu();
    }

}
