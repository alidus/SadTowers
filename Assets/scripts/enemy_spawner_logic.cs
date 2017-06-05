using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class enemy_spawner_logic : MonoBehaviour {
    private float spawnTime = 3f; //time in seconds
    private float spawn_delay;
    private int wave = 0;
    private int enemiesSpawnedInWave = 0;
    private int totalEnemiesInWave;
    private int gameMode = 0; // 0 - endless, 1 - limited waves
    private data_store_logic Data_Store;
    private float game_start_delay;
    private Color enemyColor = new Color(0, 0, 0);
    private List<GameObject> listOfEnemiesModels = new List<GameObject>();
    private GameObject enemyProto;
    [SerializeField]
    private waveNotify waveNotifier;
	// Use this for initialization
    void Start()
        {
        enemyProto = Resources.Load("game_units/enemies/enemyProto") as GameObject;
        Data_Store = GameObject.Find("DataStore").GetComponent<data_store_logic>();
        loadAllEnemiesModels();
        game_start_delay = Data_Store.getData().game_start_delay;
        if (gameMode == 0)
            {
            spawn_delay = spawnTime;
            totalEnemiesInWave = 10;
            }
        }
    void loadAllEnemiesModels()
        {
        Object[] l =  Resources.LoadAll("game_units/enemies/models");
        foreach (GameObject model in l)
            {
            listOfEnemiesModels.Add(model as GameObject);
            }
        }
    GameObject getRandomModel()
        {
        return listOfEnemiesModels[Random.Range(0, listOfEnemiesModels.Count)];
        }
	// Update is called once per frame
    void spawnEnemy(GameObject model, float health, float speed, Color color)
        {
        GameObject new_enemy = Instantiate(enemyProto, transform.position, transform.rotation) as GameObject;
        model = Instantiate(getRandomModel(), new_enemy.transform.position, new_enemy.transform.rotation) as GameObject;
        model.transform.parent = new_enemy.transform;
        new_enemy.GetComponent<enemy_logic>().changeHealth(health, "Wave health bonus");
        print("(Spawner) Spawned enemy(M:" + model + ", H:" + health + "S: " + speed + ", C:"+color.ToString());
        }

    int getCurrentWaveEnemyHealth()
        {
        return Data_Store.getData().endless_mode_base_health + wave * 5;
        }

    int getCurrentWaveEnemySpeed()
        {
        return Data_Store.getData().endless_mode_base_speed + Data_Store.getData().endless_mode_speed_per_wave * wave;
        }

    void Update () {
        if (game_start_delay <= 0)
            {
            switch (gameMode)
                {
                // Endless
                case 0:
                    if (spawn_delay >= spawnTime)
                        {
                        // TODO: spawn
                        spawnEnemy(getRandomModel(), getCurrentWaveEnemyHealth(), getCurrentWaveEnemySpeed(), enemyColor);
                        enemiesSpawnedInWave += 1;
                        if (enemiesSpawnedInWave == totalEnemiesInWave)
                            {
                            wave += 1;
                            print("Wave " + wave + " incoming");
                            waveNotifier.Notify(wave + 1);
                            enemiesSpawnedInWave = 0;
                            }
                        spawn_delay = 0;
                        }

                    else
                        {
                        spawn_delay += Time.deltaTime;
                        }
                    break;
                default:
                    break;
                }


            }
        else
            {
            game_start_delay -= Time.deltaTime;
            }
            
	}
}
