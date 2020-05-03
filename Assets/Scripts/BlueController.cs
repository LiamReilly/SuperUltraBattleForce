﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueController : PlayerBase
{
<<<<<<< HEAD
    public const float JumpCooldown = 1f;
    public const float KickCooldown = 1.2f;
    public const float PunchCooldown = 1.2f;
    public const float HadoukenCooldown = 0.9f;
    public const float TauntCooldown = 2.7f;


=======
    
    
>>>>>>> 2778617f8c791d5d22d06f909c7e54621ec1349d
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        currHealth = 100;
        maxHealth = 100;
        characterName = "Blue Guy";
        baseAttack = 1.3f;
        Speed = 1.7f;

    }

    // Update is called once per frame
    void Update()
    {
        float horz = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horz = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horz = 1;
        }
        if (Input.GetKey(KeyCode.RightArrow) == Input.GetKey(KeyCode.LeftArrow))
        {
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
        if (horz > 0) anim.SetFloat("Velocity", 1);
        if (horz < 0) anim.SetFloat("Velocity", -1);
        CharSpeed = new Vector3(0f, 0f, horz);
        if (!Attacking)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                RoundHouse();
                StartCoroutine(waitforattack(KickCooldown));
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
                StartCoroutine(waitforattack(JumpCooldown));
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                Punch();
                StartCoroutine(waitforattack(PunchCooldown));
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                Taunt();
                StartCoroutine(waitforattack(TauntCooldown));
            }
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                Hadouken();
                StartCoroutine(waitforattack(HadoukenCooldown));
            }
            moveCharacter(CharSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            DamagePlayer(10);
        }


    }
    public override void moveCharacter(Vector3 amount)
    {
        gameObject.transform.Translate(amount);
    }
    public override void RoundHouse()
    {
        Attacking = true;
        anim.SetTrigger("RoundHouse");
    }
    public override void Jump()
    {
        Attacking = true;
        anim.SetTrigger("Jump");
    }
    public override void Punch()
    {
        Attacking = true;
        anim.SetTrigger("Punch");
        //audioSource.PlayOneShot(punch, 0.1f);
    }
    public override void Taunt()
    {
        Attacking = true;
        anim.SetTrigger("Taunt");
    }
    public override void Hadouken()
    {
        Attacking = true;
        anim.SetTrigger("Hadouken");
        StartCoroutine(waitforHadouken(0.9f));
    }
    protected override IEnumerator waitforattack(float f)
    {
        yield return new WaitForSeconds(f);
        Attacking = false;

    }
    protected override IEnumerator waitforHadouken(float f)
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
<<<<<<< HEAD
=======
        audioSource.PlayOneShot(hadouken, 0.1f);
>>>>>>> 2778617f8c791d5d22d06f909c7e54621ec1349d
    }

}
