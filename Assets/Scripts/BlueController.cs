using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueController : PlayerBase
{
    // Start is called before the first frame update
    void Start()
    {
        jumpCooldown = 1f;
        kickCooldown = 0.9f;
        punchCooldown = 0.8f;
        hadoukenCooldown = 0.9f;
        tauntCooldown = 2.7f;
        jabCooldown = 0.45f;
        quickKickCooldown = 0.6f;
        takeHitCooldown = 0.3f;

        audioSource = GetComponent<AudioSource>();
        if (gameObject.transform.position.z < 0)
        {
            //healthBar = GameObject.FindGameObjectWithTag("Bar1").GetComponent<HealthBar>();
            isFacingRight = true;
        }
        else
        {
            //healthBar = GameObject.FindGameObjectWithTag("Bar2").GetComponent<HealthBar>();
            isFacingRight = false;
        }
        anim = GetComponent<Animator>();
        currHealth = 100;
        maxHealth = 100;
        characterName = "Blue Guy";
        baseAttack = 1.3f;
        Speed = 1.7f;
        color = "Blue";
        //Projectile = (GameObject)Resources.Load("Prefab/RedHadouken", typeof(GameObject));

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
        if (!CannotAttack)
        {
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                RoundHouse();
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                Jab();
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                QuickKick();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                Punch();
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                Taunt();
            }
            if (Input.GetKeyDown(KeyCode.Keypad6) && specialBar.GetLevel() == 100f)
            {
                Hadouken();
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                Block();
            }

            moveCharacter(CharSpeed);
        }
        if (Input.GetKeyUp(KeyCode.Keypad3))
        {
            UnBlock();
        }
        if (Input.GetKeyUp(KeyCode.Keypad0))
        {
            TakeHit();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            DamagePlayer(10);
        }


    }

}
