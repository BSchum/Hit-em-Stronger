using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    int playerCount;
    public float gameStartAfter { get; private set; }
    public float beginning { get; private set; }
    public static List<int> winners;
    static int actualRound = 0;
    int maxRound = 0;
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
        if (winners == null)
        {
            winners = new List<int>();
        }
        gameStartAfter = 5f;
        Time.timeScale = 0;
        beginning = Time.realtimeSinceStartup;
        maxRound = SupplyDepot.roundNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > gameStartAfter + beginning && gameStatus != GameState.finished)
        {
            StartGame();
            playerCount = PlayerManager.Instance.alivePlayers.Count;
            Debug.Log(gameStatus);
            if (playerCount <= 1)
            {
                winners.Add(PlayerManager.Instance.alivePlayers[0].playerNumber);
                gameStatus = GameState.finished;
                if (actualRound < maxRound)
                {
                    actualRound++;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                else
                {
                    int maxRepeated = winners.GroupBy(s => s)
                                             .OrderByDescending(s => s.Count())
                                             .First().Key;
                    SupplyDepot.winner = maxRepeated;
                    //TODO Handle equality
                    SceneManager.LoadScene("EndGameBoard");
                }
            }
        }
    }

    public void StartGame()
    {
        gameStatus = GameState.running;
        Time.timeScale = 1;
    }
}
