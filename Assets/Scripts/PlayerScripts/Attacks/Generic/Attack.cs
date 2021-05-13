using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Tiempo de lanzamiento del ataque")]
    private float attackCastTime = 0;
    [SerializeField]
    [Tooltip("Duración del ataque")]
    private float attackDuration = 0;
    [SerializeField]
    [Tooltip("Cooldown del ataque")]
    private float cooldown = 0;

    [SerializeField]
    [Tooltip("Prefab del ataque, hijo del root desactivado")]
    GameObject attackInstance;

    private float timer = 0;

    private GameManager gm;

    //Bool que indica si se está atacando para no poder realizar ataques mientras
    private bool attacking = false;

    private void Start()
    {
        gm = GameManager.GetInstance();
    }
    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
    }

    public void DoAttack()
    {
        if (!attacking && timer <= 0)
        {
            //Realiza el ataque tras el tiempo de casteo
            Invoke(nameof(CastAttack), attackCastTime);
            attacking = true;
            timer = cooldown;
        }
    }

    private void CastAttack()
    {
        attackInstance.SetActive(true);

        //si se ha invocado el escudo
        if (attackInstance.GetComponent<TriceratopsAttackEffect>() != null)
        {
            changeShielStateUI();

            //cooldown + duracion del ataque porque en este punto va a iniciar el ataque
            Invoke(nameof(changeShielStateUI), cooldown + attackDuration);
        }

        //Pone attacking a false una vez acabe el ataque y desactiva el ataque
        Invoke(nameof(CancelAttacking), attackDuration);
    }

    private void CancelAttacking() 
    {
        attacking = false;
        attackInstance.SetActive(false);
    }

    //invoca al gm para cambiar el estado de la ui sobre el escudo
    private void changeShielStateUI()
    {
        gm.changeShieldStateUI();
    }
}
