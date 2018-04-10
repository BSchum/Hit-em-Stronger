using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : Scripts {

	// Update is called once per frame
	void Update () {
        if (player.health <= 0)
        {
            Die();
            player.status = Characters.State.notReady;
        }
	}
    public void GetDamageFrom(Weapon weapon)
    {
        int dmgMultiplicator = (int)weapon.GetComponentInParent<Characters>().furyStade;
        player.takeDamage(weapon.damage * dmgMultiplicator);

    }

    public void GetDamageFrom(Spell spell)
    {
        int dmgMultiplicator = (int)spell.GetComponentInParent<Characters>().furyStade;
        player.takeDamage(spell.damage * dmgMultiplicator);
    }
    void Die()
    {
        //Tell to the player attached we are dead ( attached a player to the characters)
        Destroy(this.gameObject);
    }


}
