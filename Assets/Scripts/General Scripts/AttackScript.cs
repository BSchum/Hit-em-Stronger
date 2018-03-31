using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : Scripts
{
    // Update is called once per frame
    void Update()
    {
        if (Time.time > player.weapon.atkDuration + player.weapon.lastAtk)
        {
            player.weapon.PutWeaponDown();
        }
    }
    public void Attack()
    {
        if (Time.time > player.weapon.lastAtk + player.weapon.cooldownAtk)
        {
            player.weapon.Attack();
            player.weapon.lastAtk = Time.time;
        }
    }

}
