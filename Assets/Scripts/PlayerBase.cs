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

    public bool isAI = false;
    public AIController aIController;

    protected Vector3 CharSpeed;
    protected Animator anim;
    protected bool Attacking;
    protected bool move;

    public Hitbox LHand, LFoot, RHand, RFoot;
    protected string color;


    public void moveCharacter(Vector3 amount)
    {
        gameObject.transform.Translate(amount);
    }
    public void RoundHouse()
    {
        Attacking = true;
        setHitboxes(MoveTable.move.sK);
        anim.SetTrigger("RoundHouse");
    }
    public void Jump()
    {
        Attacking = true;
        anim.SetTrigger("Jump");
    }
    public void Punch()
    {
        Attacking = true;
        setHitboxes(MoveTable.move.sP);
        anim.SetTrigger("Punch");
    }
    public void Taunt()
    {
        Attacking = true;
        anim.SetTrigger("Taunt");
    }
    public void Hadouken()
    {
        Attacking = true;
        anim.SetTrigger("Hadouken");
        StartCoroutine(waitforHadouken(0.9f));
    }
    public void Jab()
    {
        Attacking = true;
        setHitboxes(MoveTable.move.wP);
        anim.SetTrigger("Jab");
    }
    public void QuickKick()
    {
        Attacking = true;
        setHitboxes(MoveTable.move.wK);
        anim.SetTrigger("QuickKick");
    }
    public void Block()
    {
        Attacking = true;
        anim.SetBool("Blocking", true);
    }
    public void UnBlock()
    {
        Attacking = false;
        anim.SetBool("Blocking", false);
    }
    public void TakeHit()
    {
        Attacking = true;
        anim.SetTrigger("TakeHit");
    }
    protected IEnumerator waitforattack(float f)
    {
        yield return new WaitForSeconds(f);
        Attacking = false;
        LHand.Reset();
        RHand.Reset();
        LFoot.Reset();
        RFoot.Reset();
    }
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

    protected void setHitboxes(MoveTable.move m)
    {
        LHand.set(MoveTable.use(m, color, MoveTable.hitbox.lh));
        RHand.set(MoveTable.use(m, color, MoveTable.hitbox.rh));
        LFoot.set(MoveTable.use(m, color, MoveTable.hitbox.lf));
        RFoot.set(MoveTable.use(m, color, MoveTable.hitbox.rf));
    }
}
