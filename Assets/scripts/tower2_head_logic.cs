using UnityEngine;
using System.Collections;

public class tower2_head_logic : MonoBehaviour {
    private float tower_damage;
    private float turn_rate = 5f;
    private float range;
    private float main_shoot_cooldown;
    public int cost;
    private float current_shoot_cooldown = 0;
    public GameObject target_enemy_go;
    private Quaternion default_rotation;
    private GameObject[] enemies;
    public Vector3 air_shot_point;
    private data_store_logic Data_Store;
    private int shooting_gun = 0;
	// Use this for initialization
	void Start () {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        default_rotation = transform.rotation;
        SetupSpecs();
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
    void SetupSpecs()
        {
        Data_Store = GameObject.Find("Data_Store").GetComponent<data_store_logic>();
        tower_damage = Data_Store.data.tower_2.damage;
        cost = Data_Store.data.tower_2.cost;
        range = Data_Store.data.tower_2.range;
        main_shoot_cooldown = Data_Store.data.tower_2.fire_delay;
        }
    // Update is called once per frame
    void Update () {
        FindNearestEnemy();
        
        
        if (target_enemy_go)
            {
            Vector3 target_look_vector = target_enemy_go.transform.position - transform.position;
            air_shot_point = target_look_vector;
            // new Vector3(0, stage2_radius * 0.7f, 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(air_shot_point + new Vector3(0, (target_enemy_go.transform.position - transform.position).magnitude / 3 * 0.7f, 0)), Time.deltaTime * turn_rate);
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
        
        transform.FindChild("tower_gun").GetComponent<tower2_gun_logic>().Shoot(shooting_gun, tower_damage);
        if (shooting_gun == 2)
            {
            shooting_gun = 0;
            }
        else
            {
            shooting_gun += 1;
            }
        }
    }
