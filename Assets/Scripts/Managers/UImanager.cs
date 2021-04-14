using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField]
    private Image[] hearts;

    [SerializeField]
    private Sprite[] heartSprites;

    [SerializeField]
    private Text herbsText;

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
}
