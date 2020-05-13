using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    public Slider specialBar;
    //public PlayerBase playerHealth;
    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

        specialBar = GetComponent<Slider>();
        specialBar.value = 0f;

    }
    private void Update()
    {
        if (specialBar.value < 100f)
        {
            specialBar.value += Time.deltaTime*50;
        }
    }
    public float GetLevel()
    {
        return specialBar.value;
    }

    public void AddLevel(float power)
    {
        specialBar.value += power;
    }

    public bool isFull(){
        return specialBar.value >= 100f;
    }
    
}
