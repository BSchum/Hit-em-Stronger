using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float damage { get; private set; }
    public float lastAtk { get; set; }
    public float cooldownAtk { get; private set; }
    public float atkDuration;

    public GameObject weaponBox;
    // Use this for initialization
    void Start () {
        damage = 10f;
        lastAtk = 0f;
        cooldownAtk = 0.5f;
        atkDuration = 0.1f;
        weaponBox.SetActive(false);

    }

    // Update is called once per frame
    void Update () {
	}

    public void Attack()
    {
        weaponBox.SetActive(true);
    }

    public void PutWeaponDown()
    {
        weaponBox.SetActive(false);
    }


}
