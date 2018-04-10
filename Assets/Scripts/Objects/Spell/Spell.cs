using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour
{
    public float damage;
    public float spellDuration;
    public float spellCooldown;
    public float lastSpell;

    public abstract void Use();

    public abstract void StopSpell();
}
