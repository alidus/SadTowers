using UnityEngine;
using System.Collections;

public class tower2_gun_logic : MonoBehaviour {
    private Vector3 spawn_point;
    public GameObject rocket_pf;
    private data_store_logic Data_Store;
    private game_logic Game_Logic;
    // Use this for initialization
    void Start () {
        Data_Store = GameObject.Find("DataStore").GetComponent<data_store_logic>();
        Game_Logic = GameObject.Find("GameLogic").GetComponent<game_logic>();
        }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Shoot(int gun_number, float damage)
        {
        switch (gun_number)
            {
            case 0:
                spawn_point = this.transform.transform.FindChild("gun_1").transform.position;
                break;
            case 1:
                spawn_point = this.transform.transform.FindChild("gun_2").transform.position;
                break;
            case 2:
                spawn_point = this.transform.transform.FindChild("gun_3").transform.position;
                break;
            }
        GameObject rocket = (GameObject)Instantiate(rocket_pf, spawn_point, this.transform.rotation);
        tower_2_rocket_logic rocket_logic = rocket.GetComponent<tower_2_rocket_logic>();
        rocket_logic.target_enemy = gameObject.transform.parent.GetComponent<tower2_head_logic>().target_enemy_go;
        rocket_logic.damage = damage;
        rocket_logic.speed = Data_Store.data.tower_2_rocket_base_speed + Game_Logic.time_passed / 100;
        }
    }
