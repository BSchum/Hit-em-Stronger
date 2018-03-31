using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager> {
    /*
     * This class is meant to control input and call right things
     */
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        foreach (GameObject player in PlayerManager.Instance.players)
        {
            if(player.GetComponent<Player>().status == Player.State.ready)
            {
                int playerNumber = player.GetComponent<Player>().playerNumber;
                MovementScript playerMvmt = player.GetComponent<MovementScript>();
                AttackScript playerAtk = player.GetComponent<AttackScript>();
                float horizontal = Input.GetAxisRaw(Constants.PLAYERS_HORIZONTAL_INPUT + playerNumber);

                playerMvmt.SetMovementAndDirectionState(horizontal);
                CheckJumpButton(playerNumber, playerMvmt);
                CheckFallOffButton(playerNumber, playerMvmt);
                CheckAttackButton(playerNumber, playerAtk);
            }
        }
    }

    private void CheckAttackButton(int playerNumber, AttackScript playerAtk)
    {
        if(Input.GetButton(Constants.PLAYER_ATTACK_INPUT + playerNumber))
        {
            playerAtk.Attack();
        }
    }

    void CheckJumpButton(int playerNumber, MovementScript playerMvmt)
    {
        if (Input.GetButtonDown(Constants.PLAYER_JUMP_INPUT + playerNumber))
        {
            playerMvmt.Jump();
        }
    }

    void CheckFallOffButton(int playerNumber, MovementScript playerMvmt)
    {
        if (Input.GetButtonDown(Constants.PLAYER_FALLOFF_INPUT + playerNumber))
        {
            playerMvmt.Fall();
        }
    }
}
