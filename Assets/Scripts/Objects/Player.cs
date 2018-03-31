using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int playerNumber { get; private set; }

    public float speed { get; private set; }
    public float jumpForce { get; private set; }

    public float health { get; private set; }

    public Weapon weapon;

    public static int totalPlayer = 0;

    public enum State{
        ready,
        notReady
    }
    public State status = State.notReady;
	// Use this for initialization
	void Awake() {
        totalPlayer += 1;
        playerNumber = totalPlayer;
        speed = 6f;
        jumpForce = 7f;
        health = 100f;
        weapon = GetComponentInChildren<Weapon>();
        status = State.ready;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }

}
