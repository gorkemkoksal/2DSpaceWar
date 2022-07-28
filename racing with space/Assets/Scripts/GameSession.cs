using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    void Awake()
    {
        SetUpSingleton();
    }
    private void SetUpSingleton()
    {
        int countGameSessions = FindObjectsOfType<GameSession>().Length;
        if(countGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }
    public void AddToScore(int ScoreValue)
    {
        score += ScoreValue;
    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }

}
