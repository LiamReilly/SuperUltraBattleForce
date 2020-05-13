using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedController : PlayerBase
{   
    public const float JumpCooldown = 1f;
    public const float KickCooldown = 0.9f;
    public const float PunchCooldown = 0.8f;
    public const float HadoukenCooldown = 0.9f;
    public const float TauntCooldown = 4.45f;
    public const float JabCooldown = 0.45f;
    public const float QuickKickCooldown = 0.6f;
    public const float TakeHitCooldown = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
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
        Speed = 2;
        color = "Red";
        //Projectile = (GameObject)Resources.Load("Prefab/BlueHadouken", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
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
        if (horz > 0) anim.SetFloat("Velocity", 1);
        if (horz < 0) anim.SetFloat("Velocity", -1);
        
        CharSpeed = new Vector3(0f, 0f, horz);
        if (!Attacking)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                RoundHouse();
                StartCoroutine(waitforattack(KickCooldown));
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                Jab();
                StartCoroutine(waitforattack(JabCooldown));
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                QuickKick();
                StartCoroutine(waitforattack(QuickKickCooldown));
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
                StartCoroutine(waitforattack(JumpCooldown));
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                Punch();
                StartCoroutine(waitforattack(PunchCooldown));
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                Taunt();
                StartCoroutine(waitforattack(TauntCooldown));
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                Hadouken();
                StartCoroutine(waitforattack(HadoukenCooldown));
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Block();
                //StartCoroutine(waitforattack());
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
            StartCoroutine(waitforattack(TakeHitCooldown));
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            DamagePlayer(10);
        }


    }

    protected override IEnumerator waitforHadouken(float f)
    {
        yield return new WaitForSeconds(f);
        Hadouken h = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.2f, gameObject.transform.position.z), Quaternion.identity).GetComponent<Hadouken>();
        if(isFacingRight){
            h.Initialize(Vector3.forward, this);
        }else{
            h.Initialize(-Vector3.forward, this);
        }
    }
}





