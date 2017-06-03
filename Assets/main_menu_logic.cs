using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class main_menu_logic : MonoBehaviour {

    public Image loading_image;
    public Image control;
    private GameObject menuPanel;
    [SerializeField]
    private GameObject levelChoisePanel;
    [SerializeField]
    private GameObject loadingImage;
    private bool control_showed = false;
    private bool levelsShowed = false;
    private bool levelChosed = false;
	// Use this for initialization
	void Start () {
        control.canvasRenderer.SetAlpha(0);
        menuPanel = GameObject.Find("menu_central_panel");

    }
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKeyDown(KeyCode.Escape))
            {
            if (control_showed)
                {
                control.enabled = false;
                control_showed = false;
                }
            else if (levelsShowed)
                {
                ShowHideLevelChoise();
                }
            
            }
        if (levelsShowed && menuPanel.transform.localPosition.x < 300)
            {
            menuPanel.transform.Translate(new Vector3(1, 0, 0));
            }
        else if (!levelsShowed && menuPanel.transform.localPosition.x > 0)
            {
            menuPanel.transform.Translate(new Vector3(-1, 0, 0));
            }
	}

    public void ButtonPressed()
        {
        print(levelChoisePanel.GetComponent<levelChoise>().choosenLevel);
        if (levelChoisePanel.GetComponent<levelChoise>().choosenLevel != "" && levelChoisePanel.GetComponent<levelChoise>().choosenLevel != null)
            {
            startGame();
            }
        else
            {
            ShowHideLevelChoise();
            }
        }

    public void ShowHideLevelChoise()
        {
        if (levelsShowed) {
            levelsShowed = false;
            CancelInvoke();
            Invoke("hideLevelChoise", 0.2f);
            GameObject.Find("TextStartButton").GetComponent<Text>().text = "Choose level";
            GameObject.Find("start_game_button").GetComponent<Button>().interactable = true;

            } else
            {
            levelsShowed = true;
            CancelInvoke();
            Invoke("showLevelChoise", 0.2f);
            GameObject.Find("TextStartButton").GetComponent<Text>().text = "Start";
            GameObject.Find("start_game_button").GetComponent<Button>().interactable = false;
            };
        //SceneManager.LoadScene("main_scene");
        }

   void startGame()
        {
        ShowLoading();
        SceneManager.LoadScene(levelChoisePanel.GetComponent<levelChoise>().choosenLevel);
        }

    private void showLevelChoise()
        {
        levelChoisePanel.SetActive(true);
        }

    private void hideLevelChoise()
        {
        levelChoisePanel.SetActive(false);
        }
    public void StartGame ()
        {
        //SceneManager.LoadScene("main_scene");
        }

    void ShowLoading()
        {
        loadingImage.GetComponent<Image>().enabled = true;
        }

    void moveMenuRightOneStep()
        {
        menuPanel.transform.Translate(new Vector3(5f, 0, 0));
        print("322");
        }

    public void ExitGame ()
        {
        Application.Quit();
        }

    public void ShowControl()
        {
        print("yes");
        control.enabled = true;
        control.CrossFadeAlpha(1f, 0.2f, true);
        control_showed = true;
        }
}
