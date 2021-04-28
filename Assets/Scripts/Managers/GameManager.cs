using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    
    const int MAXLIFE = 20;

    [SerializeField]
    int contpoisonMax = 4;
    [SerializeField]
    int poisonDamage= 1;
    int poisonTick = 0;
    [SerializeField]
    [Tooltip("Daño al caer por un precipicio")]
    int fallDamage = 4;

    //Bool que hace que el jugador no reciba daño cuando está en true
    private bool invencible = false;

    [SerializeField]
    private int herbs = 0;

    private int life;

    private UImanager theUIManager;

    //variables que contienen el ataque actual, y cuales ha conseguido el jugador
    private int activeAttack = 0;

    [SerializeField]
    private bool haveTrex = true, haveAnkylo = true, haveTrice=true;

    //punto de respawn del jugador
    [SerializeField]
    private Transform respawn;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float respawnTime = 2;

    [SerializeField]
    private GameObject playerFallingPrefab;

    Vector2 respawnPointTemp;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }

        else Destroy(this.gameObject);

        life = MAXLIFE;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void AddHerb()
    {
        herbs++;

        theUIManager.UpdateHerbs(herbs);
    }

    public void TakeDamage(int damage)
    {

        if (!invencible)
        {
            life -= damage;
        }

        if (life <= 0 && !invencible)
        {
            life = MAXLIFE;

            //Respawnea al jugador
            Respawn(respawn.position, respawnTime);
        }
        theUIManager.UpdateHearts(life);
    }

    //Método que hace invencible al jugador un tiempo determinado
    public void CancelDamage(float time) 
    {
        invencible = true;

        //Solo llama la función de desactivar si el tiempo es >= 0
        if (time >= 0) 
        {
            Invoke("DeactivateInvencibility", time);
        }

        //Usando el valor tiempo = -1 conseguimos una invencibilidad que durará hasta que decidamos desactivarla manualmente
    }

    //Función a llamar cuando queramos desactivar la invencibilidad
    public void DeactivateInvencibility() 
    {
        invencible = false;
    }

    public void Heal()
    {
        if (herbs > 0 && life <= MAXLIFE/5*4)
        {
            herbs--;

            life+=4;

            theUIManager.UpdateHearts(life);

            theUIManager.UpdateHerbs(herbs);

        }
    }

    //referencia del canvas (uimanager)
    public void SetUIManager(UImanager uim)
    {
        theUIManager = uim;

        theUIManager.UpdateHearts(life);

        theUIManager.UpdateHerbs(herbs);

        theUIManager.UpdateSouls(haveTrex, haveAnkylo, activeAttack, haveTrice);
    }

    //cambia los ataques en la UI
    public void ChangeAttackUI(int attack)
    {
        activeAttack = attack;

        theUIManager.UpdateSouls(haveTrex, haveAnkylo, activeAttack, haveTrice);
    }

    //cambia el estado del escudo en la UI
    public void changeShieldStateUI()
    {
        haveTrice = !haveTrice;

        theUIManager.UpdateSouls(haveTrex, haveAnkylo, activeAttack, haveTrice);
    }

    //Hace daño al jugador cuando se cae por un precipicio (activa animación en el futuro)
    public void CliffFall(Vector2 respawnPoint, float respawnTime)
    {
        if (!invencible) 
        {
            TakeDamage(fallDamage);
            player.gameObject.SetActive(false);

            GameObject instance = Instantiate(playerFallingPrefab, player.transform.position, player.transform.rotation);

            Destroy(instance, respawnTime);

            //Si no muere respawnea al lado
            if (life < MAXLIFE)
            {
                Respawn(respawnPoint, respawnTime);
            }
        }
    }

    //Método que respawnea al jugador en el punto dado por el vector
    void Respawn(Vector2 respawnPoint, float respawnTime) 
    {
        CancelInvoke();
        respawnPointTemp = respawnPoint;
        Invoke("RespawnPlayer", respawnTime);
    }

    void RespawnPlayer() 
    {
        player.transform.position = respawnPointTemp;

        if (!player.gameObject.activeSelf) player.gameObject.SetActive(true);
    }
    public void PoisonDamage()
    {
        if (poisonTick < contpoisonMax)
        {
            //Debug.Log("DAño");
            TakeDamage(poisonDamage);
            Invoke(nameof(PoisonDamage), 1);
            poisonTick++;
        }
        else
        {
            poisonTick = 0;
        }
    }
}
