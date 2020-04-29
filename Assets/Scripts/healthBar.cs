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
        playerHealth = player.GetComponent<PlayerBase>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
    }

    public void SetHealth(float hp)
    {
        healthBar.value = hp;
    }
}
