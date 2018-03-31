using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    int playerCount;
    public float gameStartAfter { get; private set; }
    public float beginning { get; private set; }
    public enum GameState
    {
        starting, 
        running,
        finished
    }

    public GameState gameStatus = GameState.starting;
    // Use this for initialization
    void Start()
    {
        gameStartAfter = 5f;
        Time.timeScale = 0;
        beginning = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > gameStartAfter + beginning)
        {
            StartGame();
            playerCount = PlayerManager.Instance.players.Length;
            if (playerCount <= 1)
            {
                gameStatus = GameState.finished;
            }
            else
            {
                gameStatus = GameState.running;
            }
        }
    }

    public void StartGame()
    {
        gameStatus = GameState.running;
        Time.timeScale = 1;
    }
}
