using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 CharSpeed;
    Animator anim;
    private bool Attacking;
    public float Speed;
    private bool move;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float horz = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
        if (horz != 0)
        {
                move = true;
        }
        else
        {
                move = false;
        }
        anim.SetBool("Move", move);
        if (horz>0) anim.SetFloat("Velocity", 1);
        if (horz < 0) anim.SetFloat("Velocity", -1);
        if (Input.GetKeyDown(KeyCode.X))
        {
            RoundHouse();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Punch();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Taunt();
        }

        CharSpeed = new Vector3(0f, 0f, horz);
        if (!Attacking)
        {
            gameObject.transform.Translate(CharSpeed);
        }
        

    }
    public void RoundHouse()
    {
        //Attacking = true;
        anim.SetTrigger("RoundHouse");
    }
    public void Jump()
    {
        anim.SetTrigger("Jump");
    }
    public void Punch()
    {
        //Attacking = true;
        anim.SetTrigger("Punch");
    }
    public void Taunt()
    {
        //Attacking = true;
        anim.SetTrigger("Taunt");
    }

}





