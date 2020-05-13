using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedController : PlayerBase
{   
    // Start is called before the first frame update
    void Start()
    {
        jumpCooldown = 1f;
        kickCooldown = 0.9f;
        punchCooldown = 0.8f;
        hadoukenCooldown = 0.9f;
        tauntCooldown = 4.45f;
        jabCooldown = 0.45f;
        quickKickCooldown = 0.6f;
        takeHitCooldown = 0.5f;

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
        characterName = "Red Girl";
        baseAttack = 1;
        Speed = 3;
        color = "Red";
        //Projectile = (GameObject)Resources.Load("Prefab/BlueHadouken", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAI)
        {
            float horz = 0;
            if (Input.GetKey(KeyCode.A))
            {
                horz = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                horz = 1;
            }
            if (Input.GetKey(KeyCode.A) == Input.GetKey(KeyCode.D))
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
            if (horz > 0) anim.SetFloat("Velocity", Speed);
            if (horz < 0) anim.SetFloat("Velocity", -Speed);

            CharSpeed = new Vector3(0f, 0f, horz);
            if (!CannotAttack)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    RoundHouse();
                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    Jab();
                }
                if (Input.GetKeyDown(KeyCode.J))
                {
                    QuickKick();
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    Punch();
                }
                if (Input.GetKeyDown(KeyCode.N))
                {
                    Taunt();
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    Hadouken();
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    Block();
                }
                moveCharacter(CharSpeed);
            }
            if (Input.GetKeyUp(KeyCode.L))
            {
                UnBlock();
            }
            if (Input.GetKeyUp(KeyCode.M))
            {
                TakeHit();
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                DamagePlayer(10);
            }

        }else{
            //Debug code, breaks AI so only uncomment if you need it
            // if(Input.GetKey(KeyCode.Z)){
            //     moveForward();
            // }else if(Input.GetKey(KeyCode.X)){
            //     moveBackward();
            // }else{
            //     stopMoving();
            // }


            handleAIMovement();
        }
    }

}





