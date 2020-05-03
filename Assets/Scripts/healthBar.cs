using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerBase playerHealth;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       
        healthBar = GetComponent<Slider>();
        
    }


    public void SetHealth(float hp)
    {
        healthBar.value = hp;
    }
    public void SetUp(GameObject play)
    {
        //player = play;
        //playerHealth = player.GetComponent<PlayerBase>();
        //healthBar.maxValue = playerHealth.maxHealth;
        //healthBar.value = 100f;
    }
}
