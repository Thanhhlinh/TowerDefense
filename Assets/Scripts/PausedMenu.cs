using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedMenu : MonoBehaviour
{
    public GameObject ui;

    public SceneFade sceneFade;

    public string menuSceneName = "MainMenu";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Toggle();     
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if(ui.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }


    public void Retry()
    {
        Toggle();
        sceneFade.FadeTo(SceneManager.GetActiveScene().name);
    }


    public void Menu()
    {
        Toggle();
        sceneFade.FadeTo(menuSceneName);
    }

}
