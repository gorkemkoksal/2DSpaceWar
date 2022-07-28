using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        GameSession myGame = FindObjectOfType<GameSession>();
        if (myGame != null)
        {
            myGame.ResetGame();
        }
        
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitandLoad());
        
    }
    IEnumerator WaitandLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game over");

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
