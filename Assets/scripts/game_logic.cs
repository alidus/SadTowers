using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class game_logic : MonoBehaviour {
    // pause_menu init
    public Image pause_menu_panel;
    public Image resume_game_button;
    public Image control_button;
    public Image option_button;
    public Image exit_game_button;
    public Text resume_game_text;
    public Text control_text;
    public Text option_text;
    public Text exit_game_text;
    public Image control_ingame;
    private bool control_showed = false;

    // game variables and info panels init
        // money
    public int money;
    [SerializeField]
    private Text money_text;
        // lifes
    private int lifes;
    [SerializeField]
    private Text lifes_text;
    private bool game_over = false;
        // timer
    public float time_passed;
    [SerializeField]
    private Text timer_panel_text;
    private AudioClip[] playlist;
    private bool game_paused = false;
        // music
    private AudioClip music_track;
    private AudioSource audio_source;

    public int selected_turret_index = -1;
    [SerializeField]
    private data_store_logic Data_Store;
        // game start cooldown
    private float game_start_cooldown;
    [SerializeField]
    private GameObject start_cooldown_panel;
    [SerializeField]
    private GameObject start_cooldown_text;
    private Text start_cooldown_text_text;
        // playing song panel
    private Text playing_song_name;
    Image playing_song_panel_image;
    Text playing_song_panel_lable_text;
        // turret panel
    List<Image> list_of_turret_panels_images = new List<Image>();

    void Awake ()
        {
        DontDestroyOnLoad(gameObject);
        }
    // Use this for initialization
    void Start() {
        start_cooldown_panel.GetComponent<Image>().CrossFadeAlpha(0, 0, false);
        start_cooldown_text.GetComponent<Text>().CrossFadeAlpha(0, 0, false);
        start_cooldown_text_text = start_cooldown_text.GetComponent<Text>();
        SetupVariables();
        money_text.text = "Money: " + money;
        lifes_text.text = "Lifes: " + lifes;
        ChangeTurretsPanelInfo();
        // init all turret panels of UI
        list_of_turret_panels_images.Add(GameObject.Find("turret_1_panel").GetComponent<Image>());
        list_of_turret_panels_images.Add(GameObject.Find("turret_2_panel").GetComponent<Image>());
        list_of_turret_panels_images.Add(GameObject.Find("turret_3_panel").GetComponent<Image>());
        game_start_cooldown = Data_Store.data.game_start_delay;
        HideUnecessaryUiStuff();
        init_current_playing_song_shower();
        PlayMusic();
        Invoke("ShowGameStartCooldown", 1.5f);
        ChangePauseMenuStats(false, 0f, 0f, 0f);
        control_ingame.enabled = true;
        control_ingame.canvasRenderer.SetAlpha(0);
        }
    void ShowGameStartCooldown()
        {
        start_cooldown_panel.GetComponent<Image>().CrossFadeAlpha(1f, 1f, false);
        start_cooldown_text.GetComponent<Text>().CrossFadeAlpha(1f, 1f, false);
        }
    void HideGameStartCooldown()
        {
        start_cooldown_panel.GetComponent<Image>().CrossFadeAlpha(0f, 1f, false);
        start_cooldown_text.GetComponent<Text>().CrossFadeAlpha(0f, 1f, false);
        }
    void HideUnecessaryUiStuff()
        {
        Image lifes_panel = GameObject.Find("PanelLifes").GetComponent<Image>();
        Image time_panel = GameObject.Find("PanelTimer").GetComponent<Image>();
        lifes_panel.CrossFadeAlpha(0, 0, false);
        time_panel.CrossFadeAlpha(0, 0, false);
        lifes_text.CrossFadeAlpha(0, 0, false);
        timer_panel_text.CrossFadeAlpha(0, 0, false);
        }
    void ShowUnecessaryUiStuff(float fade_time=1f)
        {
        Image lifes_panel = GameObject.Find("PanelLifes").GetComponent<Image>();
        Image time_panel = GameObject.Find("PanelTimer").GetComponent<Image>();
        lifes_panel.CrossFadeAlpha(0.8f, fade_time, false);
        time_panel.CrossFadeAlpha(0.8f, fade_time, false);
        lifes_text.CrossFadeAlpha(1f, fade_time, false);
        timer_panel_text.CrossFadeAlpha(1f, fade_time, false);
        }
    void init_current_playing_song_shower()
        {
        playing_song_panel_image = GameObject.Find("PanelPlayingSong").GetComponent<Image>();
        playing_song_panel_lable_text = GameObject.Find("TextPlayingSongHeader").GetComponent<Text>();
        playing_song_name = GameObject.Find("TextPlayingSongTitle").GetComponent<Text>();
        playing_song_panel_image.CrossFadeAlpha(0, 0, false);
        playing_song_panel_lable_text.CrossFadeAlpha(0, 0, false);
        playing_song_name.CrossFadeAlpha(0, 0, false);
        }
    void ShowCurrentPlayingSong(float duration = 3, float delay = 0)
        {
        playing_song_name.text = music_track.name;
        Invoke("show_playing_song", delay);
        Invoke("hide_playing_song", duration+delay);
        }
    void show_playing_song()
        {
        playing_song_panel_image.CrossFadeAlpha(1f, 0.8f, false);
        playing_song_panel_lable_text.CrossFadeAlpha(1f, 0.8f, false);
        playing_song_name.CrossFadeAlpha(1f, 0.8f, false);
        }
    void hide_playing_song()
        {
        playing_song_panel_image.CrossFadeAlpha(0, 0.8f, false);
        playing_song_panel_lable_text.CrossFadeAlpha(0, 0.8f, false);
        playing_song_name.CrossFadeAlpha(0, 0.8f, false);
        }

        
    void SetupVariables()
        {
        print(Data_Store.data.tower_2.name);
        lifes = Data_Store.data.starting_lifes;
        money = Data_Store.data.starting_money;
        }
	public void AddMoney(int money_income)
        {
        money += money_income;
        money_text.text = "Money: " + money;
        }
    public void SubMoney(int money_income)
        {
        money -= money_income;
        money_text.text = "Money: " + money;
        }
    public void SubstractLifes(int lifes_amount)
        {
        lifes -= lifes_amount;
        if (lifes == 0)
            {
            GameOver();
            }
        lifes_text.text = "Lifes: " + lifes.ToString();
        }
    void ChangeTurretsPanelInfo ()
        {
        GameObject.Find("turret_1_info").GetComponent<Text>().text = "Cost: " + 
            Data_Store.data.tower_1.cost.ToString() + "\nDPS: " + 
            System.Math.Round((Data_Store.data.tower_1.damage / Data_Store.data.tower_1.fire_delay), 2).ToString() + 
            "\nRange: " + Data_Store.data.tower_1.range.ToString();
        GameObject.Find("turret_2_info").GetComponent<Text>().text = "Cost: " +
            Data_Store.data.tower_2.cost.ToString() + "\nDPS: " +
            System.Math.Round((Data_Store.data.tower_2.damage / Data_Store.data.tower_2.fire_delay), 2).ToString() +
            "\nRange: " + Data_Store.data.tower_2.range.ToString();
        GameObject.Find("turret_3_info").GetComponent<Text>().text = "Cost: " +
            Data_Store.data.tower_3.cost.ToString() + "\nDPS: " +
            System.Math.Round((Data_Store.data.tower_3.damage / Data_Store.data.tower_3.fire_delay), 2).ToString() +
            "\nRange: " + Data_Store.data.tower_3.range.ToString();
        }
    public void ChangeSelectedTurretTo(int index)
        {
        selected_turret_index = index;
        print("Current turret index: " + index);
        DeselectAllTurretPanels();
        switch(index)
            {
            case 0:
                list_of_turret_panels_images[0].color = new Color(0, 1, 0, 0.4f);
                break;
            case 1:
                list_of_turret_panels_images[1].color = new Color(0, 1, 0, 0.4f);
                break;
            case 2:
                list_of_turret_panels_images[2].color = new Color(0, 1, 0, 0.4f);
                break;
            case -1:
                
                break;
            }
        }
    // Update is called once per frame
    void DeselectAllTurretPanels()
        {
        for (int i = 0; i < list_of_turret_panels_images.Count; i++)
            {
            list_of_turret_panels_images[i].color = new Color(1, 1, 1, 0.4f); ;
            }
        }
    void PlayMusic()
        {
        playlist = Resources.LoadAll<AudioClip>("music");
        audio_source = GetComponent<AudioSource>();
        audio_source.volume = 0.5f;
        ChangeAndPlayAudioTrack();  
        }
    void ChangeAndPlayAudioTrack()
        {
        CancelInvoke();
        music_track = playlist[Random.Range(0, playlist.Length)];
        audio_source.clip = music_track;
        audio_source.Play();
        ShowCurrentPlayingSong();
        Invoke("ChangeAndPlayAudioTrack", music_track.length+2);
        }
    public void PauseUnpauseGame()
        {
        if (game_paused)
            {
            audio_source.volume = 0.5f;
            Time.timeScale = 1;
            game_paused = false;
            ChangePauseMenuStats(false, 0f, 0f);
            }
        else
            {
            audio_source.volume = 0.1f;
            Time.timeScale = 0;          
            game_paused = true;
            ChangePauseMenuStats(true, 0.8f, 1f);
            }
        }
    void ChangePauseMenuStats(bool state, float alpha1, float alpha2,float t_delay = 0.3f)
        {
        if (state)
            {
            EnablePausePanel();
            }
        else
            {
            Invoke("DisablePausePanel", t_delay);
            }
        pause_menu_panel.CrossFadeAlpha(alpha1, t_delay, true);
        resume_game_button.CrossFadeAlpha(alpha2, t_delay, true);
        control_button.CrossFadeAlpha(alpha2, t_delay, true);
        option_button.CrossFadeAlpha(alpha2 * 0.5f, t_delay, true);
        exit_game_button.CrossFadeAlpha(alpha2, t_delay, true);
        resume_game_text.CrossFadeAlpha(alpha2, t_delay, true);
        control_text.CrossFadeAlpha(alpha2, t_delay, true);
        option_text.CrossFadeAlpha(alpha2, t_delay, true);
        exit_game_text.CrossFadeAlpha(alpha2, t_delay, true);
        }
        void EnablePausePanel()
        {
        pause_menu_panel.enabled = true;
        resume_game_button.enabled = true;
        control_button.enabled = true;
        option_button.enabled = true;
        exit_game_button.enabled = true;
        resume_game_text.enabled = true;
        control_text.enabled = true;
        option_text.enabled = true;
        exit_game_text.enabled = true;
        print("PP panel state = " + true.ToString());
        }
    void DisablePausePanel()
        {
        pause_menu_panel.enabled = false;
        resume_game_button.enabled = false;
        control_button.enabled = false;
        option_button.enabled = false;
        exit_game_button.enabled = false;
        resume_game_text.enabled = false;
        control_text.enabled = false;
        option_text.enabled = false;
        exit_game_text.enabled = false;
        print("PP panel state = " + false.ToString());
        }



    void Update () {
        if (!game_over)
            {
            if (Input.GetKeyDown(KeyCode.Space))
                {
                ChangeAndPlayAudioTrack();
                }
            if (Input.GetKeyDown(KeyCode.Escape) && control_showed == false)
                {
                PauseUnpauseGame();
                }
            if (Input.GetKeyDown(KeyCode.R))
                {
                SceneManager.LoadScene("main_scene");
                Destroy(gameObject);
                Instantiate(gameObject);
                }
            } 
        if (game_start_cooldown <= 0)
            {
            timer_panel_text.text = "Time: " + Mathf.Round(time_passed).ToString() + " sec";
            time_passed += Time.deltaTime;
            }
        else
            {
            start_cooldown_text_text.text = "Game will start in " + System.Math.Round(game_start_cooldown).ToString();
            game_start_cooldown -= Time.deltaTime;
            if (game_start_cooldown <= 1)
                {
                HideGameStartCooldown();
                }
            else if (game_start_cooldown <= 3)
                {
                ShowUnecessaryUiStuff();
                }
            }
        if (control_showed && Input.GetKeyDown(KeyCode.Escape))
            {
            ShowHideControls(false);
            }
        }
    public void ShowHideControls(bool show)
        {
        if (show)
            {
            print("Showing controls");
            control_ingame.CrossFadeAlpha(0.9f, 0.5f, true);
            control_showed = true;
            }
        else
            {
            print("Hiding controls");
            control_ingame.CrossFadeAlpha(0f, 0.5f, true);
            control_showed = false;
            }
        print(control_ingame.color);
        print("control_showed_state = " + control_showed.ToString());
        }
    void GameOver()
        {
        game_over = true;
        SceneManager.LoadScene("game_over");
        Invoke("ExitGame", 5f);
        audio_source.volume = 0.1f;
        }
    public void ExitGame()
        {
        Application.Quit();
        }
}
