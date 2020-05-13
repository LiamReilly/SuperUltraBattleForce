using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    [Header("Character Specifics")]
    public float currHealth;
    public float maxHealth;
    public string characterName;
    public float baseAttack;
    public float Speed;
    public GameObject Projectile;
    public HealthBar healthBar;
    public bool isFacingRight;
    public AudioSource audioSource;
    public AudioClip hadouken;
    public AudioClip punch;

    protected Vector3 CharSpeed;
    protected Animator anim;
    protected bool Attacking;
    protected bool move;

    public Hitbox LHand, LFoot, RHand, RFoot;


    public abstract void moveCharacter(Vector3 amount);
    public abstract void RoundHouse();
    public abstract void QuickKick();
    public abstract void Jump();
    public abstract void Jab();
    public abstract void Punch();
    public abstract void Taunt();
    public abstract void Hadouken();
    public abstract void Block();
    public abstract void UnBlock();
    public abstract void TakeHit();
    protected abstract IEnumerator waitforattack(float f);
    protected abstract IEnumerator waitforHadouken(float f);
 
    public void DamagePlayer(float damage)
    {
        currHealth -= damage;

        healthBar.SetHealth(currHealth);
    }

    public void KnockbackPlayer(float force)
    {
        // ???
    }
}
