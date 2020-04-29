using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedController : PlayerBase
{
    private Vector3 CharSpeed;
    Animator anim;
    private bool Attacking;
    private bool move;
    public healthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currHealth = 100;
        maxHealth = 100;
        characterName = "Red Girl";
        baseAttack = 1;
        Speed = 2;

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
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
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
        if (Input.GetKeyDown(KeyCode.J))
        {
            RoundHouse();
            StartCoroutine(waitforattack(1.8f));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
            StartCoroutine(waitforattack(1f));
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Punch();
            StartCoroutine(waitforattack(2f));
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Taunt();
            StartCoroutine(waitforattack(4.45f));
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Hadouken();
            StartCoroutine(waitforattack(3.6f));
        }

        CharSpeed = new Vector3(0f, 0f, horz);
        if (!Attacking)
        {
            moveCharacter(CharSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            DamagePlayer(10);
        }


    }
    public void moveCharacter(Vector3 amount)
    {
        gameObject.transform.Translate(amount);
    }
    public void RoundHouse()
    {
        Attacking = true;
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
        StartCoroutine(waitforHadouken(1.8f));
    }
    IEnumerator waitforattack(float f)
    {
        yield return new WaitForSeconds(f);
        Attacking = false;

    }
    IEnumerator waitforHadouken(float f)
    {
        yield return new WaitForSeconds(f);
        Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.2f, gameObject.transform.position.z+0.2f), Quaternion.identity);
    }
    public void DamagePlayer(int damage)
    {
        currHealth -= damage;

        healthBar.SetHealth(currHealth);
    }
}





