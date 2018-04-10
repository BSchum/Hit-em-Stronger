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

        if (Time.time > player.spell.lastSpell + player.spell.spellDuration)
        {
            player.spell.StopSpell();

        }
    }
    public void WeaponAttack()
    {
        if (Time.time > player.weapon.lastAtk + player.weapon.cooldownAtk)
        {
            player.weapon.Attack();
            player.weapon.lastAtk = Time.time;

        }
    }

    public void SpellAttack()
    {
        if(Time.time > player.spell.lastSpell + player.spell.spellCooldown)
        {
            player.spell.Use();
            player.spell.lastSpell = Time.time;

        }
    }

}
