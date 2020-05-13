using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundUpdater : MonoBehaviour
{
    public Image Round1;
    public Image Round2;
    public int RoundCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        //Round1 = transform.GetChild(0).GetComponent<Image>();
        //Round2 = transform.GetChild(1).GetComponent<Image>();
    }

    public bool IncreaseRoundCount()
    {
        if(RoundCount == 1)
        {
            Round1.enabled = true;
            RoundCount++;
            return false;
        }
        if (RoundCount == 2)
        {
            Round2.enabled = true;
            RoundCount++;
            return true;
        }
        else return true;
    }
    public void ResetRounds()
    {
        RoundCount = 1;
        Round1.enabled = false;
        Round2.enabled = false;
    }
}
