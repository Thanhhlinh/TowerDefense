using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public string menuSceneName = "MainMenu";
    public string nextLevel = "Level02";
    public int levelToUnlock = 2;
    public SceneFade sceneFade;

   

    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFade.FadeTo(nextLevel);

    }
    public void Menu()
    {

        sceneFade.FadeTo(menuSceneName);
    }
}
