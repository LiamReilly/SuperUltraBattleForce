using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    public Slider specialBar;
    public bool frozen;

    private float originalG;
    private int direction;
    //public PlayerBase playerHealth;
    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

        specialBar = GetComponent<Slider>();
        specialBar.value = 0f;

        originalG = specialBar.image.color.g;
        direction = 1;
    }
    private void Update()
    {
        if (specialBar.value < 100f && !frozen)
        {
            specialBar.value += Time.deltaTime*5;
            specialBar.image.color = new Color(specialBar.image.color.r, originalG, specialBar.image.color.b);
        }

        if(specialBar.value >= 100f && !frozen)
        {
            Color c = specialBar.image.color;

            if(c.g <= originalG)
            {
                direction = 1;
            }
            if(c.g >= originalG + 0.2f)
            {
                direction = -1;
            }

            Debug.Log(c.g);

            specialBar.image.color = new Color(c.r, c.g + direction*0.01f, c.b);
        }
    }
    public float GetLevel()
    {
        return specialBar.value;
    }

    public void AddLevel(float power)
    {
        if(power < 0){
            Debug.Log("That's illegal, cannot add negative level to special meter");
            return;
        }
        specialBar.value += power;
    }

    public bool isFull(){
        return specialBar.value >= 100f;
    }

    public void ResetLevel()
    {
        specialBar.value = 0f;
    }
}
