using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Telemetria;
using UnityEngine.Analytics;
using System.Reflection;
using System;
public class menuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject controls;

    [SerializeField]
    private GameObject pausa;

    [SerializeField]
    private GameObject credits;

    [SerializeField]
    private string endSceneName = "endScene";


    static bool isGameStarted = false;

    private void Awake()
    {

        if (!isGameStarted)
        {
            isGameStarted = true;

            Debug.Log("Iniciado tracker");
            Debug.Log(AnalyticsSessionInfo.userId);
            Tracker.Init(AnalyticsSessionInfo.userId, Application.persistentDataPath);
            Tracker.Instance.addFilePersister(new SerializerCSV(Assembly.GetAssembly(typeof(menuManager))), "events.csv");

            // do stuff
        }
    }
    private void Start() { }

    public void QuitApplication()
    {
        Tracker.Close();
        Debug.Log("Sesion cerrada");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void changeScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }

    public void GameEvent(string eventID)
    {
        switch(eventID)
        {
            case "Start":
                Tracker.Instance.TrackEvent(new Telemetria.StartGame());
                Debug.Log("Partida Empezada");
                break;
            case "End":
                Tracker.Instance.TrackEvent(new Telemetria.EndGame());
                Debug.Log("Partida Terminada");
                break;
        }
    }

    public void ShowHideControls()
    {
        if (controls.activeInHierarchy == true) controls.SetActive(false);
        else controls.SetActive(true);
    }

    public void ShowHideCredits()
    {
        if (credits.activeInHierarchy == true) credits.SetActive(false);
        else credits.SetActive(true);
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
        Invoke(nameof(ShowEndMenu), 1.5f);
    }

    private void ShowEndMenu()
    {
        SceneManager.LoadScene(endSceneName);
        Tracker.Instance.TrackEvent(new Telemetria.EndGame());
        Debug.Log("Partida Terminada :D");

    }

}
