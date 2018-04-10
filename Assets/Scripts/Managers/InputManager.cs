using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager> {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        foreach (Player player in PlayerManager.instance.alivePlayers)
        {
            if(player.charInstantiated && player.charInstantiated.GetComponent<Characters>().status == Characters.State.ready)
            {
                int playerNumber = player.playerNumber;
                MovementScript playerMvmt = player.charInstantiated.GetComponent<MovementScript>();
                AttackScript playerAtk = player.charInstantiated.GetComponent<AttackScript>();

                float horizontal = Input.GetAxisRaw(Constants.PLAYERS_HORIZONTAL_INPUT + playerNumber);

                playerMvmt.SetMovementAndDirectionState(horizontal);
                CheckJumpButton(playerNumber, playerMvmt);
                CheckFallOffButton(playerNumber, playerMvmt);
                CheckAttackButton(playerNumber, playerAtk);
                CheckSpellButton(playerNumber, playerAtk);
            }
        }
    }

    private void CheckAttackButton(int playerNumber, AttackScript playerAtk)
    {
        if(Input.GetButton(Constants.PLAYER_ATTACK_INPUT + playerNumber))
        {
            playerAtk.WeaponAttack();
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

    void CheckSpellButton(int playerNumber, AttackScript playerAtk)
    {
        if(Input.GetButtonDown(Constants.PLAYER_SPELL_INPUT + playerNumber))
        {
            playerAtk.SpellAttack();
        }
    }
}
