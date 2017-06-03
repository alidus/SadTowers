using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waveNotify : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
        text = this.transform.FindChild("Text").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Notify(int wave)
        {
        text.text = "Wave " + wave.ToString();
        fadeIn();
        Invoke("fadeOut", 2);
        }

    void fadeIn()
        {
        this.GetComponent<Image>().CrossFadeAlpha(0.8f, 0.5f, true);
        text.CrossFadeAlpha(0.8f, 0.5f, true);
        }

    void fadeOut()
        {
        this.GetComponent<Image>().CrossFadeAlpha(0f, 0.5f, true);
        text.CrossFadeAlpha(0f, 0.5f, true);
        }
}
