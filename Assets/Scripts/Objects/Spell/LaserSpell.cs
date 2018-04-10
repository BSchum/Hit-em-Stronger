using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpell : Spell
{

    public float distance;

    protected void Awake()
    {
        damage = 20f;
        spellDuration = 0.1f;
        spellCooldown = 2f;
        lastSpell = 0f;
        distance = 2.5f;
    }


    public void Update()
    {
        if(Time.time > spellDuration + lastSpell)
        {
            GetComponentsInChildren<Transform>(true)[1].gameObject.SetActive(false);
        }
    }

    public override void StopSpell()
    {

    }

    public override void Use()
    {
        GetComponentsInChildren<Transform>(true)[1].gameObject.SetActive(true);
        Debug.Log(GetComponentsInChildren<Transform>(true)[1].gameObject);
        MovementScript playerMvmt = GetComponentInParent<MovementScript>();
        Debug.DrawRay(transform.position, new Vector3((int)playerMvmt.GetDirection(), 0, 0), Color.red, 1f);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector3((int)playerMvmt.GetDirection(), 0, 0), distance);
        Debug.Log(hit.distance);
        if (hit && hit.collider.gameObject.tag == Constants.PLAYERS_TAG && hit.distance <= distance)
        {
           HealthScript hs =  hit.collider.gameObject.GetComponent<HealthScript>();
           hs.GetDamageFrom(this);
        }
        lastSpell = Time.time;
    }

    // Use this for initialization
    void Start()
    {

    }
}