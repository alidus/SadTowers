using UnityEngine;
using System.Collections;

public class tower_2_1_head_logic : towerLogic {
    Vector3 projectileSpawnPoint;
    GameObject rocketPrefab;

    private void Awake()
        {
        codeName = "trocket2";

        lightObject = new GameObject("The Light");
        lightComp = lightObject.AddComponent<Light>();
        lightComp.intensity = 5;
        lightComp.color = Color.green;
        lightObject.transform.position = transform.position;

        projectileSpawnPoint = transform.GetChild(0).GetChild(2).transform.position;
        rocketPrefab = Resources.Load("game_units/towers/ammo/tower_2_rocket") as GameObject;
        }


    public override void RotateToEnemyPoint(Vector3 enemyPosition)
        {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(enemyPosition - transform.position), Time.deltaTime * turnRate);
        }

    public override void Shoot()
        {
        tower_2_rocket_logic projectile = Instantiate(rocketPrefab, projectileSpawnPoint, this.transform.rotation).GetComponent<tower_2_rocket_logic>();
        projectile.target_enemy = target_enemy_go;
        projectile.damage = damage;
        }
    }
