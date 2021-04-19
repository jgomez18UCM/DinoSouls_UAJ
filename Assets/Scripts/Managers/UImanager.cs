using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    //variables para la vida
    [SerializeField]
    private Image[] hearts;

    [SerializeField]
    private Sprite[] heartSprites;

    //variables para la hierbas
    [SerializeField]
    private Text herbsText;

    //variables para la parte de las almas
    [SerializeField]
    private Image[] souls;

    [SerializeField]
    private Sprite[] soulSprites;

    //da una referencia al gamemanager
    private void Start()
    {
        GameManager.GetInstance().SetUIManager(this);
    }

    //hace update de los corazones en pantalla
    public void UpdateHearts(int life)
    {
        int numHeart = life / 4, amountHeart = life % 4;

        for(int i = 0; i < numHeart; i++)
        {
            hearts[i].sprite = heartSprites[4];
        }

        if (numHeart != 5)
        {
            hearts[numHeart].sprite = heartSprites[amountHeart];
        }
    }

    //hace update del numero en pantalla que muestra las hierbas restantes
    public void UpdateHerbs(int herbs)
    {
        herbsText.text = "x" + herbs;
    }

    //se encarga de la parte de la interfaz de las almas
    public void UpdateSouls(bool haveTrex, bool haveAnkylo, int activeAttack, bool haveTrice)
    {
        Sprite trex=soulSprites[1], ankylo=soulSprites[3];

        if (haveTrex) trex = soulSprites[0];
      
        if (haveAnkylo) ankylo = soulSprites[2];

        if (activeAttack == 0)
        {
            souls[0].sprite = trex;
            souls[1].sprite = ankylo;
        }
        else if(activeAttack==1)
        {
            souls[0].sprite = ankylo;
            souls[1].sprite = trex;
        }

        if (haveTrice) souls[2].sprite = soulSprites[4];
        
        else souls[2].sprite = soulSprites[5];

        
    }
}
