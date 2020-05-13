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
    public SpecialBar specialBar;
    public bool isFacingRight;
    public AudioSource audioSource;
    public AudioClip hadouken;
    public AudioClip punch;

    public bool isAI = false;
    public AIController aIController;

    public bool isTraining = false;

    protected Vector3 CharSpeed;
    protected Animator anim;
    protected bool CannotAttack;
    protected bool move;

    public Hitbox LHand, LFoot, RHand, RFoot;
    protected string color;

    public float JumpCooldown = 1f;
    public float KickCooldown = 0.9f;
    public float PunchCooldown = 0.8f;
    public float HadoukenCooldown = 0.9f;
    public float TauntCooldown = 2.7f;
    public float JabCooldown = 0.45f;
    public float QuickKickCooldown = 0.6f;
    public float TakeHitCooldown = 0.3f;


    public void moveCharacter(Vector3 amount)
    {
        gameObject.transform.Translate(amount);
    }
    public void RoundHouse()
    {
        CannotAttack = true;
        setHitboxes(MoveTable.move.sK);
        anim.SetTrigger("RoundHouse");
        StartCoroutine(waitforattack(KickCooldown));
    }
    public void Jump()
    {
        CannotAttack = true;
        anim.SetTrigger("Jump");
        StartCoroutine(waitforattack(JumpCooldown));
    }
    public void Punch()
    {
        CannotAttack = true;
        setHitboxes(MoveTable.move.sP);
        anim.SetTrigger("Punch");
        StartCoroutine(waitforattack(PunchCooldown));
    }
    public void Taunt()
    {
        CannotAttack = true;
        anim.SetTrigger("Taunt");
        StartCoroutine(waitforattack(TauntCooldown));
    }
    public void Hadouken()
    {
        CannotAttack = true;
        anim.SetTrigger("Hadouken");
        StartCoroutine(waitforHadouken(0.9f));
        specialBar.AddLevel(-100);
        StartCoroutine(waitforattack(HadoukenCooldown));
    }
    public void Jab()
    {
        CannotAttack = true;
        setHitboxes(MoveTable.move.wP);
        anim.SetTrigger("Jab");
        StartCoroutine(waitforattack(JabCooldown));
    }
    public void QuickKick()
    {
        CannotAttack = true;
        setHitboxes(MoveTable.move.wK);
        anim.SetTrigger("QuickKick");
        StartCoroutine(waitforattack(QuickKickCooldown));
    }
    public void Block()
    {
        CannotAttack = true;
        anim.SetBool("Blocking", true);
    }
    public void UnBlock()
    {
        CannotAttack = false;
        anim.SetBool("Blocking", false);
    }
    public void TakeHit()
    {
        CannotAttack = true;
        anim.SetTrigger("TakeHit");
        StartCoroutine(waitforattack(TakeHitCooldown));
    }
    protected IEnumerator waitforattack(float f)
    {
        yield return new WaitForSeconds(f);
        CannotAttack = false;
        LHand.Reset();
        RHand.Reset();
        LFoot.Reset();
        RFoot.Reset();
    }

    protected IEnumerator waitforHadouken(float f)
    {
        yield return new WaitForSeconds(f);
        Hadouken h = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.2f, gameObject.transform.position.z), Quaternion.identity).GetComponent<Hadouken>();
        if (isFacingRight)
        {
            h.Initialize(Vector3.forward, this);
        }
        else
        {
            h.Initialize(-Vector3.forward, this);
        }
        audioSource.PlayOneShot(hadouken, 0.1f);
    }

    public void DamagePlayer(float damage)
    {
        currHealth -= damage;

        if(isTraining && currHealth <= 0){
            currHealth = 100;
        }

        healthBar.SetHealth(currHealth);
    }
    public void IncreaseSpecial(float damage)
    {

        specialBar.AddLevel(damage);
    }

    public void KnockbackPlayer(float force)
    {
        TakeHit();
        if(isFacingRight){
            force *= -1;
        }
        GetComponent<Rigidbody>().AddForce(new Vector3 (0, 0, force*10));
    }

    protected void setHitboxes(MoveTable.move m)
    {
        LHand.set(MoveTable.use(m, color, MoveTable.hitbox.lh));
        RHand.set(MoveTable.use(m, color, MoveTable.hitbox.rh));
        LFoot.set(MoveTable.use(m, color, MoveTable.hitbox.lf));
        RFoot.set(MoveTable.use(m, color, MoveTable.hitbox.rf));
    }

}
