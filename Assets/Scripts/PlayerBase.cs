﻿using System.Collections;
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
    protected bool willMoveForward;
    protected bool willMoveBackward;

    public bool isTraining = false;

    protected Vector3 CharSpeed;
    public Animator anim;
    protected bool CannotAttack;
    protected bool Blocking = false;
    protected bool move;

    public Hitbox LHand, LFoot, RHand, RFoot;
    protected string color;

    public float kickCooldown;
    public float punchCooldown;
    public float hadoukenCooldown;
    public float tauntCooldown;
    public float jabCooldown;
    public float quickKickCooldown;
    public float takeHitCooldown;



    public void moveCharacter(Vector3 amount)
    {
        gameObject.transform.Translate(amount);
    }

    public void moveForward(){
        if(!CannotAttack){
            willMoveForward = true;
            willMoveBackward = false;
        }else{
            // Debug.Log(gameObject.name + " on cooldown");
        }
    }

    public void moveBackward(){
        if(!CannotAttack){
            willMoveBackward = true;
            willMoveForward = false;
        }else{
            // Debug.Log(gameObject.name + " on cooldown");
        }
    }

    public void stopMoving(){
        willMoveForward = false;
        willMoveBackward = false;
    }

    public void RoundHouse()
    {
        if (!CannotAttack)
        {
            CannotAttack = true;
            setHitboxes(MoveTable.move.sK);
            anim.SetTrigger("RoundHouse");
            StartCoroutine(waitforattack(kickCooldown));
        }
    }
    public void Punch()
    {
        if (!CannotAttack)
        {
            CannotAttack = true;
            setHitboxes(MoveTable.move.sP);
            anim.SetTrigger("Punch");
            StartCoroutine(waitforattack(punchCooldown));
        }
    }
    public void Taunt()
    {
        if (!CannotAttack)
        {
            CannotAttack = true;
            anim.SetTrigger("Taunt");
            StartCoroutine(waitforattack(tauntCooldown));
        }
    }
    public void Hadouken()
    {
        if (!CannotAttack && specialBar.isFull())
        {
            CannotAttack = true;
            anim.SetTrigger("Hadouken");
            StartCoroutine(waitforHadouken(0.9f));
            specialBar.ResetLevel();
            StartCoroutine(waitforattack(hadoukenCooldown));
        }

    }
    public void Jab()
    {
        if (!CannotAttack)
        {
            CannotAttack = true;
            setHitboxes(MoveTable.move.wP);
            anim.SetTrigger("Jab");
            StartCoroutine(waitforattack(jabCooldown));
        }
    }
    public void QuickKick()
    {
        if (!CannotAttack)
        {
            CannotAttack = true;
            setHitboxes(MoveTable.move.wK);
            anim.SetTrigger("QuickKick");
            StartCoroutine(waitforattack(quickKickCooldown));
        }
    }
    public void Block()
    {
        if (!CannotAttack)
        {
            CannotAttack = true;
            Blocking = true;
            anim.SetBool("Blocking", true);
        }
    }
    public void UnBlock()
    {
        CannotAttack = false;
        Blocking = false;
        anim.SetBool("Blocking", false);
    }
    public void TakeHit()
    {
        CannotAttack = true;
        anim.SetTrigger("TakeHit");
        StartCoroutine(waitforattack(takeHitCooldown));
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
        // reduce damage if blocking
        // multiply by 0.2 = take 80% less damage
        if (Blocking)
            damage *= 0.2f;

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
        // its debatable if we want to change knockback if you're blocking
        // if we do want to, edit the multiplier here

        if (Blocking){
            force *= 1.0f;
        }else{
            TakeHit();
        }
        if(isFacingRight){
            force *= -1;
        }
        GetComponent<Rigidbody>().AddForce(new Vector3 (0, 0, force*10));
    }

    protected void setHitboxes(MoveTable.move m)
    {
        LHand.set(baseAttack * MoveTable.use(m, color, MoveTable.hitbox.lh));
        RHand.set(baseAttack * MoveTable.use(m, color, MoveTable.hitbox.rh));
        LFoot.set(baseAttack * MoveTable.use(m, color, MoveTable.hitbox.lf));
        RFoot.set(baseAttack * MoveTable.use(m, color, MoveTable.hitbox.rf));
    }

    public bool specialReady(){
        return specialBar.isFull();
    }

    protected void handleAIMovement()
    {
        float horz = 0;

        if (willMoveForward)
        {
            if (isFacingRight)
            {
                horz = 1;
            }
            else
            {
                horz = 1;
            }
            // willMoveForward = false;
        }
        if (willMoveBackward)
        {
            if (isFacingRight)
            {
                horz = -1;
            }
            else
            {
                horz = -1;
            }
            // willMoveBackward = false;
        }

        if(willMoveForward == willMoveBackward){
            horz = 0;
        }

        horz = horz * Time.deltaTime * Speed;

        if (horz != 0)
        {
            move = true;
        }
        else
        {
            move = false;
        }

        anim.SetBool("Move", move);
        if (horz > 0) anim.SetFloat("Velocity", Speed);
        if (horz < 0) anim.SetFloat("Velocity", -Speed);
        CharSpeed = new Vector3(0f, 0f, horz);
        moveCharacter(CharSpeed);

    }

}
