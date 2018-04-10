using UnityEngine;
using System.Collections;

public class WeaponBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.PLAYERS_TAG)
        {
            Weapon weapon = GetComponentInParent<Weapon>();
            Characters player = weapon.GetComponentInParent<Characters>();
            player.GainFury(Constants.FURY_GAIN_SIMPLE_ATTACK);

            HealthScript healthScript = collision.gameObject.GetComponent<HealthScript>();
            healthScript.GetDamageFrom(weapon);
        }
    }
}
