using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerBase playerHealth;
    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       
        healthBar = GetComponent<Slider>();
        
    }


    public void SetHealth(float hp)
    {
        healthBar.value = hp;
    }
    public void SetUp(PlayerBase play)
    {
        //player = play;
        playerHealth = play;
        healthBar.maxValue = playerHealth.maxHealth;
        //healthBar.maxValue = 100f;
        healthBar.value = healthBar.maxValue;
    }
}