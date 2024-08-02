using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    
    public string menuSceneName = "MainMenu";
    public SceneFade sceneFade;
   
    public void Retry()
    {
        sceneFade.FadeTo(SceneManager.GetActiveScene().name);
    }


    public void Menu()
    {
        
        sceneFade.FadeTo(menuSceneName);
    }


}
