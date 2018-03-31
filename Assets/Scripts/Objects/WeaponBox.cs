using UnityEngine;
using System.Collections;

public class WeaponBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Constants.PLAYERS_TAG)
        {
            Weapon weapon = GetComponentInParent<Weapon>();
            HealthScript healthScript = collision.gameObject.GetComponent<HealthScript>();
            healthScript.GetDamageFrom(weapon);
        }
    }
}
