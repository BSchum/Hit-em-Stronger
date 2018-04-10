using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour {
    public float speed { get; private set; }
    public float jumpForce { get; private set; }
    public float health { get; private set; }
    public Weapon weapon;
    public Spell spell;
    public float fury { get; private set; }

    public enum State{
        ready,
        notReady
    }
    public State status = State.notReady;

    //Fury is a damage multiplicator
    //The more you hit with attack, the more damage you will do
    public enum Fury 
    {
        stade1 = 1,
        stade2 = 3, 
        stade3 = 6
    }
    public Fury furyStade;

    // Use this for initialization
    void Awake() {
        speed = 6f;
        jumpForce = 7f;
        health = 100f;
        weapon = GetComponentInChildren<Weapon>();
        spell = GetComponentInChildren<Spell>();
        furyStade = Fury.stade1;
        status = State.ready;

    }
	
	// Update is called once per frame
	void Update () {
        if(fury > 30)
        {
            furyStade = Fury.stade2;
        }
        else if(fury > 90)
        {
            furyStade = Fury.stade3;
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }

    public void GainFury(float fury)
    {
        this.fury += fury;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Constants.PLAYERS_TAG)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
        }

        if(collision.gameObject.tag == Constants.MOVING_PLATEFORM_TAG)
        {
            this.transform.parent = collision.gameObject.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Constants.MOVING_PLATEFORM_TAG)
        {
            this.transform.parent = GameObject.Find(Constants.PLAYER_PARENT_GO).transform;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.GetComponentInParent<Characters>().gameObject != this.gameObject)
        {
            GetComponent<HealthScript>().GetDamageFrom(other.GetComponentInParent<Spell>());
        }
    }


}
