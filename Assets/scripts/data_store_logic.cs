using UnityEngine;
using System.Collections;
using System.IO;

public class data_store_logic : MonoBehaviour {
    // Data_Store = GameObject.Find("Data_Store").GetComponent<data_store_logic>();
    TextAsset data_asset;
    public SerializedData data;
    [System.Serializable]
    public class SerializedData
        {
        public TowerData tower_1;
        public TowerData tower_2;
        public TowerData tower_2_1;
        public TowerData tower_3;
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
        public float damage;
        public float fire_delay;
        }
	// Use this for initialization
    void Awake ()
        {
        data_asset = Resources.Load("game_data/towers_info") as TextAsset;
        data = JsonUtility.FromJson<SerializedData>(data_asset.text);
        }
	void Start () {
        
        }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
