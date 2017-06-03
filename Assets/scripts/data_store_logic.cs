using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class data_store_logic : MonoBehaviour {
    // Data_Store = GameObject.Find("Data_Store").GetComponent<data_store_logic>();
    public SerializedData data;
    [System.Serializable]
    public class SerializedData
        {
        public TowerData tower_1;
        public TowerData tower_2;
        public TowerData tower_2_1;
        public TowerData tower_3;
        public TowerData tower_4;
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
        switch (codename)
            {
            case "tsimple1":
                return data.tower_1;
                break;
            case "trocket1":
                return data.tower_2;
                break;
            case "trocket2":
                return data.tower_2_1;
                break;
            case "tflame1":
                return data.tower_3;
                break;
            case "tdark1":
                return data.tower_4;
                break;
            default:
                return null;
            }
        }
}
