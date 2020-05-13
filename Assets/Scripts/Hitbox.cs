using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour { 

    //
    public GameObject parentChar;

    private float damage, knockback;
    public int scale = 1;

    public void Start()
    {
        transform.localScale *= scale;
        damage = 0f;
        knockback = 0f;
    }
    
    public void set(float d)
    {
        set(d, d);
    }

    public void set(float d, float k)
    {
        // if(d > 0)Debug.Log("Set damage to " + d);

        damage = d;
        knockback = k;
    }

    public void Reset()
    {
        // Debug.Log("reset");
        damage = 0f;
        knockback = 0f;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        if (other.gameObject.Equals(parentChar))
            return;

        if (damage == 0)
            return;

        PlayerBase otherChar = other.gameObject.GetComponent<PlayerBase>();

        if (damage > 0)
            Debug.Log("Should have done damage?");
        else
            Debug.Log("Collided with 0 damage?");

        otherChar.DamagePlayer(damage);
        otherChar.KnockbackPlayer(knockback);
        Reset();
    }
}
