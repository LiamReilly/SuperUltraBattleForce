using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hadouken : MonoBehaviour
{
    private float life = 5f;
    public Vector3 direction;
    public PlayerBase owner;
    private Rigidbody rb;
    private ParticleSystem explosion;
    private MeshRenderer mesh;

    public void Initialize(Vector3 direction, PlayerBase owner){
        this.direction = direction;
        this.owner = owner;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.detectCollisions = false;
        explosion = GetComponentInChildren<ParticleSystem>();
        mesh = GetComponent<MeshRenderer>();
        explosion.Pause();
    }

    void Update()
    {
        gameObject.transform.Translate(direction * Time.deltaTime * 5);
        life -= Time.deltaTime;
        if (life < 0) Destroy(gameObject);
        if (life < 4.85f)
        {
            rb.detectCollisions = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Player")){
            PlayerBase playerRef = collision.gameObject.GetComponent<PlayerBase>();
            if(playerRef != owner){
                playerRef.DamagePlayer(10f*owner.baseAttack);
            }
        }
        mesh.enabled = false;
        explosion.Play();
        StartCoroutine(DestroyHadouken());
    }
    IEnumerator DestroyHadouken()
    {
        rb.detectCollisions = false;
        yield return new WaitForSeconds(0.3f);
        explosion.Pause();
        Destroy(gameObject);
    }
}
