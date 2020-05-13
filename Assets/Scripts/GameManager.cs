using System.Collections;
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
    private enum GameType {
        ONEPLAYER, TWOPLAYER, TRAINING, CPUMODE
    }

    private GameType gameType;

    public GameObject playerAIPrefab;

    public GameObject StageSelect;
    public GameObject CharacterSelect;
    private AudioSource Music;
    public AudioSource Button;
    public GameObject Options;
    public GameObject Players;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    public GameObject Health;
    public HealthBar bar1;
    public HealthBar bar2;
    private GameObject MusicChanger;
    public AudioClip[] Songs;
    private int CurrentSong;
    public GameObject[] Specialbars;
    
    // Start is called before the first frame update
    void Awake()
    {
        bar1 = Health.transform.GetChild(0).gameObject.GetComponent<HealthBar>();
        bar2 = Health.transform.GetChild(1).gameObject.GetComponent<HealthBar>();
        SceneManager.activeSceneChanged += ChangedActiveScene;
        //PauseItems.SetActive(false);
    }
    private void Start()
    {
        Time.timeScale = 1;
        timer = GameObject.Find("Timer").GetComponent<Text>();
        Music = GetComponent<AudioSource>();
        Button = transform.GetChild(0).GetComponent<AudioSource>();
        MusicChanger = StageSelect.transform.GetChild(4).gameObject;
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
        if (next.name == "StartMenu" && startButtons != null)
        {
            startButtons.SetActive(true);
            Health.SetActive(false);
            Specialbars[0].SetActive(false);
        }
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
        if (SceneManager.GetActiveScene().name != "StartMenu" && SceneManager.GetActiveScene().name != "PreLoading")
        {
            Players = GameObject.Find("Players");
            if (CharacterChip.CharacterSelected.Equals("Blue") && Player2Chip.CharacterSelected.Equals("Red"))
            {
                var p1 = Instantiate(Player4, Players.transform.GetChild(0).position, Quaternion.identity);
                var p2 = Instantiate(Player3, Players.transform.GetChild(1).position, Quaternion.Euler(new Vector3(0f, 180f, 0)));
                /*p1.GetComponent<BlueController>().enabled = false;
                p1.AddComponent<RedController>();
                p2.GetComponent<RedController>().enabled = false;
                p2.AddComponent<BlueController>();*/
                FinishLevelSetup(p1, p2);
            }
            if (CharacterChip.CharacterSelected.Equals("Red") && Player2Chip.CharacterSelected.Equals("Red"))
            {
                var p1 = Instantiate(Player1, Players.transform.GetChild(0).position, Quaternion.identity);
                var p2 = Instantiate(Player3, Players.transform.GetChild(1).position, Quaternion.Euler(new Vector3(0f, 180f, 0)));
               /* p2.GetComponent<RedController>().enabled = false;
                p2.AddComponent<BlueController>();*/
                FinishLevelSetup(p1, p2);
            }
            if(CharacterChip.CharacterSelected.Equals("Red") && Player2Chip.CharacterSelected.Equals("Blue"))
            {
                var p1 = Instantiate(Player1, Players.transform.GetChild(0).position, Quaternion.identity);
                var p2 = Instantiate(Player2, Players.transform.GetChild(1).position, Quaternion.Euler(new Vector3(0f, 180f, 0)));
                FinishLevelSetup(p1, p2);
            }
            if (CharacterChip.CharacterSelected.Equals("Blue") && Player2Chip.CharacterSelected.Equals("Blue"))
            {
                var p1 = Instantiate(Player4, Players.transform.GetChild(0).position, Quaternion.identity);
                var p2 = Instantiate(Player2, Players.transform.GetChild(1).position, Quaternion.Euler(new Vector3(0f, 180f, 0)));
               /* p1.GetComponent<BlueController>().enabled = false;
                p1.AddComponent<RedController>();*/
                FinishLevelSetup(p1, p2);
            }
            

        }
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
        gameType = GameType.ONEPLAYER;
        showCharacters();
        //when we have these different modes, will change which prefab fighters to load into each level
    }
    public void twoPlayer()
    {
        Button.Play();
        gameType = GameType.TWOPLAYER;
        showCharacters();
    }
    public void Training()
    {
        Button.Play();
        gameType = GameType.TRAINING;
        showCharacters();
    }
    public void CPUMode()
    {
        Button.Play();
        gameType = GameType.CPUMODE;
        showCharacters();
    }
    void showCharacters()
    {
        Button.Play();
        GameTypeButtons.SetActive(false);
        CharacterSelect.SetActive(true);
        StartCoroutine(WaitForSelection(1f));
        //showStages();
    }
    public void showStages()
    {
        Button.Play();
        StageSelect.SetActive(true);
        MusicChanger.GetComponentInChildren<Text>().text = Songs[CurrentSong].name;
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
        LoadLevel("Dojo");
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
    public void MusicLeft()
    {
            CurrentSong--;
            if (CurrentSong < 0)
            {
                CurrentSong = Songs.Length-1;
            }
            Music.clip = Songs[CurrentSong];
            Music.Play();
            MusicChanger.GetComponentInChildren<Text>().text = Songs[CurrentSong].name;
    }
    public void MusicRight()
    {
            CurrentSong++;
            if (CurrentSong >= Songs.Length)
            {
                CurrentSong = 0;
            }
            Music.clip = Songs[CurrentSong];
            Music.Play();
            MusicChanger.GetComponentInChildren<Text>().text = Songs[CurrentSong].name;
    }
    #endregion
    IEnumerator WaitForSelection(float f)
    {
        if (CharacterChip.Selected && Player2Chip.Selected)
        {
            
            CharacterSelect.SetActive(false);
            showStages();
        }
        else
        {
            yield return new WaitForSeconds(f);
            yield return WaitForSelection(1.5f);
        }
        
    }
    void FinishLevelSetup(GameObject p1, GameObject p2)
    {
        var placeholder1 = Players.transform.GetChild(0);
        Players.transform.GetChild(0).SetParent(null);
        placeholder1.SetParent(p1.transform);
        var placeholder2 = Players.transform.GetChild(0);
        Players.transform.GetChild(0).SetParent(null);
        placeholder2.SetParent(p2.transform);
        Destroy(Players);
        Health.SetActive(true);
        Specialbars[0].SetActive(true);
        //var playerbases  = p1.GetComponents<PlayerBase>();
        p2.tag = "Player2";
        //playerbases[1].healthBar = bar1;
        p1.GetComponent<PlayerBase>().healthBar = bar1;
        p2.GetComponent<PlayerBase>().healthBar = bar2;
        p1.GetComponent<PlayerBase>().specialBar = Specialbars[1].GetComponent<SpecialBar>();
        p2.GetComponent<PlayerBase>().specialBar = Specialbars[2].GetComponent<SpecialBar>();
        //bar1.SetUp(playerbases[1]);
        //bar1.SetUp(p1.GetComponent<PlayerBase>());
        //bar2.SetUp(p2.GetComponent<PlayerBase>());
        //print("Hello Sir");

        if (gameType == GameType.CPUMODE){
            AIController ai1 = p1.AddComponent<AIController>();
            AIController ai2 = p2.AddComponent<AIController>();

            ai1.player = p1.GetComponent<PlayerBase>();
            ai1.player.isAI = true;

            ai2.player = p2.GetComponent<PlayerBase>();
            ai2.player.isAI = true;

            
        } else if (gameType == GameType.ONEPLAYER){
            AIController ai2 = p2.AddComponent<AIController>();

            ai2.player = p2.GetComponent<PlayerBase>();
            ai2.player.isAI = true;
        }
        

    }
}
