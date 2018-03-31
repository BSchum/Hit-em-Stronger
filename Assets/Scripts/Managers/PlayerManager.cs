using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {
    public GameObject[] players { get; private set; }
    public GameObject parent;
    public GameObject playerPrefab;
    public int numberPlayerToSpawn;
	// Use this for initialization
	void Start () {
        numberPlayerToSpawn = SupplyDepot.playerNumber;
        SpawnPlayers();
        players = GameObject.FindGameObjectsWithTag(Constants.PLAYERS_TAG);

    }

    // Update is called once per frame
    void FixedUpdate () {
        players = GameObject.FindGameObjectsWithTag(Constants.PLAYERS_TAG);
    }

    void SpawnPlayers()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(Constants.SPAWNPOINT_TAG);
        List<int> spawnsIndexes = ChooseSpawnPoint(spawnPoints);
        foreach(int spawnIndex in spawnsIndexes)
        {
            Instantiate(playerPrefab, spawnPoints[spawnIndex].transform.position, new Quaternion(0, 0, 0, 0), parent.transform);
        }
    }

    List<int> ChooseSpawnPoint(GameObject[] spawnPoints)
    {
        int playerSpawned = 1;
        List<int> spawnChoosed = new List<int>();
        spawnChoosed.Add(Random.Range(0, spawnPoints.Length));
        int randomIndex = 0;
        while (playerSpawned < numberPlayerToSpawn)
        {
            do
            {
                randomIndex = Random.Range(0, spawnPoints.Length);
            } while (spawnChoosed.Contains(randomIndex));
            spawnChoosed.Add(randomIndex);
            playerSpawned++;
        }
        return spawnChoosed;
    }
}
