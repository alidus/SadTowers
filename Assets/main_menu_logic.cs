using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class main_menu_logic : MonoBehaviour {

    public Image loading_image;
    public Image control;
    private bool control_showed = false;
	// Use this for initialization
	void Start () {
        control.canvasRenderer.SetAlpha(0);
	}
	
	// Update is called once per frame
	void Update () {
	if (control_showed && Input.GetKeyDown(KeyCode.Escape))
            {
            control.CrossFadeAlpha(0, 0.3f, false);
            control_showed = false;
            print("No");
            }
        print(control.color);
	}
    public void StartGame ()
        {
        loading_image.enabled = true;
        SceneManager.LoadScene("main_scene");
        }
    public void ExitGame ()
        {
        Application.Quit();
        }
    public void ShowControl()
        {
        print("yes");
        control_showed = true;
        control.CrossFadeAlpha(1f, 0.5f, true);
        }
}
