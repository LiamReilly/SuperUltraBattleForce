using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float gameTime;
    public Text timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        string time = string.Format("{0:N0}", gameTime);
        timer.text = time;
    }
}
