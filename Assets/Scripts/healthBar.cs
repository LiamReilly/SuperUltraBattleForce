using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider healBar;
    public RedController playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player1").GetComponent<RedController>();
        healBar = GetComponent<Slider>();
        healBar.maxValue = playerHealth.maxHealth;
        healBar.value = playerHealth.maxHealth;
    }

    public void SetHealth(int hp)
    {
        healBar.value = hp;
    }
}
