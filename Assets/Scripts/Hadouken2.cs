using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hadouken2 : MonoBehaviour
{
    private float life = 5f;
    void Update()
    {
        gameObject.transform.Translate(Vector3.back * Time.deltaTime * 5);
        life -= Time.deltaTime;
        if (life < 0) Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
