﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    private int life = 20;

    //Bool que hace que el jugador no reciba daño cuando está en true
    private bool invencible = false;

    [SerializeField]
    private int herbs = 0;

    private int maxLife;

    private UImanager theUIManager;

    //variables que contienen el ataque actual, y cuales ha conseguido el jugador
    private int activeAttack = 0;

    [SerializeField]
    private bool haveTrex = true, haveAnkylo = true, haveTrice=true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }

        else Destroy(this.gameObject);

    }

    private void Start()
    {
        maxLife = life;
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

    public bool TakeDamage(int damage)
    {
        //Bool para respawnear
        bool respawn = false;

        if (!invencible)
        {
            life -= damage;
        }

        if (life <= 0 && !invencible)
        {
            life = maxLife;

            respawn = true;
        }

        theUIManager.UpdateHearts(life);

        return respawn;
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
        if (herbs > 0 && life <= maxLife/5*4)
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
}
