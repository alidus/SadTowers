using UnityEngine;
using System.Collections;

public class tower_3_head_logic : MonoBehaviour
    {
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
    private data_store_logic Data_Store;
    private bool isFireing = false;
    private GameObject FlameParticles;
    // Use this for initialization
    void Start()
        {
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
        Data_Store = GameObject.Find("DataStore").GetComponent<data_store_logic>();
        tower_damage = Data_Store.data.tower_3.damage;
        cost = Data_Store.data.tower_3.cost;
        range = Data_Store.data.tower_3.range;
        main_shoot_cooldown = Data_Store.data.tower_3.fire_delay;
        print("Specs set up");
        }
    void Update()
        {
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
            isFireing = false;
            Destroy(FlameParticles);
            }

        }
    void Shoot()
        {
        if (!isFireing)
            {
            isFireing = true;
            Vector3 spawn_point = this.transform.position + new Vector3(0, 0, 0);
            FlameParticles = Instantiate(Resources.Load("game_units/enemies/FlamethrowerFlame"), spawn_point, transform.rotation) as GameObject;
            }
        else
            {
            FlameParticles.transform.rotation = this.transform.rotation;
            }

        Invoke("dealDelayedDamage", 0.5f);


        }
    void dealDelayedDamage()
        {
        try
            {
            enemy_logic enemy_logic = target_enemy_go.GetComponent<enemy_logic>();
            enemy_logic.changeHealth(-tower_damage, "Tower 3 Flame");
            }

        catch (UnityException)
            {
            // Enemy is dead
            }
        }
    }

