using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ScriptableObject {
    public int playerNumber { get; private set; }
    public static int totalNumber = 1;
    public GameObject charPrefab;
    public GameObject charInstantiated;
    public bool isAlive = true;

    public Player()
    {
        playerNumber = totalNumber;
        totalNumber += 1;
    }

    public void chooseChar(GameObject prefab)
    {
        charPrefab = prefab;
    }

    public void SpawnPlayerChar(Vector3 spawnPos, Transform parent)
    {
        charInstantiated = Instantiate(charPrefab, spawnPos, Quaternion.identity, parent);
    }

}
