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

    protected Vector3 CharSpeed;
    protected Animator anim;
    protected bool Attacking;
    protected bool move;

    public abstract void moveCharacter(Vector3 amount);
    public abstract void RoundHouse();
    public abstract void Jump();
    public abstract void Punch();
    public abstract void Taunt();
    public abstract void Hadouken();
    protected abstract IEnumerator waitforattack(float f);
    protected abstract IEnumerator waitforHadouken(float f);

    public void DamagePlayer(float damage)
    {
        currHealth -= damage;

        healthBar.SetHealth(currHealth);
    }
}
