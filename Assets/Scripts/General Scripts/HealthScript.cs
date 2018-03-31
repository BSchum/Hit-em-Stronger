using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : Scripts {

	// Update is called once per frame
	void Update () {
        if (player.health <= 0)
        {
            Die();
        }
	}
    public void GetDamageFrom(Weapon weapon)
    {
        player.takeDamage(weapon.damage);
        Debug.Log(player.health);
    }
    void Die()
    {
        Destroy(this.gameObject);
    }


}
