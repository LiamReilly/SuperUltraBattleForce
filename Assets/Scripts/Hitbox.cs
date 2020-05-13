using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour { 

    //
    public GameObject parentChar;

    private float damage;
    public int scale = 2;

    public void Start()
    {
        damage = 10f;
    }

    public void setDamage(float f)
    {
        damage = f;
    }

    public void Reset()
    {
        damage = 0f;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        if (other.gameObject.Equals(parentChar))
            return;

        PlayerBase otherChar = other.gameObject.GetComponent<PlayerBase>();

        otherChar.DamagePlayer(damage);
        Reset();

        Debug.Log("Collision?");
    }
}
