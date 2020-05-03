﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{

    public float maxGameTime;
    private float gameTime = 60f;
    public Text timer;
    bool matchStart = false;
    public GameObject PauseItems;
    private int count = 0;
    public GameObject startButtons;
    public GameObject GameTypeButtons;
    private string GameType;
    public GameObject StageSelect;
    private AudioSource Music;
    public AudioSource Button;
    public GameObject Options;
    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        //PauseItems.SetActive(false);
    }
    private void Start()
    {
        Time.timeScale = 1;
        timer = GameObject.Find("Timer").GetComponent<Text>();
        Music = GetComponent<AudioSource>();
        Button = transform.GetChild(0).GetComponent<AudioSource>();
        //print(timer.name);
    }
    // Update is called once per frame
    void Update()
    {
        if (matchStart)
        {
            gameTime -= Time.deltaTime;
            string time = string.Format("{0:N0}", gameTime);
            timer.text = time;

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (Time.timeScale == 1)
                {
                    Pause();
                }
                else if (Time.timeScale == 0)
                {
                    UnPause();
                }
            }
        }

        
    }
    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (SceneManager.GetActiveScene().name == "PreLoading")
        {
            SceneManager.LoadScene("StartMenu");
            startButtons.SetActive(true);
        }
        if(next.name == "StartMenu"&&startButtons!=null) startButtons.SetActive(true);
        timer = GameObject.Find("Timer").GetComponent<Text>();
        PauseItems = GameObject.Find("PauseItems");
        //if(PauseItems.activeSelf &&PauseItems!=null) PauseItems.SetActive(false); 
        gameTime = maxGameTime;
        if(count>0)
        {
            //print(count);
            var temp = GameObject.FindGameObjectsWithTag("Temp");
            foreach(GameObject item in temp)
            {
                Destroy(item);
            }
            var perm = GameObject.FindGameObjectWithTag("Perm");
            PauseItems = perm.transform.GetChild(0).gameObject;
        }
        count++;
        timer = GameObject.Find("Timer").GetComponent<Text>();
    }
    #region Buttons
    public void GameStart()
    {
        Button.Play();
        startButtons.SetActive(false);
        GameTypeButtons.SetActive(true);
    }
    public void LoadLevel(string pickedLevel)
    {
        Button.Play();
        SceneManager.LoadScene(pickedLevel);
        matchStart = true;
    }
    public void QuitGame()
    {
        Button.Play();
        startButtons.SetActive(false);
        print("Quit the game");
        Application.Quit();
    }
    public void Pause()
    {
        Button.Play();
        Music.volume /= 2f;
        Time.timeScale = 0;
        PauseItems.SetActive(true);
    }
    public void UnPause()
    {
        Button.Play();
        Music.volume *= 2f;
        Time.timeScale = 1;
        PauseItems.SetActive(false);
    }
    public void ReturnToMain()
    {
        Button.Play();
        UnPause();
        SceneManager.LoadScene("StartMenu");
        matchStart = false;
        
    }
    public void OptionsMenu()
    {
        Button.Play();
        Music.volume *= 2f;
        PauseItems.SetActive(false);
        Options.SetActive(true);
        //plan to have mute button and other functionality
    }
    public void ReturnFromOptions()
    {
        Button.Play();
        Music.volume /= 2f;
        Options.SetActive(false);
        PauseItems.SetActive(true);
    }
    public void LowerVolume()
    {
        if (Music.volume > 0)
        {
            Music.volume-= 0.1f;
        }
    }
    public void RaiseVolume()
    {
        if (Music.volume < 1f)
        {
            Music.volume += 0.1f;
        }
    }
    public void onePlayer()
    {
        Button.Play();
        GameType = "onePlayer";
        showStages();
        //when we have these different modes, will change which prefab fighters to load into each level
    }
    public void twoPlayer()
    {
        Button.Play();
        GameType = "twoPlayer";
        showStages();
    }
    public void Training()
    {
        Button.Play();
        GameType = "Training";
        showStages();
    }
    public void Cockfight()
    {
        Button.Play();
        GameType = "Cockfight";
        showStages();
    }
    public void showStages()
    {
        Button.Play();
        GameTypeButtons.SetActive(false);
        StageSelect.SetActive(true);
    }
    public void Stage1()
    {
        Button.Play();
        print("this doesn't do anything yet");
    }
    public void Dojolevel()
    {
        Button.Play();
        StageSelect.SetActive(false);
        LoadLevel("SampleScene");
    }
    public void Stage3()
    {
        Button.Play();
        print("this doesn't do anything yet");
    }
    public void Stage4()
    {
        Button.Play();
        print("this doesn't do anything yet");
    }
    #endregion
}
