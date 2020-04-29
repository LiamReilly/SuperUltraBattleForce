using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hadouken : MonoBehaviour
{
    private float life = 5f;
    public Vector3 direction;
    public PlayerBase owner;

    public void Initialize(Vector3 direction, PlayerBase owner){
        this.direction = direction;
        this.owner = owner;
    }

    void Update()
    {
        gameObject.transform.Translate(direction * Time.deltaTime * 5);
        life -= Time.deltaTime;
        if (life < 0) Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject is PlayerBase){
            PlayerBase playerRef = (PlayerBase)collision.gameObject.GetComponent<PlayerBase>();
            if(playerRef != owner){
                playerRef.DamagePlayer(10);
            }
        }
        Destroy(gameObject);
    }
}
