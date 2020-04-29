using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar2 : MonoBehaviour
{
    public Slider healBar;
    public BlueController playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player2").GetComponent<BlueController>();
        healBar = GetComponent<Slider>();
        healBar.maxValue = playerHealth.maxHealth;
        healBar.value = playerHealth.maxHealth;
    }

    public void SetHealth(int hp)
    {
        healBar.value = hp;
    }
}
