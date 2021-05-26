using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject controls;

    [SerializeField]
    private GameObject pausa;

    [SerializeField]
    private string endSceneName = "endScene";
    public void QuitApplication()
    {
        Application.Quit();
    }

    public void changeScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }

    public void ShowHideControls()
    {
        if (controls.activeInHierarchy == true) controls.SetActive(false);
        else controls.SetActive(true);
    }

    public void MenuPausa()
    {
        if (pausa.activeInHierarchy == true)
        {
            pausa.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pausa.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void EndMenu()
    {
        Invoke(nameof(ShowEndMenu),1.5f);
    }

    private void ShowEndMenu()
    {
        SceneManager.LoadScene(endSceneName);
    }
}
