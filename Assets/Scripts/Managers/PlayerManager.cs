using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {
    SupplyDepot sp;
    public GameObject[] currentPlayers { get; private set; }
    public GameObject parent;
    public GameObject firePrefab;
    public GameObject icePrefab;
    public Player[] allPlayers;
    public List<Player> alivePlayers;
	// Use this for initialization
	void Start () {
        allPlayers = SupplyDepot.players;
        alivePlayers = SupplyDepot.players.ToList();
        SpawnPlayers();
        
    }

    // Update is called once per frame
    void Update () {
        foreach(Player p in allPlayers)
        {
            if(p.charInstantiated == null)
            {
                alivePlayers.Remove(p);
            }
        }
    }

    void SpawnPlayers()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(Constants.SPAWNPOINT_TAG);
        List<int> spawnsIndexes = ChooseSpawnPoint(spawnPoints);
        int i = 0;
        foreach (int spawnIndex in spawnsIndexes)
        {
            allPlayers[i].SpawnPlayerChar(spawnPoints[spawnIndex].transform.position, parent.transform);
            
            i++;
        }
    }

    List<int> ChooseSpawnPoint(GameObject[] spawnPoints)
    {
        int playerSpawned = 1;
        List<int> spawnChoosed = new List<int>();
        spawnChoosed.Add(Random.Range(0, spawnPoints.Length));
        int randomIndex = 0;
        while (playerSpawned < allPlayers.Length)
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
