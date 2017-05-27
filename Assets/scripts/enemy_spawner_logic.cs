using UnityEngine;
using System.Collections;

public class enemy_spawner_logic : MonoBehaviour {
    private float spawn_interval = 3f; //time in seconds
    private float spawn_delay;
    private int wave = 0;
    private int enemies_out_this_wave = 0;
    private int enemies_in_wave = 10;
    private data_store_logic Data_Store;
    private float game_start_delay;
    public GameObject enemy;
    private Color enemy_color;
	// Use this for initialization
    void Start()
        {
        Data_Store = GameObject.Find("Data_Store").GetComponent<data_store_logic>();
        game_start_delay = Data_Store.data.game_start_delay;
        ChangeEnemyColor();
        spawn_delay = spawn_interval;
        }
	void ChangeEnemyColor()
        {
        enemy_color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
	// Update is called once per frame
	void Update () {
        if (game_start_delay <= 0)
            {
            if (spawn_delay >= spawn_interval)
                {
                GameObject new_enemy = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
                new_enemy.transform.FindChild("enemy_1_model").GetComponent<Renderer>().material.color = enemy_color;
                new_enemy.GetComponent<enemy_1_logic>().health += wave * 8;
                enemies_out_this_wave += 1;
                if (enemies_out_this_wave == enemies_in_wave)
                    {
                    wave += 1;
                    enemies_out_this_wave = 0;
                    ChangeEnemyColor();
                    }
                spawn_delay = 0;
                }
            
            else
                {
                spawn_delay += Time.deltaTime;
                }
            
            }
        else
            {
            game_start_delay -= Time.deltaTime;
            }
            
	}
}
