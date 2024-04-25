using System.Collections;
using System.Collections.Generic;
using Telemetria;
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
    bool poisoned = false;
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
    private bool haveTrex = true, haveAnkylo = false, haveTrice = false;

    //punto de respawn del jugador
    [SerializeField]
    private Transform respawn;

    private Vector2 respawnVector;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float respawnTime = 2;

    [SerializeField]
    private GameObject playerFallingPrefab;
    [SerializeField]
    private GameObject deadPlayerPrefab;

    [SerializeField]
    private AudioClip[] audioClips;

    Vector2 respawnPointTemp;

    private RandomCameraShake randomShake;

    private bool haveLanza = true;

    //variable que indica si se le puede envenenar al jugador por parte del escudo(es decir,si no lo tiene)
    private bool canPoison = true;

    private bool poisonedSpear = false;

    [SerializeField]
    private GameObject poisonEffect;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }

        else Destroy(this.gameObject);

        life = MAXLIFE;
    }

    private void Start()
    {
        randomShake = Camera.main.GetComponent<RandomCameraShake>();
        respawnVector = new Vector2(respawn.position.x, respawn.position.y);
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

    public void TakeDamage(int damage, bool cliff)
    {
        if (!cliff) randomShake.enabled = true;

        if (!invencible)
        {
            life -= damage;
            SoundManager.Instance.Play(audioClips[1]);
        }

        if (life <= 0 && !invencible)
        {
            life = MAXLIFE;

            //Respawnea al jugador
            Respawn(respawnVector, respawnTime);

            if (!cliff)
            { 
                player.gameObject.SetActive(false);

                GameObject instance = Instantiate(deadPlayerPrefab, player.transform.position, player.transform.rotation);
                SoundManager.Instance.Play(audioClips[0]);

                Destroy(instance, respawnTime);
            }

        }
        theUIManager.UpdateHearts(life);
    }

    //Método que hace invencible al jugador un tiempo determinado
    public void CancelDamage(float time) 
    {
        invencible = true;
        canPoison = false;

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
        canPoison = true;
    }

    public void Heal()
    {
        if (herbs > 0 && life <= MAXLIFE/5*4)
        {
            herbs--;

            life+=4;

            theUIManager.UpdateHearts(life);

            theUIManager.UpdateHerbs(herbs);

            Debug.Log("Planta verde usada");
            Tracker.Instance.TrackEvent(new UseItemEvent("PlantaVerde", player.transform.position.x, player.transform.position.y));

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
            TakeDamage(fallDamage, true);
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
    void PoisonDamage()
    {
        if (poisonTick < contpoisonMax)
        {           
            TakeDamage(poisonDamage, false);
            Invoke(nameof(PoisonDamage), 1);
            poisonTick++;
        }
        else
        {
            poisonTick = 0;
            poisoned = false;
            poisonEffect.SetActive(false);
        }
    }
     
    public void ActivatePoison()
    {
        if (!poisoned && canPoison)
        {
            PoisonDamage();
            poisoned = true;
            poisonEffect.SetActive(true);
        }
    }
    //cambia el estado de la lanza en la interfaz
    public void changeSpearStateUI() 
    {
        haveLanza = !haveLanza;

        theUIManager.UpdateSpear(haveLanza,poisonedSpear);
    }

    public void ActivateAnkylo()
    {
        haveAnkylo = true;
        theUIManager.UpdateSouls(haveTrex, haveAnkylo, activeAttack, haveTrice);
    }

    public void ActivateTrice()
    {
        haveTrice = true;
        theUIManager.UpdateSouls(haveTrex, haveAnkylo, activeAttack, haveTrice);
    }

    public void changeRespawnPoint(Vector2 respawnPosition)
    {
        respawnVector = respawnPosition;
    }

    public void ActivatePoisonSpear()
    {
        poisonedSpear = true;
        theUIManager.UpdateSpear(haveLanza, poisonedSpear);
    }
}
