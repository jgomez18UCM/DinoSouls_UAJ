using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab del ataque")]
    private GameObject attackPrefab = null;  
    [SerializeField]
    [Tooltip("Tiempo de lanzamiento del ataque")]
    private float attackCastTime = 0;
    [SerializeField]
    [Tooltip("Duración del ataque")]
    private float attackDuration = 0;
    [SerializeField]
    [Tooltip("Cooldown del ataque")]
    private float cooldown = 0;

    private float timer = 0;

    //Bool que indica si se está atacando para no poder realizar ataques mientras
    private bool attacking = false;

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
        GameObject attackInstance = Instantiate(attackPrefab, transform);
        //Lo destruye una vez acabada la duración
        Destroy(attackInstance, attackDuration);

        //Pone attacking a false una vez acabe el ataque
        Invoke(nameof(CancelAttacking), attackDuration);
    }

    private void CancelAttacking() 
    {
        attacking = false;
    }
}
