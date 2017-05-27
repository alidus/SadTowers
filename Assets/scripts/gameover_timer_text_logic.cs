using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameover_timer_text_logic : MonoBehaviour {
    game_logic game_logic;
	// Use this for initialization
	void Start () {
        game_logic = GameObject.Find("Game_Logic").GetComponent<game_logic>();
        gameObject.GetComponent<Text>().text = "You survived for "+Mathf.Round(game_logic.time_passed)+" seconds!";
        }

}
