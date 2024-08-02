using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver;
    
    public GameObject gameOverUI;

    public GameObject completeLevelUI;

    


    private void Start()
    {
        isGameOver = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        if(PlayerState.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
         isGameOver = true;  
         gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        isGameOver = true;
        completeLevelUI.SetActive(true);
    }

}
