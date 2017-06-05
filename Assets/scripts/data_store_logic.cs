using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class data_store_logic : MonoBehaviour {
    // Data_Store = GameObject.Find("Data_Store").GetComponent<data_store_logic>();
    private SerializedData data;
    [System.Serializable]
    public class SerializedData
        {
        public TowerData[] towersData;
        public int starting_money;
        public int starting_lifes;
        public int tower_2_rocket_base_speed;
        public int game_start_delay;
        public int endless_mode_base_health;
        public int endless_mode_base_speed;
        public int endless_mode_speed_per_wave;

        }
    [System.Serializable]
    public class TowerData
        {
        public string name;
        public int cost;
        public float range;
        public string codename;
        public float damage;
        public float fire_delay;
        public float turnRate;
        }
	// Use this for initialization
    void Awake ()
        {
        data = JsonUtility.FromJson<SerializedData>((Resources.Load("game_data/towers_info") as TextAsset).text);
        }

    public TowerData getTowerData(string codename)
        {
        TowerData resultTowerData = null;
        foreach (TowerData towerData in data.towersData)
            { 
            if (towerData.codename == codename)
                {
                resultTowerData = towerData;
                } 
            }
        return resultTowerData;
        }

    public TowerData getTowerData(int index)
        {
        return data.towersData[index];
        }

    public SerializedData getData()
        {
        return data;
        }
    }
