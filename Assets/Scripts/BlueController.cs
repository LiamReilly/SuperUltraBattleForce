﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueController : PlayerBase
{
    // Start is called before the first frame update
    void Start()
    {
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
        if(Input.GetKey(KeyCode.RightArrow) == Input.GetKey(KeyCode.LeftArrow))
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
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            RoundHouse();
            StartCoroutine(waitforattack(1.8f));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
            StartCoroutine(waitforattack(1f));
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Punch();
            StartCoroutine(waitforattack(2f));
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Taunt();
            StartCoroutine(waitforattack(2.7f));
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Hadouken();
            StartCoroutine(waitforattack(3.6f));
        }

        CharSpeed = new Vector3(0f, 0f, horz);
        if (!Attacking)
        {
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
        StartCoroutine(waitforHadouken(1.8f));
    }
    protected override IEnumerator waitforattack(float f)
    {
        yield return new WaitForSeconds(f);
        Attacking = false;

    }
    protected override IEnumerator waitforHadouken(float f)
    {
        yield return new WaitForSeconds(f);
        Hadouken h = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.2f, gameObject.transform.position.z+0.2f), Quaternion.identity).GetComponent<Hadouken>();
        if(isFacingRight){
            h.Initialize(Vector3.forward, this);
        }else{
            h.Initialize(-Vector3.forward, this);
        }    
    }

}
