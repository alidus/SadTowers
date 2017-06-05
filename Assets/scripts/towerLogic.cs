using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerLogic : MonoBehaviour
    {
    protected float damage;
    protected float turnRate;
    protected float range;
    protected float main_shoot_cooldown;
    protected int cost;
    protected string codeName;
    protected Vector3 enemyPosition;
    protected GameObject lightObject;
    protected Light lightComp;

    private float current_shoot_cooldown = 0;
    protected GameObject target_enemy_go;
    private GameObject[] enemies;
    private data_store_logic dataStore;
    protected bool isFireing = false;   

    void Start()
        { 
        SetupSpecs(codeName);
        enemies = GameObject.FindGameObjectsWithTag("enemy");
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
    void SetupSpecs(string codename)
        {
        dataStore = GameObject.Find("DataStore").GetComponent<data_store_logic>();
        var towerInfo = dataStore.getTowerData(codename);
        damage = towerInfo.damage;
        cost = towerInfo.cost;
        range = towerInfo.range;
        turnRate = towerInfo.turnRate;
        main_shoot_cooldown = towerInfo.fire_delay;
        print("Specs for "+towerInfo.name+" set up");
        }

    void Update()
        {
        FindNearestEnemy();

        if (target_enemy_go)
            {
            enemyPosition = target_enemy_go.transform.position;
            RotateToEnemyPoint(enemyPosition);
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
            isFireing = false;
            ParticularTurretMethod();
            }

        }
    public virtual void RotateToEnemyPoint(Vector3 enemyPosition)
        {

        }

    public virtual void ParticularTurretMethod()
        {

        }

    public virtual void Shoot()
        {
        // Overloading method

        }
    void dealDelayedDamage()
        {
        try
            {
            enemy_logic enemy_logic = target_enemy_go.GetComponent<enemy_logic>();
            enemy_logic.changeHealth(-damage, "Tower 3 Flame");
            }

        catch (UnityException)
            {
            // Enemy is dead
            }
        }
    }

