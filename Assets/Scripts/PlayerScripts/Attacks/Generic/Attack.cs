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

    //Bool que indica si se está atacando para no poder realizar ataques mientras
    private bool attacking = false;

    public void DoAttack()
    {
        if (!attacking)
        {
            //Realiza el ataque tras el tiempo de casteo
            Invoke(nameof(CastAttack), attackCastTime);
            attacking = true;
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
