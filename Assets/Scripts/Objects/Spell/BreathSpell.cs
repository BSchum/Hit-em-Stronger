using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathSpell : Spell {
    public ParticleSystem particleSystemPrefab;
    public ParticleSystem particleSystem;
    // Use this for initialization
    protected void Awake()
    {
        damage = 0.5f;
        spellDuration = 1f;
        spellCooldown = 1.5f;
        lastSpell = 0f;
    }

    public override void Use()
    {
        if (particleSystem == null)
        {
            particleSystem = Instantiate(particleSystemPrefab, this.transform.position, new Quaternion(0, 0, 0, 0), this.gameObject.transform);
        }
    }

    public void Update()
    {
        if (particleSystem != null)
        {
            MovementScript playerMvmt = GetComponentInParent<MovementScript>();
            particleSystem.GetComponent<Transform>().localScale = new Vector3((int)playerMvmt.GetDirection(), 1, 1);
        }
    }

    public override void StopSpell()
    {
        if(particleSystem != null)
        {
            Destroy(particleSystem.gameObject);
        }
    }
}
