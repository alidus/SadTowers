using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;


public class levelChoise : MonoBehaviour {
    EditorBuildSettingsScene[] mapScenes;
    bool lastEnabled = false;
    public string choosenLevel = null;

    [SerializeField]
    GameObject buttonPrefab;

    [SerializeField]
    GameObject startButton;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        print(choosenLevel);
	}
    private void OnDisable()
        {
        choosenLevel = null;
        }

    private void OnEnable()
        {  
        mapScenes = EditorBuildSettings.scenes;

        foreach (Transform child in this.transform)
            {
            Destroy(child.gameObject);
            }

        foreach (EditorBuildSettingsScene scene in mapScenes)
            {

            int i = 0;
            if (scene.path.IndexOf("level") != -1)
                {
                // determine name
                string name = scene.path.Substring(scene.path.IndexOf("level")+6);
                name = name.Substring(0, name.IndexOf(".unity"));
                print(name);

                GameObject button = (GameObject)Instantiate(buttonPrefab, this.transform.position + i * buttonPrefab.transform.localScale, this.transform.rotation) as GameObject;
                button.GetComponentInChildren<Text>().text = name;
                button.transform.SetParent(this.transform, false);
                button.GetComponent<Button>().onClick.AddListener(
                    () => { choosenLevel = scene.path;
                        startButton.GetComponent<Button>().interactable = true;
                    }
                    );
                }
            i++;
            }
        


        }
    }
