using UnityEngine;
using System.Collections;

public class tower1_head_logic : MonoBehaviour {
    private float tower_damage;
    private float turn_rate = 5f;
    private float range;
    private float main_shoot_cooldown;
    [System.NonSerialized]
    public int cost;
    private float current_shoot_cooldown = 0;
    private GameObject target_enemy_go;
    private Quaternion default_rotation;
    private GameObject[] enemies;
    public GameObject bullet_pf;
    private data_store_logic Data_Store;
	// Use this for initialization
	void Start () {
        SetupSpecs();
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        default_rotation = transform.rotation;
        FindNearestEnemy();
    }
	void FindNearestEnemy()
        {
        target_enemy_go = null;
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        float min_dist = -1;
        foreach (GameObject enemy in enemies)
            {
            Vector3 vector = enemy.transform.position - transform.position;
            if (vector.magnitude <= range)
                {
                if (min_dist == -1)
                {
                min_dist = vector.magnitude;
                target_enemy_go = enemy;
                }
            else
                {
                if (vector.magnitude <= min_dist)
                    {
                    target_enemy_go = enemy;
                    min_dist = vector.magnitude;
                    }
                }
                }
            
            }
      
        }
	// Update is called once per frame
    void SetupSpecs()
        {
        Data_Store = GameObject.Find("Data_Store").GetComponent<data_store_logic>();
        tower_damage = Data_Store.data.tower_1.damage;
        cost = Data_Store.data.tower_1.cost;
        range = Data_Store.data.tower_1.range;
        main_shoot_cooldown = Data_Store.data.tower_1.fire_delay;
        print("Specs set up");
        }
	void Update () {
        FindNearestEnemy();
        
        
        if (target_enemy_go)
            {
            Vector3 target_look_vector = target_enemy_go.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target_look_vector), Time.deltaTime * turn_rate);
            if (current_shoot_cooldown <= 0)
                {
                Shoot();
                current_shoot_cooldown = main_shoot_cooldown;
                }
            else
                {
                current_shoot_cooldown -= Time.deltaTime;
                }
            
            }
        else
            {
            transform.rotation = Quaternion.Lerp(transform.rotation, default_rotation, Time.deltaTime * turn_rate);
            }
        
        }
    void Shoot()
        {
        Vector3 spawn_point = this.transform.FindChild("Cylinder").transform.position;
        GameObject bullet = (GameObject)Instantiate(bullet_pf, spawn_point, this.transform.rotation);
        bullet_1_logic b = bullet.GetComponent<bullet_1_logic>();
        b.target_enemy = target_enemy_go;
        b.damage = tower_damage;
        }
}
