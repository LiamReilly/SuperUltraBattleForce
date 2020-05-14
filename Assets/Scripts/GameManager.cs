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

    public static bool VisibleHitboxes;

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
    public GameObject[] PickedPlayers;
    public GameObject Health;
    public HealthBar bar1;
    public HealthBar bar2;
    private GameObject MusicChanger;
    public AudioClip[] Songs;
    private int CurrentSong;
    public GameObject[] Specialbars;
    public GameObject RoundsParent;
    public GameObject Countdown;
    public RoundUpdater[] Rounds;
    public GameObject EndOfMatchSelection;
    
    // Start is called before the first frame update
    void Awake()
    {
        bar1 = Health.transform.GetChild(0).gameObject.GetComponent<HealthBar>();
        bar2 = Health.transform.GetChild(1).gameObject.GetComponent<HealthBar>();
        SceneManager.activeSceneChanged += ChangedActiveScene;
        //PauseItems.SetActive(false);

        VisibleHitboxes = false;
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

            if (Input.GetKeyDown(KeyCode.H))
            {
                VisibleHitboxes = !VisibleHitboxes;
            }

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
            if (gameTime <= 3f)
            {
                Countdown.SetActive(true);
                Countdown.GetComponent<Text>().text = time;
            }
            if (gameTime < 0f)
            {
                gameTime = 0f;
                //Countdown.GetComponent<Text>().text = "Match Over";
                matchStart = false;
                EndRound();
            }
            if(bar1.GetComponent<HealthBar>().GetValue() <=0 || bar2.GetComponent<HealthBar>().GetValue() <= 0)
            {
                gameTime = 0f;

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
            RoundsParent.SetActive(false);
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
                PickedPlayers[0] = p1;
                PickedPlayers[1] = p2;
                FinishLevelSetup(p1, p2);
            }
            if (CharacterChip.CharacterSelected.Equals("Red") && Player2Chip.CharacterSelected.Equals("Red"))
            {
                var p1 = Instantiate(Player1, Players.transform.GetChild(0).position, Quaternion.identity);
                var p2 = Instantiate(Player3, Players.transform.GetChild(1).position, Quaternion.Euler(new Vector3(0f, 180f, 0)));
                /* p2.GetComponent<RedController>().enabled = false;
                 p2.AddComponent<BlueController>();*/
                PickedPlayers[0] = p1;
                PickedPlayers[1] = p2;
                FinishLevelSetup(p1, p2);
            }
            if(CharacterChip.CharacterSelected.Equals("Red") && Player2Chip.CharacterSelected.Equals("Blue"))
            {
                var p1 = Instantiate(Player1, Players.transform.GetChild(0).position, Quaternion.identity);
                var p2 = Instantiate(Player2, Players.transform.GetChild(1).position, Quaternion.Euler(new Vector3(0f, 180f, 0)));
                PickedPlayers[0] = p1;
                PickedPlayers[1] = p2;
                FinishLevelSetup(p1, p2);
            }
            if (CharacterChip.CharacterSelected.Equals("Blue") && Player2Chip.CharacterSelected.Equals("Blue"))
            {
                var p1 = Instantiate(Player4, Players.transform.GetChild(0).position, Quaternion.identity);
                var p2 = Instantiate(Player2, Players.transform.GetChild(1).position, Quaternion.Euler(new Vector3(0f, 180f, 0)));
                /* p1.GetComponent<BlueController>().enabled = false;
                 p1.AddComponent<RedController>();*/
                PickedPlayers[0] = p1;
                PickedPlayers[1] = p2;
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
        if (Music.volume > 0.5f) Music.volume = 0.5f;
        Time.timeScale = 1;
        PauseItems.SetActive(false);
    }
    public void ReturnToMain()
    {
        Button.Play();
        UnPause();
        SceneManager.LoadScene("StartMenu");
        Specialbars[1].GetComponent<SpecialBar>().ResetLevel();
        Specialbars[2].GetComponent<SpecialBar>().ResetLevel();
        Specialbars[1].GetComponent<SpecialBar>().frozen = false;
        Specialbars[2].GetComponent<SpecialBar>().frozen = false;
        if (EndOfMatchSelection.activeSelf) EndOfMatchSelection.SetActive(false);
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
        CharacterChip.Selected = false;
        Player2Chip.Selected = false;
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
        StageSelect.SetActive(false);
        LoadLevel("Airport");
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
        StageSelect.SetActive(false);
        LoadLevel("Space");
    }
    public void Stage4()
    {
        Button.Play();
        StageSelect.SetActive(false);
        LoadLevel("whitehouse");
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
    /*public void Rematch()
    {
        Button.Play();
        EndOfMatchSelection.SetActive(false);
        StopAllCoroutines();
        Countdown.SetActive(false);
        LoadLevel(SceneManager.GetActiveScene().name);
        StartCoroutine(ResetPlayers(3f));

    }*/
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
    IEnumerator DisableAftersecs(float f, GameObject Disable)
    {
        yield return new WaitForSeconds(f);
        Disable.SetActive(false);
    }
    IEnumerator ResetPlayers(float f)
    {
        yield return new WaitForSeconds(f);
        var currentRound = Rounds[0].RoundCount + Rounds[1].RoundCount - 1;
        print(currentRound);
        Countdown.GetComponent<Text>().text = "Round" + currentRound + "\nstart!";
        Countdown.SetActive(true);
        yield return new WaitForSeconds(3f);
        Countdown.SetActive(false);
        Specialbars[1].GetComponent<SpecialBar>().frozen = false;
        Specialbars[2].GetComponent<SpecialBar>().frozen = false;
        PickedPlayers[0].GetComponent<PlayerBase>().enabled = true;
        PickedPlayers[1].GetComponent<PlayerBase>().enabled = true;
        string time = string.Format("{0:N0}", maxGameTime);
        gameTime = maxGameTime;
        timer.text = time;
        matchStart = true;
        

    }
    IEnumerator OfferEndofMatchOptions()
    {
        yield return new WaitForSeconds(2f);
        Countdown.SetActive(false);
        EndOfMatchSelection.SetActive(true);
    }
    private void EndRound()
    {
        if(bar1.GetComponent<HealthBar>().GetValue() > bar2.GetComponent<HealthBar>().GetValue())
        {
            var result = Rounds[0].IncreaseRoundCount();
            print("p1 won round");
            PickedPlayers[0].GetComponent<PlayerBase>().anim.SetBool("Move", false);
            PickedPlayers[1].GetComponent<PlayerBase>().anim.SetBool("Move", false);
            PickedPlayers[0].GetComponent<PlayerBase>().anim.SetTrigger("Victory");
            PickedPlayers[1].GetComponent<PlayerBase>().anim.SetTrigger("Defeat");
            PickedPlayers[0].GetComponent<PlayerBase>().enabled = false;
            PickedPlayers[1].GetComponent<PlayerBase>().enabled = false;
            Specialbars[1].GetComponent<SpecialBar>().frozen = true;
            Specialbars[2].GetComponent<SpecialBar>().frozen = true;
            
            if (result)
            {
                EndMatch(PickedPlayers[0].GetComponent<PlayerBase>().name);
            }
            else
            {
                RoundReset();
            }
        }
        else if (bar1.GetComponent<HealthBar>().GetValue() < bar2.GetComponent<HealthBar>().GetValue())
        {
            var result = Rounds[1].IncreaseRoundCount();
            print("p2 won round");
            PickedPlayers[0].GetComponent<PlayerBase>().anim.SetBool("Move", false);
            PickedPlayers[1].GetComponent<PlayerBase>().anim.SetBool("Move", false);
            PickedPlayers[0].GetComponent<PlayerBase>().anim.SetTrigger("Defeat");
            PickedPlayers[1].GetComponent<PlayerBase>().anim.SetTrigger("Victory");
            PickedPlayers[0].GetComponent<PlayerBase>().enabled = false;
            PickedPlayers[1].GetComponent<PlayerBase>().enabled = false;
            Specialbars[1].GetComponent<SpecialBar>().frozen = true;
            Specialbars[2].GetComponent<SpecialBar>().frozen = true;
            
            if (result)
            {
                EndMatch(PickedPlayers[1].GetComponent<PlayerBase>().name);
            }
            else
            {
                RoundReset();
            }
        }
        else if (PickedPlayers[0].GetComponent<PlayerBase>().currHealth == PickedPlayers[1].GetComponent<PlayerBase>().currHealth)
        {
            PickedPlayers[0].GetComponent<PlayerBase>().anim.SetBool("Move", false);
            PickedPlayers[1].GetComponent<PlayerBase>().anim.SetBool("Move", false);
            PickedPlayers[0].GetComponent<PlayerBase>().anim.SetTrigger("Defeat");
            PickedPlayers[1].GetComponent<PlayerBase>().anim.SetTrigger("Defeat");
            PickedPlayers[0].GetComponent<PlayerBase>().enabled = false;
            PickedPlayers[1].GetComponent<PlayerBase>().enabled = false;
            Specialbars[1].GetComponent<SpecialBar>().frozen = true;
            Specialbars[2].GetComponent<SpecialBar>().frozen = true;
            var result = Rounds[0].IncreaseRoundCount();
            var result2 = Rounds[1].IncreaseRoundCount();
            if (result&&result2)
            {
                EndMatch("It's a tie");
            }
            if (result&&!result2)
            {
                EndMatch(PickedPlayers[0].GetComponent<PlayerBase>().name);
            }
            if (!result&&result2)
            {
                EndMatch(PickedPlayers[1].GetComponent<PlayerBase>().name);
            }
            if (!result && !result2)
            {
                RoundReset();
            }
        }
    }
    private void RoundReset()
    {
        Countdown.GetComponent<Text>().text = "Round Over";
        StartCoroutine(DisableAftersecs(3f, Countdown));
        PickedPlayers[0].GetComponent<PlayerBase>().currHealth = PickedPlayers[0].GetComponent<PlayerBase>().maxHealth;
        PickedPlayers[1].GetComponent<PlayerBase>().currHealth = PickedPlayers[1].GetComponent<PlayerBase>().maxHealth;
        bar1.SetUp(PickedPlayers[0].GetComponent<PlayerBase>());
        bar2.SetUp(PickedPlayers[1].GetComponent<PlayerBase>());
        StartCoroutine(ResetPlayers(3f));
    }
    private void EndMatch(string winner)
    {
        if (winner.StartsWith("R"))
        {
            string str = winner.Substring(0, 3);
            Countdown.GetComponent<Text>().text = str + "\nWins!";
        }
        if (winner.StartsWith("I"))
        {
            string str = winner;
            Countdown.GetComponent<Text>().text = winner;
        }
        if (winner.StartsWith("B"))
        { 
            string str = winner.Substring(0, 4);
            Countdown.GetComponent<Text>().text = str + "\nWins!";
        }
        StartCoroutine(OfferEndofMatchOptions());
        
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
        //Specialbars[1].GetComponent<SpecialBar>().ResetLevel();
        //Specialbars[2].GetComponent<SpecialBar>().ResetLevel();
        RoundsParent.SetActive(true);
        Rounds[0].GetComponent<RoundUpdater>().ResetRounds();
        Rounds[1].GetComponent<RoundUpdater>().ResetRounds();
        //var playerbases  = p1.GetComponents<PlayerBase>();

        PlayerBase p1Script = p1.GetComponent<PlayerBase>();
        PlayerBase p2Script = p2.GetComponent<PlayerBase>();


        //playerbases[1].healthBar = bar1;
        p1Script.healthBar = bar1;
        p2Script.healthBar = bar2;
        p1Script.specialBar = Specialbars[1].GetComponent<SpecialBar>();
        p2Script.specialBar = Specialbars[2].GetComponent<SpecialBar>();
        //bar1.SetUp(playerbases[1]);
        bar1.SetUp(p1Script);
        bar2.SetUp(p2Script);
        //print("Hello Sir");

        if (gameType == GameType.CPUMODE){
            AIController ai1 = p1.AddComponent<AIController>();
            AIController ai2 = p2.AddComponent<AIController>();

            ai1.player = p1Script;
            ai1.opponent = p2Script;
            ai1.player.isAI = true;

            ai2.player = p2Script;
            ai2.opponent = p1Script;
            ai2.player.isAI = true;


        } else if (gameType == GameType.ONEPLAYER){
            AIController ai2 = p2.AddComponent<AIController>();

            ai2.player = p2Script;
            ai2.opponent = p1Script;
            ai2.player.isAI = true;
        } else if(gameType == GameType.TRAINING){
            p1Script.isTraining = true;
            p2Script.isTraining = true;
        }
        ResetPlayers(1f);


    }
}
